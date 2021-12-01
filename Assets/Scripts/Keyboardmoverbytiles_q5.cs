using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This class represents player movement across tiles and mining specific tiles.
 * This class inherits all data from Keyboardmover_q5 (and then obviously from MonoBehaviour)
 * @author Noy Ohana
 * @since 2021-11
 */
public class Keyboardmoverbytiles_q5 : Keyboardmover_q5
{
    [SerializeField] Tilemap tilemap;
    [Tooltip("The tilemap itself")]
    [SerializeField] AllowedTiles allowedTiles;
    [Tooltip("Tiles that the player allowed to be on")]
    [SerializeField] TileBase[] AllowedCut;
    [Tooltip("What tiles are allowed to be cutted")]
    [SerializeField] TileBase afterCut;
    [Tooltip("The tile that would be in mining position after the mining")]
    [SerializeField] float delay = 1f;
    [Tooltip("The amount of delay in seconds for mining")]
    [SerializeField] KeyCode MineKey = KeyCode.X;
    [Tooltip("The key which will be used for mining (default is X)")]
    private float curr = 0f;
    private bool flag = false;


    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()
    {
        Vector3 newPosition = NewPosition(); //checks everytime if the player went left,right,back,forward and places the player there
        TileBase tileOnNewPosition = TileOnPosition(newPosition); //gets the tile of the position
        if (allowedTiles.Contain(tileOnNewPosition)) //checks if this tile is an allowed movement, if so, it places the controlled player there
        {
            transform.position = newPosition;
        }
        else
        {
            //Debug.Log("You cannot walk on " + tileOnNewPosition + "!"); else a debug message pops up in console and the player doesnt move there 
        }
        if (Input.GetKeyDown(MineKey))
        {
             TileBase tileOnDirPosition = TileOnPosition(transform.position + saveStep); //checks the tile of the direction the player is facing
            if (Time.time > curr + delay) //check if delay was completed (between Minings).
            {
                curr = Time.time;
                flag = true;
            }
            else
            {
                flag = false;
            }

            if (AllowedCut.Contains(tileOnDirPosition)&&flag) //if you can mine the current tile
            {
                Vector3 playerPos = transform.position + saveStep; //save the tile position using the player position and saveStep
                tilemap.SetTile(tilemap.WorldToCell(playerPos), afterCut); //and replace the tile with the new tile
            }

            flag = false; //for the next frame, flag=false as default.
        }

        
    }




}

