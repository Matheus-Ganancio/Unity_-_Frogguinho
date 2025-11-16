using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    public float runSpeed;

    private float activeSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // não precisa usar "if" pq o valor é negativo apertando pra esquerda ou positivo apertando pra direita
        // Vector2 usa os eixos X e Y, no caso está multiplicando o input da unity "Horizontal" pela variavel "modeSpeed"
        // e aplicando na linearVelocity.y, assim faz se mover baseado no valor da variavel para velocidade
        //theRB.linearVelocity = new Vector2( Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.linearVelocity.y);


        // faz com que quando o player aperte o LeftShift, a activeSpeed se torne runSpeed, para alterar a velocidade
        // de andando para correndo
        activeSpeed = moveSpeed;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            activeSpeed = runSpeed;
        }

        theRB.linearVelocity = new Vector2( Input.GetAxisRaw("Horizontal") * activeSpeed, theRB.linearVelocity.y);

        if(Input.GetButtonDown("Jump"))
        {
            theRB.linearVelocity = new Vector2(theRB.linearVelocity.x, jumpForce);
        }
    }
}
