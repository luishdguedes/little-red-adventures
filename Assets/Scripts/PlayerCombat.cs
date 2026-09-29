using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour{

    public Animator animator;

    public Transform attackPoint;

    public float attackRange = 0.5f;
    public int attackDamage = 40;
    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;
    public LayerMask enemyLayers;
    private PlayerMovement playerMovement;
    

    private void Awake(){
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown("j") && cooldownTimer > attackCooldown){
            Attack();
       } 

       cooldownTimer += Time.deltaTime;
    }

    void Attack()
    {
        cooldownTimer = 0;
        animator.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Skull skull = enemy.GetComponent<Skull>();
            if (skull != null)
            {
                skull.TakeDamage(attackDamage);
                continue; // Se encontrou um Skull, pula para o próximo enemy
            }

            LoboHealth lobo = enemy.GetComponent<LoboHealth>();
            if (lobo != null)
            {
                lobo.TakeDamage(attackDamage);
                continue; // Se encontrou um Lobo, pula para o próximo enemy
            }
        }
    }

    void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
