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

    // sera usado para salvar o calculo de metade da resolucao usada naquele momento no game, util para limitar no editor
    // onde o clamp finaliza  como um ponto final ou inicial, inves deixar a camera passar parcialmente dele, ja que sem esse
    // calculo a camera ultrapassa ate que a referencia de clamp do editor chegue ao centro da tela
    private float halfHeight;
    private float halfWidth;
    public Camera theCam;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionStore = transform.position;

        // remove a camera ser pai dos emptyObjects que foram adicionados as variaveis clampMin e clampMax dentro da unity,
        // no editor a camera esta como pai desses objetos para organizacao do projeto, aqui deixa de ser ao iniciar o game
        // para que a camera nao mova os clamps ao se mover
        clampMin.SetParent(null);
        clampMax.SetParent(null);

        // esta sendo usado para setar o calculo do aspect ratio do game
        halfHeight = theCam.orthographicSize;
        halfWidth = theCam.orthographicSize * theCam.aspect;
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
            // usa as referencias inseridas nos transforms para definir limitador da camera(clamp) e subtrai a variavel
            // que guarda e calcula metade do aspect ratio que o player esta usando para controle do clamp como ponto
            // final ou inicial
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth),
                Mathf.Clamp(transform.position.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight),
                transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        // Caso a bool clampPosition esteja ativa, o editor(e apenas no editor) vai mostrar linhas desenhadas com o gizmo,
        // cada linha parte de um clamp adicionado ao slot no editor criado com as variaveis "clampMin" e "clampMax", onde
        // o eixo X de um clamp segue o eixo Y de outro, assim criando 4 linhas pra formar uma area de visao do clamp em cada
        // ponto do game
        if(clampPosition == true)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));

            Gizmos.DrawLine(clampMax.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));
        }
    }
}
