// Player Mobility
// Author -Zane Gittins
// Top down movement script for Unity3D. 
// Needs to have a lot of code cut out so that it can be used for the current project. 
// Can cut out all the charge, and slash functionality. 


using UnityEngine;
using System.Collections;

public class PlayerMobilityRay : MonoBehaviour
{
    public GameObject wallCheck;


    public float speed;
    public float speedCopy;

    public float zeroSpeed = 0;
    public float actualSpeed;
    private float speedHorRight;
    private float speedHorLeft;
    private float speedVertUp;
    private float speedVertDown;

    public bool againstWallVertUp = false;
    public bool againstWallVertDown = false;
    public bool againstWallHorRight= false;
    public bool againstWallHorLeft = false;
    public bool againstWallUpRight = false;
    public bool againstWallUpLeft = false;
    public bool againstWallDownRight = false;
    public bool againstWallDownLeft = false;

    private Rigidbody PlayerObject;
    private Vector2 speedObj;
    private Vector2 velocity;

    
    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "wall" || other.tag == "dest")
        {
            //PlayerObject.AddForce(-1f * PlayerObject.velocity);
            //other.attachedRigidbody.AddForce(-0.1 * other.attachedRigidbody.velocity);
            speed *= -1;
            Debug.Log("Speed is now 0!");
        }
    }
    */
    
    /*    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "wall" || other.tag == "dest")
        {
            speed = speedCopy;
            Debug.Log("Speed is now " + speed);
        }
    }
    */

    /*
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "wall" || other.tag == "dest")
        {
            speed *= -1;
            //PlayerObject.AddForce(-1f * PlayerObject.velocity);
            Debug.Log("Get out of my house!");
        }
        //Rigidbody2D otherBody = other.GetComponent<Rigidbody2D>
        //other.attachedRigidbody.AddForce(-0.1 * other.attachedRigidbody.velocity);
    }
    */
    void Start ()
    {
        //wallCheck = GameObject.FindGameObjectWithTag("wallCheck");
        //actualSpeed = speed;

        //speedHorRight = speed;
        //speedHorLeft = speed;
        //speedVertUp = speed;
        //speedVertDown = speed;

        //speedCopy = speed;

        PlayerObject = GetComponent<Rigidbody>();
        //speedObj = new Vector2(speed, speed);
        //velocity = new Vector2(0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AI")
        {
            simPerson person = other.gameObject.GetComponent<simPerson>();
            person.myState = simPerson.State.Detection;
            person.myDestinations.Clear();
            //Debug.Log("Player Detected!");
            //Debug.Log("My new state is " + person.myState);
        }
    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        //GetAxisRaw is 0 or 1, GetAxis changes from 0 to 1, from 1 to 0, not instantly.

        Vector3 movement = new Vector3(inputX, inputY, 0);
        PlayerObject.velocity = movement * speed;
    }
    /*
    void baseMovementWrapper()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            var moveHorRight = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position += moveHorRight * speedHorRight * Time.deltaTime;
           
        }
        if (Input.GetAxis("Horizontal") < 0)
        {

            var moveHorLeft = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position += moveHorLeft * speedHorLeft * Time.deltaTime;
            
        }
        if (Input.GetAxis("Vertical") > 0)
        {

            var moveVertUp = new Vector3(0, Input.GetAxis("Vertical"), 0);
            transform.position += moveVertUp * speedVertUp * Time.deltaTime;
            
        }

        if (Input.GetAxis("Vertical") < 0)
        {

            var moveVertDown = new Vector3(0, Input.GetAxis("Vertical"), 0);
            transform.position += moveVertDown * speedVertDown * Time.deltaTime;
           
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        //Sets appropriate speed to zero if against a wall. 

        /*
        checkRay();
        if (againstWallVertUp)
        {
            speedVertUp = zeroSpeed; 
        }
        else
        {
            speedVertUp = actualSpeed;
        }

        if (againstWallVertDown)
        {
            speedVertDown = zeroSpeed;
        }
        else
        {
            speedVertDown = actualSpeed;
        }

        if (againstWallHorLeft)
        {
            speedHorLeft = zeroSpeed; 
        }
        else
        {
            speedHorLeft = actualSpeed;
        }

        if (againstWallHorRight)
        {
            speedHorRight = zeroSpeed;
        }
        else
        {
          speedHorRight = actualSpeed;
        }

        if(againstWallUpRight)
        {
            //Debug.Log("Diagonal Up Right is being hit!");
            speedHorRight = zeroSpeed;
            speedVertUp = zeroSpeed;
        }
        else
        {
            speedHorRight = actualSpeed;
            speedVertUp = actualSpeed;
        }

        if (againstWallUpLeft)
        {
            //Debug.Log("Diagonal Up Left is being hit!");

            speedHorLeft = zeroSpeed;
            speedVertUp = zeroSpeed;
        }
        else
        {
            speedHorLeft = actualSpeed;
            speedVertUp = actualSpeed;
        }

        if (againstWallDownRight)
        {
            //Debug.Log("Diagonal Down Right is being hit!");

            speedHorRight = zeroSpeed;
            speedVertDown = zeroSpeed;
        }
        else
        {
            speedHorRight = actualSpeed;
            speedVertDown = actualSpeed;
        }

        if (againstWallDownLeft)
        {
            //Debug.Log("Diagonal Down Left is being hit!");

            speedHorLeft = zeroSpeed;
            speedVertDown = zeroSpeed;
        }
        else
        {
            speedHorLeft = actualSpeed;
            speedVertDown= actualSpeed;
        }

        //baseMovementWrapper();
       */

    }
    /*
    void checkRay()
    {
        checkWall cW = wallCheck.GetComponent<checkWall>();
        if (cW.againstWallVertUp == true) againstWallVertUp = true;
        else againstWallVertUp = false;

        if (cW.againstWallVertDown == true) againstWallVertDown = true;
        else againstWallVertDown = false;

        if (cW.againstWallHorLeft == true) againstWallHorLeft = true;
        else againstWallHorLeft = false;


        if (cW.againstWallHorRight == true) againstWallHorRight = true;
        else againstWallHorRight = false;

        if (cW.againstWallUpRight == true) againstWallUpRight = true;
        else againstWallUpRight = false;

        if (cW.againstWallUpLeft == true) againstWallUpLeft = true;
        else againstWallUpLeft = false;

        if (cW.againstWallDownRight == true) againstWallDownRight = true;
        else againstWallDownRight = false;

        if (cW.againstWallDownLeft == true) againstWallDownLeft = true;
        else againstWallDownLeft = false;
    }
    */
}