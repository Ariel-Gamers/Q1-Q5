using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A graph that represents a tilemap, using only the allowed tiles.
 */
public class TileMapDijkstra : WGraph<Vector3Int>
{
    private Dictionary<TileBase, int> w; //map of tilebases with weights
    private Tilemap tilemap;
    private TileBase[] allowedTiles;

    //same weights
    public TileMapDijkstra(Tilemap tilemap1, TileBase[] allowedTiles1,int weight) 
    {
        this.tilemap = tilemap1;
        this.allowedTiles = allowedTiles1;
        w = new Dictionary<TileBase, int>();
        for (int i = 0; i < allowedTiles.Length; i++)
        {
            w.Add(allowedTiles[i], weight);
        }
    }

    //diffrent weights
    public TileMapDijkstra(Tilemap tilemap1, TileBase[] allowedTiles1, int[] weights)
    {
        this.tilemap = tilemap1;
        this.allowedTiles = allowedTiles1;
        w = new Dictionary<TileBase, int>();
        for (int i = 0; i < allowedTiles.Length; i++)
        {
            w.Add(allowedTiles[i], weights[i]);
        }
    }

    public int getW(Vector3Int node) //returns a given node weight
    {
        TileBase tb = tilemap.GetTile(node);
        return w[tb];
    }

    static Vector3Int[] directions = { //represents directions for all steps
            new Vector3Int(-1, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(0, 1, 0),
    };

    public IEnumerable<Vector3Int> Neighbors(Vector3Int node)
    {
        foreach (var direction in directions)
        {
            Vector3Int neighborPos = node + direction; //checks where the enemy faces
            TileBase neighborTile = tilemap.GetTile(neighborPos); //gets the needed tile
            if (allowedTiles.Contains(neighborTile)) //checks the allowed neighbors
                yield return neighborPos; //and returns them
        }
    }
}
