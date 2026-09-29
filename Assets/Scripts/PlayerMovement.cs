using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    //SerializeField eh usado para poder editar diretamente da unity
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anime;
    private bool grounded;
    private int jumpCount = 0;
    [SerializeField] private int maxJumps = 2; // Maximo de pulos

    private void Awake(){

        body = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    private void Update(){

        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        
        //virar o sprite quando move pra esquerda e direita
        if(horizontalInput > 0.01f){
            transform.localScale = new Vector3(3, 3, 3);
        }else if(horizontalInput < -0.01f){
            transform.localScale = new Vector3(-3, 3, 3);
        }

        if(Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps){
            Jump();
        }

        //Animacoes
        anime.SetBool("run", horizontalInput != 0);
        anime.SetBool("grounded", grounded);     
    }

    private void Jump(){
        //mantem a mesma velocidade no x e aplica o valor de speed para y
        body.velocity = new Vector2(body.velocity.x, speed);
        anime.SetTrigger("jump");
        grounded = false;
        jumpCount++;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground"){
            grounded = true;
            jumpCount = 0; // Reseta jump count quando ground = true
        }
    }

}
