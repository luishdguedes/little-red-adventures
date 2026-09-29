using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    [Header("Parametros de ataques")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;

    [Header("Collider")]
    [SerializeField]private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField]private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator animator;

    private Health playerHealth;

    private EnemyPatrol enemyPatrol;

    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    
    private void Awake(){
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update(){
        
        cooldownTimer += Time.deltaTime;

        if(PlayerInSight()){
            if(cooldownTimer >= attackCooldown){
                cooldownTimer = 0;
                animator.SetTrigger("attack");
            }
        }

        if(enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight(){
        float resultadoX;
        if(transform.localScale.x <= 0){
            resultadoX = -1;
        }else{
            resultadoX = 1;
        }

        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * resultadoX * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);

        if(hit.collider != null){
            playerHealth = hit.transform.GetComponent<Health>();
        }
        

        return hit.collider != null;
    }

    private void OnDrawGizmos(){
        float resultadoX;
        if(transform.localScale.x <= 0){
            resultadoX = -1;
        }else{
            resultadoX = 1;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * resultadoX * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer(){
        if(PlayerInSight()){
            playerHealth.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage){
        
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        Debug.Log("Enemy died!");

        animator.SetTrigger("die");
        if(GetComponent<EnemyPatrol>() != null){
            GetComponent<EnemyPatrol>().enabled = false;
        }
        if(GetComponent<Collider2D>() != null){
            GetComponent<Collider2D>().enabled = false;
        }
        
        this.enabled = false;
        GameObject objetoParaDestruir = GameObject.Find("Enemies (1)"); // Encontre o objeto pelo nome
        Destroy(objetoParaDestruir, 1.4f);
    }
}
