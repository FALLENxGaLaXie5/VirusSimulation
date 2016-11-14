/**
        Virus Simulation Project - Software Engineering Comp 350
        destinationTalk.cs
        Purpose: Mainly used for debugging purposes. Will display text associated with different types of destinations.

        @author Joshua Steward
        @version 1.0 11/7/2016
*/

using UnityEngine;
using System.Collections;

public class destinationTalk : MonoBehaviour
{
    //flag for whether this destination building will show text
    public bool talk;
    
    //text game object
    TextMesh text;

    //text reference
    GameObject text1;

    /**
        Initialization Function. Initializes global variables. Similar to constructors.
    */
    void Start ()
    {
        text = gameObject.GetComponentInChildren(typeof(TextMesh)) as TextMesh;
        talk = false;
    }

    /**
        Update is called at the beginning of every frame at run time.
        This means that all runnable code is ran at one point or another from here.
        Similar to main or runnable with frame by frame implementation.
        Sets destination debugging code.
    */
    void Update ()
    {
        if(text != null)
        {
            if (text.gameObject.tag == "coffeeTag" && talk)
            {
                text.text = "I'm a coffee shop!";
            }
            else if (text.gameObject.tag == "foodTag" && talk)
            {
                text.text = "I gotz food for ya!";
            }
            else if(text.gameObject.tag == "schoolTag" && talk)
            {
                text.text = "School time bois and gurls!";
            }
            else if(text.gameObject.tag == "churchTag" && talk)
            {
                text.text = "Come get ya Jesus!";
            }
            else if (text.gameObject.tag == "clubTag" && talk)
            {
                text.text = "UNGST UNGST UNGST UNGST UNGST UNGST UNGST";
            }
            else if (text.gameObject.tag == "frysElectronicsTag" && talk)
            {
                text.text = "Electronics for everyone!";
            }
            else if (text.gameObject.tag == "homeTag" && talk)
            {
                text.text = "Finally home...";
            }
            else if (text.gameObject.tag == "gameStopTag" && talk)
            {
                text.text = "ALL DA GAMES!";
            }
            else if (talk == false)
            {
                text.text = "";
            }
        }
	}
}
