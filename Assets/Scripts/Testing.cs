using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    private Grid grid;
    private Pathfinding pathfinding;
    public Transform characterTransform;  // Referência ao transform do personagem

    private void Start(){
        grid = new Grid(20, 15, 1f, new Vector3(-10, -5));
        pathfinding = new Pathfinding(grid);

        
    }

    private void Update(){
        if (Input.GetMouseButtonDown(0)){
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            grid.GetXY(mousePosition, out int x, out int y);
            grid.SetValue(x, y, grid.GetValue(x, y) + 5);
        }

        if(Input.GetMouseButtonDown(1)){
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            grid.GetXY(mousePosition, out int x, out int y);
            Debug.Log(grid.GetValue(x, y));
        }

        if(Input.GetMouseButtonDown(2)){
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            grid.GetXY(mousePosition, out int targetX, out int targetY);

            Vector3 characterPosition = characterTransform.position;
            grid.GetXY(characterPosition, out int startX, out int startY);

            List<PathNode> path = pathfinding.FindPath(startX, startY, targetX, targetY);
            if (path != null) {
                StartCoroutine(FollowPath(path));
            }
        }
    }

    private IEnumerator FollowPath(List<PathNode> path){
        foreach (PathNode node in path){
            Vector3 targetPosition = new Vector3(node.x, node.y) * grid.GetCellSize() + grid.GetOriginPosition() + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * 0.5f;
            while (Vector3.Distance(characterTransform.position, targetPosition) > 0.1f) {
                characterTransform.position = Vector3.MoveTowards(characterTransform.position, targetPosition, 5f * Time.deltaTime);
                yield return null;
            }
        }
    }

    public Grid GetGrid(){
        return grid;
    }
}