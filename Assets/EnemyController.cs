using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float attackRange = 1f; // Alcance de ataque
    public float stopDistance = 1.0f; // Distância para parar de se mover antes de atacar
    public LayerMask attackMask;
    public Vector3 attackOffset;

    private Graph graph;
    private List<Vector2> path;
    private int currentStep = 0;
    private bool pathfindingActive = false;
    private Animator animator;
    private bool isPlayerInRange = false;
    private Health playerHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = player.GetComponent<Health>(); // Obtém o componente Health do jogador
    }

    public void StartPathfinding(Graph initializedGraph)
    {
        graph = initializedGraph;
        pathfindingActive = true;
    }

    void Update()
    {
        if (!pathfindingActive || graph == null)
        {
            return;
        }

        // Virar o inimigo na direção do jogador
        FlipTowardsPlayer();

        // Verifica se o inimigo deve parar de se mover antes de atacar
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= stopDistance)
        {
            return; // Sai da função Update para não continuar movendo
        }

        Vector2 start = graph.FindNearestVertex(transform.position, 20f);
        Vector2 goal = graph.FindNearestVertex(player.position, 20f);

        path = graph.BFS(start, goal);

        if (path == null || path.Count == 0)
        {
            Debug.LogError("Path is null or empty after BFS");
            return;
        }

        if (currentStep < path.Count)
        {
            Vector2 target = path[currentStep];
            Vector2 newPosition = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            transform.position = newPosition;

            if (Vector2.Distance(transform.position, target) < 0.01f)
            {
                currentStep++;
            }
        }

        // Verifica se o jogador está dentro do alcance de ataque
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null && colInfo.CompareTag("Player"))
        {
            isPlayerInRange = true;
            AttackPlayer();
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    void AttackPlayer()
    {
        // Ativa a animação de ataque
        animator.SetTrigger("attack");
    }

    // Método a ser chamado pelo evento de animação
    void ApplyDamage()
    {
        if (isPlayerInRange && playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }
    }

    void FlipTowardsPlayer()
    {
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }
        else
        {
            transform.localScale = new Vector3(3, 3, 3);
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