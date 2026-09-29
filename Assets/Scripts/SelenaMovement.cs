using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelenaMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform playerTransform;
    [SerializeField] float attackDistance = 1.0f; // Distância para ativar a animação de ataque
    Rigidbody2D myRigidBody2D;
    Animator myAnimator;

    Transform target;
    Grid grid;
    Pathfinding pathfinding;
    List<PathNode> path;
    Vector3 originalScale;
    bool isFollowing;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        grid = new Grid(20, 10, 1f, new Vector3(-10, -5));
        pathfinding = new Pathfinding(grid);
        target = null; // Não seguirá nenhum inimigo inicialmente
        originalScale = transform.localScale; // Armazena a escala original
        isFollowing = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isFollowing)
            {
                target = FindClosestEnemy();
                if (target != null)
                {
                    isFollowing = true;
                    StartCoroutine(UpdatePath());
                    Debug.Log("Following enemy: " + target.name);
                }
            }
            else
            {
                StopCoroutine(UpdatePath());
                isFollowing = false;
            }
        }

        if (isFollowing && path != null && path.Count > 0)
        {
            FollowPath();
        }
        FlipSprite();
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
        return closestEnemy;
    }

    IEnumerator UpdatePath()
    {
        while (isFollowing)
        {
            if (target != null)
            {
                Vector3 targetPosition = target.position;
                grid.GetXY(targetPosition, out int targetX, out int targetY);

                Vector3 selenaPosition = transform.position;
                grid.GetXY(selenaPosition, out int startX, out int startY);

                path = pathfinding.FindPath(startX, startY, targetX, targetY);
                if (path != null && path.Count > 0)
                {
                    Debug.Log("Path found with " + path.Count + " nodes.");
                }
                else
                {
                    Debug.Log("No valid path found.");
                }
            }
            yield return new WaitForSeconds(1f); // Atualiza o caminho a cada 1 segundo
        }
    }

    void FollowPath()
    {
        if (path == null || path.Count == 0) return;

        PathNode currentNode = path[0];
        Vector3 targetPosition = new Vector3(currentNode.x, currentNode.y) * grid.GetCellSize() + grid.GetOriginPosition() + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * 0.5f;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            path.RemoveAt(0);
        }

        if (target != null && Vector3.Distance(transform.position, target.position) < attackDistance)
        {
            myAnimator.SetTrigger("attack");
        }
        else
        {
            myAnimator.SetBool("Running", path.Count > 0);
        }
    }

    void FlipSprite()
    {
        if (target != null)
        {
            if (target.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }
        }
    }
}
