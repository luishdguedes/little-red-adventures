using System.Collections.Generic;
using UnityEngine;

public class GraphInitializer : MonoBehaviour
{
    public Graph graph;
    public float connectionRadius = 1f; // Radius within which vertices will be connected
    public LayerMask vertexLayer; // LayerMask for vertices

    void Start()
    {
        graph = new Graph();
    }

    public void InitializeGraph()
    {
        // Find all objects on the "Vertices" layer
        Collider2D[] vertexColliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity, vertexLayer);
        List<Vector2> vertices = new List<Vector2>();

        // Add each vertex to the graph
        foreach (Collider2D collider in vertexColliders)
        {
            Vector2 position = collider.transform.position;
            vertices.Add(position);
            graph.AddVertex(position, collider.gameObject);
            Debug.Log($"Added Vertex: {collider.gameObject.name} at {position}");
        }

        // Manually add edges based on proximity
        for (int i = 0; i < vertices.Count; i++)
        {
            Vector2 position1 = vertices[i];
            for (int j = i + 1; j < vertices.Count; j++)
            {
                Vector2 position2 = vertices[j];
                if (Vector2.Distance(position1, position2) <= connectionRadius)
                {
                    graph.AddEdge(position1, position2);
                }
            }
        }
    }
}