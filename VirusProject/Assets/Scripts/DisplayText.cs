/**
Virus Simulation Project - Software Engineering Comp 350
DisplayText.cs
Purpose: Debugging purposes; calls destinationTalk.cs script 
    to display graphical destination data.

@author Joshua Steward
@author Zane Gittins
@author Zachery Van Es
@author Lowell Batacan
@author Jessica Perez
@author Andrew Walters

@version 1.0 11/7/2016
*/

using UnityEngine;
using System.Collections;

public class DisplayText : MonoBehaviour
{
    //tag for destination. References from unity editor
    public string destinationTag;

    //range to set tag as visible
    public float range;

    //list of destination buildings
    GameObject[] destinations;
    
    
    /**
    Initialization Function. Initializes global variables. Similar to constructors.
    */
    void Start ()
    {
        destinations = GameObject.FindGameObjectsWithTag(destinationTag);
	}


    /**
    Update is called at the beginning of every frame at run time.
    This means that all runnable code is ran at one point or another from here.
    Similar to main or runnable with frame by frame implementation.
    Update will check if near a destination every frame.
    */
    void Update()
    {
        checkIfNearDestination();
    }


    /**
    Iterates through destinations, and checks if eaech is close enough to player to call the talk script, and display text for it.
    */
    void checkIfNearDestination()
    {
        for (int i = 0; i < destinations.Length; i++)
        {
            GameObject currentDestination = destinations[i];
            float distance = Vector3.Distance(transform.position, currentDestination.transform.position);
            if (distance < range)
            {
                destinationTalk talkScript = currentDestination.GetComponent<destinationTalk>();
                talkScript.talk = true;
            }
            else
            {
                destinationTalk talkScript = currentDestination.GetComponent<destinationTalk>();
                talkScript.talk = false;
            }
        }
    }
}
