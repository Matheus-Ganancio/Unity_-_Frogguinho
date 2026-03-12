using System.Collections;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public static LifeController instance;
    private void Awake()
    {
        instance = this;
    }

    private PlayerController thePlayer;

    public float respawnDelay = 2f;

    public int currentLives = 3;

    public GameObject deathEffect;
    public GameObject respawnEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thePlayer = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        //thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;

        // para deixar o player com o HP preenchido igual ao HP maximo quando ele respawnar
        //PlayerHealthController.instance.AddHealth(PlayerHealthController.instance.maxHealth);

        thePlayer.gameObject.SetActive(false);

        // para evitar que a morte com queda (por exemplo buraco) faça o player nascer pixeis abaixo de onde deveria
        thePlayer.theRB.linearVelocity = Vector2.zero;

        currentLives--;

        if(currentLives > 0 )
        {
            StartCoroutine(RespawnCo());
        }

        // so uma garantia se por algum motivo algo no game for fazer o player perder mais de uma vida, e o valor seria -X,
        // a UI nao mostrar vida negativa na imagem
        else
        {
            currentLives = 0;

        StartCoroutine(GameOverCo());
        }

        if(UIController.instance != null)
        {

            UIController.instance.UpdateLivesDisplay(currentLives);
        }

        Instantiate(deathEffect, thePlayer.transform.position, deathEffect.transform.rotation);
    }

    public IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(respawnDelay);

        thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;

        // para deixar o player com o HP preenchido igual ao HP maximo quando ele respawnar
        PlayerHealthController.instance.AddHealth(PlayerHealthController.instance.maxHealth);

        thePlayer.gameObject.SetActive(true);

        Instantiate(respawnEffect, thePlayer.transform.position, respawnEffect.transform.rotation);
    }

    public IEnumerator GameOverCo()
    {
        yield return new WaitForSeconds(respawnDelay);
        
        if(UIController.instance != null )
        {
            UIController.instance.ShowGameOver();
        }
    }
}
