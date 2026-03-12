using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    private void Awake()
    {
        // apenas uma Instance pode ser ativada por vez,
        // por exemplo: se houver mais de um Player usando esse script, ele tera as informacoes do ultimo que chamar,
        // ou seja, apenas o de 1 player que vai setar o instance para todos
        instance = this;
    }

    public int currentHealth;
    public int maxHealth;

    public float invincibilityLength = 1f;
    private float invincibilityCounter;

    public SpriteRenderer theSR;
    public Color normalColor;
    public Color fadeColor;

    private PlayerController thePlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        thePlayer = GetComponent<PlayerController>();
        // para que o player comece o game com HP full
        currentHealth = maxHealth;

        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            if(invincibilityCounter <= 0)
            {
                theSR.color = normalColor;
            }
        }


// Usado para limitar esse trecho de codigo para funcionar apenas no editor
#if UNITY_EDITOR

        // para testar a funcionalidade de adição de hp
        if(Input.GetKeyDown(KeyCode.H))
        {
            AddHealth(1);
        }
#endif
    }

    public void DamagePlayer()
    {

        if (invincibilityCounter <= 0)
        {

            // invincibilityCounter = invincibilityLength;

            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                //gameObject.SetActive(false);

                LifeController.instance.Respawn();
            }
            else
            {
                invincibilityCounter = invincibilityLength;

                theSR.color = fadeColor;

                thePlayer.KnockBack();
            }

                UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
        }
    }

    public void AddHealth(int amountToAdd)
    {
        // função para que seja adicionado hp ao personagem, mas caso ultrapasse o hp maximo, o hp sera setado para
        // o valor do hp maximo, assim o player não excede o tamanho maximo de hp

        currentHealth += amountToAdd;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealthDisplay(currentHealth, maxHealth);
    }
}
