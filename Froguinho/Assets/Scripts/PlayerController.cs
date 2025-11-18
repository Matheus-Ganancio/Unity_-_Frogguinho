using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimentacao direcional")]
    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    public float runSpeed;
    // essa variavel vai ser usada para transicionar entre a velocidade de movimento padrão "moveSpeed" e
    // a velocidade de movimento da corrida "runSpeed"
    private float activeSpeed;

    [Header("Configuracao de pulo")]
    public Transform groundedCheckPoint;
    public float groundedCheckRadius;
    public LayerMask whatIsGrounded;
    private bool isGrounded;
    private bool canDoubleJump;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Physics2D.OverlapCircle serve para criar um circulo imaginário qual será usado para verificar se o player
        // está ou não em contato com o chão, assim controlar o bool "isGrounded" para o player pular apenas quando
        // estiver no chão
        isGrounded = Physics2D.OverlapCircle(groundedCheckPoint.position, groundedCheckRadius, whatIsGrounded);

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
            if (isGrounded == true)
            {
                theRB.linearVelocity = new Vector2(theRB.linearVelocity.x, jumpForce);
                canDoubleJump = true;
            }
            else 
            {
                // usa o canDoubleJump habilitado ao final do "if (isGrounded == true)" para acessar um segundo pulo no ar,
                //desativa mais pulos caso já tenha usado um segundo pulo
                if (canDoubleJump == true)
                {
                    theRB.linearVelocity = new Vector2(theRB.linearVelocity.x, jumpForce);
                    canDoubleJump = false;
                }
            }
        }
    }
}
