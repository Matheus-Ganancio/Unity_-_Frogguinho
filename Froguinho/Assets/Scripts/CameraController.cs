using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    public bool freezeVertical;
    public bool freezeHorizontal;

    private Vector3 positionStore; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionStore = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Aqui sera difinido via o check dos bool "freezeVertical" e "freezeHorizontal" qual eixo de positionStore
        // sera preservado, qual teve o vetor salvo no local padrao ao iniciar o game
        if(freezeVertical == true)
        {
            // aqui trava a camera para seguir o player apenas no Y, preservando a posição padrão da camera no X e Z
            transform.position = new Vector3(transform.position.x, positionStore.y, transform.position.z);
        }
        if(freezeHorizontal == true)
        {
            // aqui trava a camera para seguir o player apenas no X, preservando a posição padrão da camera no Y e Z
            transform.position = new Vector3(positionStore.x, transform.position.y, transform.position.z);
        }
    }
}
