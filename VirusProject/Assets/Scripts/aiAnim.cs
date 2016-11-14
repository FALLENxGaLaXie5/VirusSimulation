/**
    Virus Simulation Project - Software Engineering Comp 350
    aiAnim.cs
    Purpose: Initialize animators and control transitions between animations using booleans and triggers.

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
        Initialization Function. Initializes global variables; similar to constructors.
    */
    void Start ()
    {
        anim = GetComponent<Animator>();
        sim = GetComponentInParent<simPerson>();
	}

    /**
        Update is called at the beginning of every frame at run time.
        This means that all runnable code is ran at one point or another from here.
        Similar to main or runnable with frame by frame implementation.
        Update will check for the current state every frame.
    */
    void Update ()
    {
        checkForState();
	}

    /**
        Essentially, if the state is walking, we set the animator to walk. If state is detection, we tell animator to run.
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
