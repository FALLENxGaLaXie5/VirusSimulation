/**
    Virus Simulation Project - Software Engineering Comp 350
    aiAnim.cs
    Purpose: Initialization Function. Initializes global variables. Similar to constructors.

    @author Joshua Steward
    @version 1.0 11/7/2016
*/

using UnityEngine;
using System.Collections;

public class aiAnim : MonoBehaviour
{
    //Animator attached to gameobject
    Animator anim;
    
    //simPerson program attached to gameObject
    simPerson sim;


    /**
        Virus Simulation Project - Software Engineering Comp 350
        aiAnim.cs
        Purpose: Initialization Function. Initializes global variables. Similar to constructors.

        @author Joshua Steward
        @version 1.0 11/7/2016
    */
    void Start ()
    {
        anim = GetComponent<Animator>();
        sim = GetComponentInParent<simPerson>();
	}

    /**
        Virus Simulation Project - Software Engineering Comp 350
        aiAnim.cs
        Purpose: Update is called at the beginning of every frame at run time.
        This means that all runnable code is ran at one point or another from here.
        Similar to main or runnable with frame by frame implementation.
        Update will check for the current state every frame.

        @author Joshua Steward
        @version 1.0 11/7/2016
    */
    void Update ()
    {
        checkForState();
	}

    /**
        Virus Simulation Project - Software Engineering Comp 350
        aiAnim.cs
        Purpose: Essentially, if the state is walking, we set the animator to walk. If state is detection, we tell animator to run.

        @author Joshua Steward
        @version 1.0 11/7/2016
    */
    void checkForState()
    {
        if (sim.myState == simPerson.State.Walking)
        {
            anim.SetBool("running", false);
        }
        else if (sim.myState == simPerson.State.Detection)
        {
            anim.SetBool("running", true);
        }
    }
}
