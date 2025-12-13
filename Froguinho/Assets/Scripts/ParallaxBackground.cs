using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Transform theCam;

    public Transform sky;
    public Transform treeline;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theCam = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        sky.position = new Vector3(theCam.position.x, theCam.position.y, sky.position.z);
    }
}
