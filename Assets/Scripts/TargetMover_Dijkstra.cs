using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component moves its object towards a given target position.
 */
public class TargetMover_Dijkstra : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f;

    [SerializeField] int weight = 1;

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld;

    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid;

    protected bool atTarget;  // This property is set to "true" whenever the object has already found the target.

    public void SetTarget(Vector3 newTarget)
    {
        if (targetInWorld != newTarget)
        {
            targetInWorld = newTarget;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
        }
    }

    public Vector3 GetTarget()
    {
        return targetInWorld;
    }

    private TileMapDijkstra tilemapDijkstra = null;
    private float timeBetweenSteps;


    protected virtual void Start()
    {
        tilemapDijkstra = new TileMapDijkstra(tilemap, allowedTiles.Get(), weight); //get the shortest path to the target (player usually, unless defined otherwise)
        timeBetweenSteps = 1 / speed; //sets speed of moving
        StartCoroutine(MoveTowardsTheTarget());
    }

    IEnumerator MoveTowardsTheTarget() //moves toward the target in controlled time as long as the game works
    {
        for (; ; )
        {
            yield return new WaitForSeconds(timeBetweenSteps);
            if (enabled && !atTarget)
                MakeOneStepTowardsTheTarget();
        }
    }

    private void MakeOneStepTowardsTheTarget() //gets to the player
    {
        Vector3Int startNode = tilemap.WorldToCell(transform.position); //current start position
        Vector3Int endNode = targetInGrid; //player current position
        List<Vector3Int> shortestPath = Dijkstra.GetPath(tilemapDijkstra, startNode, endNode); //get shortest path using Dijkstra algorithm
        //Debug.Log("shortestPath = " + string.Join(" , ", shortestPath)); outputs the shortest path
        if (shortestPath.Count >= 2) //as long as you are not on the player tile
        {
            Vector3Int nextNode = shortestPath[1]; 
            transform.position = tilemap.GetCellCenterWorld(nextNode); //set the next position to nextNode
        }
        else
        {
            atTarget = true; //you are at the target
        }
    }
}
