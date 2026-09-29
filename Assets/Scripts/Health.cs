using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour{
    [SerializeField] private float startingHealth;
    public float currentHealth{ get; private set;}
    private Animator animator;
    private bool dead;

    private void Awake(){
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
    }

    private void Update(){
            
    }

    public void TakeDamage(float _damage){
        Debug.Log(Mathf.Clamp(currentHealth - _damage, 0, startingHealth));
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0){
            animator.SetTrigger("hurt");
        }else{
            if(!dead){
                if (animator != null)
                {
                    animator.SetTrigger("die");
                }

                // Player
                var playerMovement = GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.enabled = false;
                }

                // Inimigo
                var enemyPatrol = GetComponentInParent<EnemyPatrol>();
                if (enemyPatrol != null)
                {
                    enemyPatrol.enabled = false;
                }

                var skull = GetComponent<Skull>();
                if (skull != null)
                {
                    skull.enabled = false;
                }

                dead = true;

                // Chama o método para mudar a cena após 2 segundos
                StartCoroutine(LoadGameOverSceneAfterDelay(2f));




            }

        }
    }

    private IEnumerator LoadGameOverSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Substitua "GameOver" pelo nome ou índice da sua cena
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }




}
