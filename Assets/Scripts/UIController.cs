using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }

    public Image[] heartIcons;

    public Sprite heartFull;
    public Sprite heartEmpty;

    public TMP_Text livesText;

    public GameObject gameOverScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateLivesDisplay(LifeController.instance.currentLives);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthDisplay(int health, int maxHealth)
    {
        for(int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = true;

            //if(health <= i)
            //{
            //    heartIcons[i].enabled = false;
            //}

            if(health > i)
            {
                heartIcons[i].sprite = heartFull;
            }
            else
            {
                heartIcons[i].sprite = heartEmpty;

                if(maxHealth <= i)
                {
                    heartIcons[i].enabled = false;
                }
            }

        }
    }

    public void UpdateLivesDisplay(int currentLives)
    {
        // acessa o texto do componente e devolve como string
        livesText.text = currentLives.ToString();
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }
    public void Restart()
    {
        //Debug.Log("Restarting");

        // usado para recarregar a cena, no caso esta sendo usado para reiniciar o level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
