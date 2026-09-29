using System.Collections.Generic;
using UnityEngine;

public class Pathfinding {
    private Grid grid;
    private PathNode[,] pathNodeArray;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    public Pathfinding(Grid grid){
        this.grid = grid;
        CreatePathNodeArray();
    }

    private void CreatePathNodeArray(){
        pathNodeArray = new PathNode[grid.GetWidth(), grid.GetHeight()];
        for (int x = 0; x < grid.GetWidth(); x++){
            for (int y = 0; y < grid.GetHeight(); y++){
                pathNodeArray[x, y] = new PathNode(x, y);
            }
        }
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY){
        PathNode startNode = pathNodeArray[startX, startY];
        PathNode endNode = pathNodeArray[endX, endY];

        Debug.Log($"Finding path from ({startX}, {startY}) to ({endX}, {endY})");

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++){
            for (int y = 0; y < grid.GetHeight(); y++){
                PathNode pathNode = pathNodeArray[x, y];
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0){
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode){
                Debug.Log("Path found");
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighborNode in GetNeighborList(currentNode)){
                if (closedList.Contains(neighborNode)) continue;
                if (!grid.IsWalkable(neighborNode.x, neighborNode.y)){
                    closedList.Add(neighborNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighborNode);
                if (tentativeGCost < neighborNode.gCost){
                    neighborNode.cameFromNode = currentNode;
                    neighborNode.gCost = tentativeGCost;
                    neighborNode.hCost = CalculateDistanceCost(neighborNode, endNode);
                    neighborNode.CalculateFCost();

                    if (!openList.Contains(neighborNode)){
                        openList.Add(neighborNode);
                    }
                }
            }
        }

        Debug.Log("No valid path found");
        return null;
    }

    private List<PathNode> GetNeighborList(PathNode currentNode){
        List<PathNode> neighborList = new List<PathNode>();

        if (currentNode.x - 1 >= 0){
            neighborList.Add(pathNodeArray[currentNode.x - 1, currentNode.y]);
            if (currentNode.y - 1 >= 0) neighborList.Add(pathNodeArray[currentNode.x - 1, currentNode.y - 1]);
            if (currentNode.y + 1 < grid.GetHeight()) neighborList.Add(pathNodeArray[currentNode.x - 1, currentNode.y + 1]);
        }
        if (currentNode.x + 1 < grid.GetWidth()){
            neighborList.Add(pathNodeArray[currentNode.x + 1, currentNode.y]);
            if (currentNode.y - 1 >= 0) neighborList.Add(pathNodeArray[currentNode.x + 1, currentNode.y - 1]);
            if (currentNode.y + 1 < grid.GetHeight()) neighborList.Add(pathNodeArray[currentNode.x + 1, currentNode.y + 1]);
        }
        if (currentNode.y - 1 >= 0) neighborList.Add(pathNodeArray[currentNode.x, currentNode.y - 1]);
        if (currentNode.y + 1 < grid.GetHeight()) neighborList.Add(pathNodeArray[currentNode.x, currentNode.y + 1]);

        return neighborList;
    }

    private List<PathNode> CalculatePath(PathNode endNode){
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null){
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b){
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return 14 * Mathf.Min(xDistance, yDistance) + 10 * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList){
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++){
            if (pathNodeList[i].fCost < lowestFCostNode.fCost){
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}