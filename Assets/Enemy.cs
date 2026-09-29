using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;
    public int debugHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        debugHealth = currentHealth;
    }

    public void TakeDamage(int damage){
        
        currentHealth -= damage;
        debugHealth = currentHealth;

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        Debug.Log("Enemy died!");

        animator.SetBool("IsDead", true);

        
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    
}
