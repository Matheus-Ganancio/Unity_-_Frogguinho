using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    [Header("Orientacao da camera")]
    public bool freezeVertical;
    public bool freezeHorizontal;
    private Vector3 positionStore;

    // clampMin e clampMax serao usadas para definir o ponto A e B maximo do qual podera ser mostrado na tela do game,
    // muito util para evitar a camera vazar ao seguir o player e mostrar algum local vazio (basicamente limitacao de alcance
    // da camera)
    public bool clampPosition;
    public Transform clampMin;
    public Transform clampMax;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionStore = transform.position;

        // remove a camera ser pai dos emptyObjects que foram adicionados as variaveis clampMin e clampMax dentro da unity,
        // no editor a camera esta como pai desses objetos para organizacao do projeto, aqui deixa de ser ao iniciar o game
        // para que a camera nao mova os clamps ao se mover
        clampMin.SetParent(null);
        clampMax.SetParent(null);
    }

    // LateUpdate faz também ser chamado uma vez por frame como o update, mas sera chamado apos os outros
    // updates terem rodado, é importante usar na camera para evitar um possivel efeito de "Jerky" na movimentacao dela
    void LateUpdate()
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

        if(clampPosition == true)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, clampMin.position.x, clampMax.position.x),
                Mathf.Clamp(transform.position.y, clampMin.position.y, clampMax.position.y),
                transform.position.z);
        }
    }
}
