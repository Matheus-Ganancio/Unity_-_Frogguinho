using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private PlayerHealthController healthController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // vai encontrar a função "DamagePlayer" dentro do "PlayerHealthController" e executar a função
        //healthController = FindFirstObjectByType<PlayerHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnTriggerEnter2D, faz algo quando um colisor entrar na area de ativacao, o objeto colisor precisar ter
    // um RigidBody nele
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.gameObject.SetActive(false);

            //healthController.DamagePlayer();

            // busca na "instance" criada em "PlayerHealthController", ter criado uma "instance" em "PlayerHealthController"
            // ajuda muito nesse caso, pois a unity nao vai precisar correr todo o script sempre que for procurar(caso fosse usado
            // "FindFirstObjectByType" por exemplo, o que seria problematico e causaria lag se fosse ter que acontecer multiplas
            // vezes
            PlayerHealthController.instance.DamagePlayer();

        }
    }
}
