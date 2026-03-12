using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public Checkpoint[] allCP;

    private Checkpoint activeCP;
    public Vector3 respawnPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // serve para não organizar os objetos checkpoints → FindObjectsSortMode.None
        allCP = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);

        foreach (Checkpoint cp in allCP)
        {
            cp.cpMan = this;
        }

        respawnPosition = FindFirstObjectByType<PlayerController>().transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.C))
        //{
        //    DeactivateAllCheckpoints();
        //}
    }

    public void DeactivateAllCheckpoints()
    {
        foreach(Checkpoint cp in allCP)
        {
            cp.DeactivateCheckpoint();
        }
    }

    public void SetActiveCheckpoint(Checkpoint newActiveCP)
    {
        DeactivateAllCheckpoints();

        activeCP = newActiveCP;

        respawnPosition = newActiveCP.transform.position;
    }
}
