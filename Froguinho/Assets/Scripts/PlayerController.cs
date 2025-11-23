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
    private bool isDoubleJump;

    [Header("Animator")]
    public Animator anim;


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
                Jump();
                canDoubleJump = true;
                anim.SetBool("isDoubleJump", false);
            }
            else 
            {
                // usa o canDoubleJump habilitado ao final do "if (isGrounded == true)" para acessar um segundo pulo no ar,
                //desativa mais pulos caso já tenha usado um segundo pulo
                if (canDoubleJump == true)
                {
                    Jump();
                    isDoubleJump = true;
                    anim.SetBool("isDoubleJump", true);
                }
            }
        }

        // verifica o valor da velocidade no X é maior ou menor que zero para usar na movimentacao e inverter
        // todo o eixo x do player, assim inverte nao apenas o sprite do personagem, mas tudo atrelado a ele,
        // como ponto de disparo de projetil
        if(theRB.linearVelocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if(theRB.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        // animation setup

        // Aqui é controlado para que faça a transicao no animator usando a variavel speed criada no animator, aqui também
        // é passado para aplicar o valor positivo independente da direção(-5 pode se tornar simplesmente 5), assim aplica a 
        // animação que está pra ser chamada sempre que a velocidade for maior que 0.1
        anim.SetFloat("speed", Mathf.Abs (theRB.linearVelocity.x));

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("ySpeed", theRB.linearVelocity.y);
    }

    void Jump()
    {
        theRB.linearVelocity = new Vector2(theRB.linearVelocity.x, jumpForce);
    }
}
