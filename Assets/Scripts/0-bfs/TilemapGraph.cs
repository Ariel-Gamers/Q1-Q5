using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

/**
 * A graph that represents a tilemap, using only the allowed tiles.
 */
public class TilemapGraph: IGraph<Vector3Int> {
    private Tilemap tilemap;
    private TileBase[] allowedTiles;

    public TilemapGraph(Tilemap tilemap, TileBase[] allowedTiles) {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
    }

    static Vector3Int[] directions = { //represents directions for all steps
            new Vector3Int(-1, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(0, 1, 0),
    };

    public IEnumerable<Vector3Int> Neighbors(Vector3Int node) {
        foreach (var direction in directions) {
            Vector3Int neighborPos = node + direction; //checks where the enemy faces
            TileBase neighborTile = tilemap.GetTile(neighborPos); //gets the needed tile
            if (allowedTiles.Contains(neighborTile)) //checks the allowed neighbors
                yield return neighborPos; //and returns them
        }
    }

    public static implicit operator TilemapGraph(TileMapDijkstra v)
    {
        throw new NotImplementedException();
    }
}
