/**
        Virus Simulation Project - Software Engineering Comp 350
        CameraScript.cs
        Purpose: To attach camera object to player object without altering camera rotation when input is redirected.

        @author Joshua Steward
        @version 1.0 11/7/2016
*/

using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    //target will be the player position
    public Transform target;

    //distance from player
    public float distance;

    /**
        Update is called at the beginning of every frame at run time.
        This means that all runnable code is ran at one point or another from here.
        Similar to main or runnable with frame by frame implementation.
        Update will change the camera's position to reflect the player's position
            WITHOUT reflecting the player's rotation and MAINTAINING a constant z offset.
    */
    void Update ()
    {
        transform.position = new Vector3(target.position.x, target.position.y, target.position.z - distance);
	}
}
