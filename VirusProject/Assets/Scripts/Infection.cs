/**
    Virus Simulation Project - Software Engineering Comp 350
    Infection.cs
    Purpose: Detects and triggers when trigger-marked colliders are within range.
    Generates probability which is used to infect the game object attached to other collider.
    Sets infected flags. Will be attached to all AI for potential infection.

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
using System.Collections.Generic;

public class Infection : MonoBehaviour
{
    //GameObject reference to the player object
    // - will be used reference player states and positioning compared to AI objects
    public GameObject playerReference;

    //Infected status indicator
    public bool infected;

    //Current color reference of the AI - used for debugging infection purposes
    public Color currentColor;

    //reference for static GameManager
    GameManager gameManagerInstance;

    //R Color indicators - used for debugging infection state
    public float R;

    //G Color indicators - used for debugging infection state
    public float G;

    //B Color indicators - used for debugging infection state
    public float B;

    //A Color indicators - used for debugging infection state
    public float A;

    //Child and renderer - the AI animations and materials are set on a child of the actual AI object
    // - therefore, these must be referenced
    private GameObject child;

    //renderer reference
    private SpriteRenderer renderer;


    /**
        Detects and triggers when trigger-marked colliders are within range.
        Generates probability which is used to infect the game object attached to other collider.
        Sets infected to true, and uses debugging tool.

        @param other The 2D collider that is attached to another gameObject that collides with this game object
    */
    void OnTriggerEnter2D(Collider2D other)
    { 
        int prob = Random.Range(0, 100);
        if(infected && other.gameObject.tag == "AI" && prob <= 10)
        {
            Infection inf = other.gameObject.GetComponent<Infection>();
            inf.infected = true;
            Debug.Log("AI INFECTED AI!");
        }
    }

    /**
        Initialization Function. Initializes global variables. Similar to constructors.
    */
    void Start ()
    {
        gameManagerInstance = GameManager.instance;
        playerReference = GameObject.FindGameObjectWithTag("wallCheck");
        infected = false;
        child = gameObject.transform.GetChild(0).gameObject;
        renderer = child.GetComponent<SpriteRenderer>();
     }

    /**
        Update is called at the beginning of every frame at run time.
        This means that all runnable code is ran at one point or another from here.
        Similar to main or runnable with frame by frame implementation.
    */
    void Update ()
    {
        progressInfection();
	}

    /**
    Checks if distance from player is sufficient to be infected.
        If true, global infection for this object is flagged. Called from OnMouseDown.
    */
    void checkForInfection()
    {
        if(Vector3.Distance(transform.position, playerReference.transform.position) <= 2.0)
        {
            infected = true;
        }
    }

    /**
    Triggered when the mouse is clicked. If infected is not flagged, will call checkForInfection.
    */
    void OnMouseDown()
    {
        if(!infected)
        {
            checkForInfection();
        }
    }

    /**
    Called from update. If infected is flagged, will progress visual infection using
        RGBA color values. When infection reaches a certain point, objecet is destroyed.
    */
    void progressInfection()
    {
        if (infected)
        {
            //if they are infected, progress infection
            renderer.color = Color.Lerp(renderer.color, Color.red, 0.001f);
            R = renderer.color.r;
            G = renderer.color.g;
            B = renderer.color.b;
            A = renderer.color.a;
            if (G <= 0.05)
            {
                gameManagerInstance.aiList.Remove(gameObject);
                Destroy(gameObject);
            } 
        }
    }
}