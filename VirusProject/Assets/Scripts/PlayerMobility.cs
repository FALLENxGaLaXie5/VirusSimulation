using UnityEngine;
using System.Collections;

public class PlayerMobility : MonoBehaviour
{
    /*
    public float speed;
    public float speedCopy;
    private Rigidbody2D PlayerObject;

    Animator animator;
    void Start ()
    {
        PlayerObject = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speedCopy = speed;
    }

    void OnTriggerEnter2D(Collider2D other)
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
        //float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        if(inputY > 0)
        {
            speedCopy = speed;
            Vector3 mouseScreen = Input.mousePosition;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mouseScreen);
            mouseScreen = new Vector3(mouseScreen.x, mouseScreen.y, 0);
            PlayerObject.velocity = (mouseScreen - transform.position) * speedCopy;
            //Vector3 movement = new Vector3(mouseScreen.x, mouseScreen.y, 0);
            //PlayerObject.velocity = movement * speed;
            //PlayerObject.velocity = (((transform.right * mouseScreen.x) + (transform.forward * mouseScreen.y))/Time.deltaTime) * speedCopy;
            //mouseScreen.z = -Camera.main.transform.position.z;
            //mouseScreen = Camera.main.ScreenToWorldPoint(mouseScreen);
            //Vector3 dir = mouseScreen - transform.position;
            //PlayerObject.AddForce(dir * speedCopy);
        }
        else
        {
            PlayerObject.velocity = Vector2.zero;
            speedCopy = 0;
        }
        
        //Vector3 movement = new Vector3(inputX, inputY, 0);
        
        if (PlayerObject.velocity == Vector2.zero)
        {
            animator.SetBool("idle", true);
        }
        else
        {
            animator.SetBool("idle", false);
        }
    }
    
    

        
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, speed);
        }
    }
    
    */
    public float speed;
    private float speedCopy;
    public float increasedSpeed;
    Animator animator;

    private Vector3 targetPosition;
    private bool isMoving;
    Rigidbody2D playerObject;

    const int BUTTON_FOR_MOVEMENT = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerObject = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
        isMoving = false;
        speedCopy = speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "AI")
        {
            simPerson person = other.gameObject.GetComponent<simPerson>();
            person.myState = simPerson.State.Detection;
            person.myDestinations.Clear();
            //Debug.Log("Player Detected!");
            //Debug.Log("My new state is " + person.myState);
        }
    }

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

    void FixedUpdate()
    {
        if (isMoving)
        {
            MovePlayer();
        }
    }

    void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isMoving = true;
    }

    void MovePlayer()
    {
        Vector2 direction = (targetPosition - transform.position).normalized;
        playerObject.AddForce(direction * speed);

        Debug.DrawLine(transform.position, targetPosition, Color.red);
    }

}