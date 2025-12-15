using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Transform theCam;

    public Transform sky;
    public Transform treeline;
    [Range(0f, 1f)]

    public float parallaxSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theCam = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        sky.position = new Vector3(theCam.position.x, theCam.position.y, sky.position.z);

        // controle para que o conteudo da variavel "treeline" se mova se baseando na posição X e Y da
        // camera(variavel "theCam") pela velocidade setada na variavel "parallaxSpeed"
        treeline.position = new Vector3(
            theCam.position.x * parallaxSpeed,
            theCam.position.y,
            treeline.position.z);
    }
}
