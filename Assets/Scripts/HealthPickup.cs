using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthToAdd;

    public GameObject pickupEffect;

    public bool giveFullHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player"))
        {
            // confirma que o player nao esta com o hp maximo para poder pegar mais hp, assim ele nao perde
            // o item de recuperacao de hp caso pegue com o hp ja no maximo
            if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
            {
                if (giveFullHealth == true)
                {
                    PlayerHealthController.instance.AddHealth(PlayerHealthController.instance.maxHealth);
                }
                else
                {
                    PlayerHealthController.instance.AddHealth(healthToAdd);
                }

                Destroy(gameObject);
                Instantiate(pickupEffect, transform.position, transform.rotation);
            }
        }
    }
}
