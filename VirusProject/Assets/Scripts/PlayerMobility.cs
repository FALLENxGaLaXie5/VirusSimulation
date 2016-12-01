/**
    Virus Simulation Project - Software Engineering Comp 350
    PlayerMobility.cs
    Purpose: Player movement code. Uses physics processing to add force vectors and change velocities using these calculations.

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

public class PlayerMobility : MonoBehaviour
{

    //Speed that is applied to force for player physics
    public float speed;

    //original speed is manipulated - hence copy is stored
    private float speedCopy;

    //Increased speed for running capabilities
    public float increasedSpeed;

    //Reference for animator component - will control player animations
    Animator animator;

    //Position of target - usually the mouse cursor
    private Vector3 targetPosition;

    //Flag for if player is moving
    private bool isMoving;

    //Reference the Rigid Body of player - hub for the player physics, which will be used to add force and change velocity on player.
    Rigidbody2D playerObject;

    //Input constant
    const int BUTTON_FOR_MOVEMENT = 1;


    /**
        Initialization Function. Initializes global variables. Similar to constructors.
    */
    void Start()
    {
        animator = GetComponent<Animator>();
        playerObject = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
        isMoving = false;
        speedCopy = speed;
    }

    /**
        Detects if an AI is close enough to player to detect him. If so, puts AI into detection state, and clears destinations.
        
        @param other The 2D collider that is attached to another gameObject that collides with this game object
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "AI")
        {
            simPerson person = other.gameObject.GetComponent<simPerson>();
            person.myState = simPerson.State.Detection;
            person.myDestinations.Clear();
        }
    }

    /**
        Update is called at the beginning of every frame at run time.
        This means that all runnable code is ran at one point or another from here.
        Similar to main or runnable with frame by frame implementation.
        Here, update will be used to get input, and if specific input is inputted, it will set the 
            target position for the player to move to, along with increased or decreased speeds and changes
            in the current animations.
    */
    void Update()
    {      
        if((Input.GetMouseButton(BUTTON_FOR_MOVEMENT) && Input.GetAxis("Fire3") > 0) || (Input.GetKey("w") && Input.GetAxis("Fire3") > 0))
        {
            speed = increasedSpeed;
            SetTargetPosition();
            animator.SetBool("idle", false);
            animator.SetBool("running", true);
        }
        else if (Input.GetMouseButton(BUTTON_FOR_MOVEMENT) || Input.GetKey("w"))
        {
            speed = speedCopy;
            SetTargetPosition();
            animator.SetBool("idle", false);
            animator.SetTrigger("slowing");
            animator.SetBool("running", false);
        }
        else
        {
            speed = speedCopy;
            playerObject.velocity = Vector2.zero;
            animator.SetBool("idle", true);
            animator.SetBool("running", false);
        }
    }

    /**
        Like update, FixedUpdate is called every frame. However, it is called at the END
            of every frame, which is where physics calculations are generally made. 
            Here, we check if isMoving is flagged. If so, we call moveplayer.
    */
    void FixedUpdate()
    {
        if (isMoving)
        {
            MovePlayer();
        }
    }

    /**
        Sets target position global to where, from the camera's perspective, the mouse cursor's
            position is.
            Sets isMoving flag.
    */
    void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isMoving = true;
    }

    /**
        Calculates a normalized direction based off of the target position's coordinates. 
        Performs physics calculations - adds force with param DIRECTION * SPEED. 
        Add force takes into account the 1) Mass on the rigid body and 2) The linear and angular velocities on the rigid bodies
    */
    void MovePlayer()
    {
        Vector2 direction = (targetPosition - transform.position).normalized;
        playerObject.AddForce(direction * speed);

        Debug.DrawLine(transform.position, targetPosition, Color.red);
    }

}