using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * This class represents simple player movement actions (monitors movement to the left,right,back,forward and places the player 1 tile ahead) and position monitoring.  
 * @author Noy Ohana
 * @since 2021-11
 */
public class Keyboardmover_q5 : MonoBehaviour
{
    protected Vector3 saveStep; //the direction the player faces
    protected Vector3 currPosition; //stores current player position
    protected Vector3 NewPosition() //checks everytime if the player went left,right,back,forward and places the player there.
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            saveStep = Vector3.left;
            currPosition = transform.position + saveStep;
            return currPosition;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            saveStep = Vector3.right;
            currPosition = transform.position + saveStep;
            return currPosition;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            saveStep = Vector3.down;
            currPosition = transform.position + saveStep;
            return currPosition;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            saveStep = Vector3.up;
            currPosition = transform.position + saveStep;
            return currPosition;
        }
        else
        {
            currPosition = transform.position;
            return transform.position;
        }
    }


    void Update()
    {
        transform.position = NewPosition(); //monitors player position
    }
}
