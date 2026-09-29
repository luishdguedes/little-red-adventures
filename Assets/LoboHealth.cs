using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoboHealth : MonoBehaviour
{
    public int health = 300;
    public bool isInvulnerable = false;

    public LoboHealthBar healthBar;
    public float delayBeforeDestroy = 5f;

    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("hp = " + health);
        if(isInvulnerable)
            return;

        health -= damage;
        healthBar.SetHealth(health);

        if (health <= 150)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        if (health <= 0)
        {
            GetComponent<Animator>().SetTrigger("Die");
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject, delayBeforeDestroy);
    }
}
