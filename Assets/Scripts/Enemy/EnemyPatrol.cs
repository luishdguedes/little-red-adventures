using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;

    [Header ("Paremetros para movimentacao")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    
    [Header ("Idle time")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header ("Enemy Animator")]
    [SerializeField] private Animator animator;

    private void Awake(){
        if (enemy != null)
        {
            initScale = enemy.localScale;
        }
    }

    private void OnDisable(){
        if (animator != null)
        {
            animator.SetBool("moving", false);
        }
    }

    private void Update(){
        if (enemy == null || leftEdge == null || rightEdge == null)
        {
            return; // Não faça nada se qualquer um dos objetos necessários estiver faltando
        }

        if(movingLeft){
            if(enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else{
                DirectionChange();
            }

        }else{
            if(enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else{
                DirectionChange();
            }
        }
        
    }

    private void DirectionChange(){
        if (animator != null)
        {
            animator.SetBool("moving", false);
        }

        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration){
            movingLeft = !movingLeft;
        }

        
    }

    private void MoveInDirection(int _direction){
        if (enemy == null)
        {
            return;
        }

        idleTimer = 0;
        if (animator != null)
        {
            animator.SetBool("moving", true);
        }

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
}

