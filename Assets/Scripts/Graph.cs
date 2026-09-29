using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public Dictionary<Vector2, List<Vector2>> adjList;
    public Dictionary<Vector2, GameObject> vertexObjects; // Store the GameObject reference

    public Graph()
    {
        adjList = new Dictionary<Vector2, List<Vector2>>();
        vertexObjects = new Dictionary<Vector2, GameObject>();
    }

    public void AddVertex(Vector2 vertex, GameObject obj)
    {
        if (!adjList.ContainsKey(vertex))
        {
            adjList[vertex] = new List<Vector2>();
            vertexObjects[vertex] = obj;
        }
    }

    public void AddEdge(Vector2 vertex1, Vector2 vertex2)
    {
        if (adjList.ContainsKey(vertex1) && adjList.ContainsKey(vertex2))
        {
            adjList[vertex1].Add(vertex2);
            adjList[vertex2].Add(vertex1);

            // Output the names of the parent GameObjects
            Debug.Log($"Added Edge: {vertexObjects[vertex1].name} -> {vertexObjects[vertex2].name}");
        }
    }

    public Vector2 FindNearestVertex(Vector2 position, float threshold = 0.1f)
    {
        Vector2 nearestVertex = position;
        float minDistance = float.MaxValue;

        foreach (Vector2 vertex in adjList.Keys)
        {
            float distance = Vector2.Distance(position, vertex);
            if (distance < minDistance && distance <= threshold)
            {
                nearestVertex = vertex;
                minDistance = distance;
            }
        }

        if (minDistance == float.MaxValue)
        {
            Debug.LogError($"No vertex found near position: {position} within threshold: {threshold}");
        }

        return nearestVertex;
    }

    public List<Vector2> BFS(Vector2 start, Vector2 goal)
    {
        start = FindNearestVertex(start, 1f);
        goal = FindNearestVertex(goal, 1f);

        Debug.Log($"Starting BFS from {start} to {goal}");

        if (!adjList.ContainsKey(start) || !adjList.ContainsKey(goal))
        {
            Debug.LogError($"Start or Goal vertex not found in graph. Start: {start}, Goal: {goal}");
            return null;
        }

        Queue<Vector2> queue = new Queue<Vector2>();
        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();
        HashSet<Vector2> visited = new HashSet<Vector2>();

        queue.Enqueue(start);
        visited.Add(start);
        cameFrom[start] = start;

        while (queue.Count > 0)
        {
            Vector2 current = queue.Dequeue();
            Debug.Log($"Visiting: {current}");

            if (current == goal)
            {
                break;
            }

            foreach (Vector2 neighbor in adjList[current])
            {
                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                    cameFrom[neighbor] = current;
                    Debug.Log($"Queueing: {neighbor}");
                }
            }
        }

        if (!cameFrom.ContainsKey(goal))
        {
            Debug.LogError("Goal not reached, no path found.");
            return null;
        }

        List<Vector2> path = new List<Vector2>();
        Vector2 step = goal;

        while (!step.Equals(start))
        {
            path.Add(step);
            step = cameFrom[step];
        }
        path.Add(start);
        path.Reverse();

        return path;
    }
}