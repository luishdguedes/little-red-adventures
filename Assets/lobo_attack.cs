using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lobo_attack : MonoBehaviour
{
    public float attackRange = 1f;
    public LayerMask attackMask;
    public Vector3 attackOffset;

    public void Attack()
{
    Vector3 pos = transform.position;
    pos += transform.right * attackOffset.x;
    pos += transform.up * attackOffset.y;

    Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
    if (colInfo != null)
    {
        Health playerHealth = colInfo.GetComponent<Health>();
        if (playerHealth != null)
        {
            Debug.Log("Player hit!");
            playerHealth.TakeDamage(1); // Ou a quantidade de dano que você deseja causar
        }
        else
        {
            Debug.Log("Health component not found!");
        }
    }
    else
    {
        Debug.Log("No collider found in attack range.");
    }
}

    // Adicione este método para visualizar o alcance do ataque no Editor
    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
