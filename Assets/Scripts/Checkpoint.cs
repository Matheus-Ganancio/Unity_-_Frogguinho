using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isActive;
    public Animator anim;

    // oculto inves de privado para usar o script CheckpointManager para desativar os checkpoints ja ativos anteriormente
    // ao pegar um checkpoint novo
    [HideInInspector]
    public CheckpointManager cpMan;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // checa se o trigger é causado pelo player e se o bool da animacao esta como false, caso ambos sejam sim,
        // sera tocada a animacao e a bool sera verdadeira
        if(other.tag == "Player" && isActive == false)
        {
            // ativa o checkpoint atual depois de usar o script Checkpoint Manager para desativar todos os anteriores
            // para evitar deixar mais de um checkpoint ativo em cena simultaneamente
            cpMan.SetActiveCheckpoint(this);

            anim.SetBool("flagActive", true);

            isActive = true;
        }
    }

    public void DeactivateCheckpoint()
    {
        anim.SetBool("flagActive", false);
        isActive = false;
    }
}
