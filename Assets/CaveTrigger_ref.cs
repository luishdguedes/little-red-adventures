using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveTrigger_ref : MonoBehaviour
{
    public GraphInitializer graphInitializer; // Reference to the GraphInitializer
    public EnemyController enemyController; // Reference to the EnemyController

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger Entered");

            // Start the graph initialization and enemy movement
            if (graphInitializer != null)
            {
                graphInitializer.InitializeGraph();
                enemyController.StartPathfinding(graphInitializer.graph);
                Debug.Log("Graph and Enemy Pathfinding Started");
            }
        }
    }
}
