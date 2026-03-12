using UnityEngine;

public class TreelineMover : MonoBehaviour
{
    public float maxDistance = 22f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // controle para usar a posição da camera para ajustar o "Treeline" para repetir a imagem ao player
        // chegar na ponta esquerda ou direita da camera, ressaltando que esta sendo referenciado ao Treeline
        // via script component anexado ao objeto na unity
        float distance = transform.position.x - Camera.main.transform.position.x;
        
        // se a distance for maior ou menor que o maxDistance, desloca o objeto para uma posição a
        // direita da tela ou esquerda da tela, multiplicando o maxDistance *2, esquerda caso distance seja maior
        // e direita caso o distance seja menor
        if(distance > maxDistance)
        {
            transform.position -= new Vector3(maxDistance * 2f, 0f, 0f);
        }
        if(distance < -maxDistance)
        {
            transform.position += new Vector3(maxDistance * 2f, 0f, 0f);
        }
    }
}
