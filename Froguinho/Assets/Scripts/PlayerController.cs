using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // não precisa usar "if" pq o valor é negativo apertando pra esquerda ou positivo apertando pra direita
        theRB.linearVelocity = new Vector2( Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.linearVelocity.y);

        if(Input.GetButtonDown("Jump"))
        {
            theRB.linearVelocity = new Vector2(theRB.linearVelocity.x, jumpForce);
        }
    }
}
