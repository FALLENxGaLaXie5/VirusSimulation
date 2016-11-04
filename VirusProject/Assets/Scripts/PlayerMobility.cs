using UnityEngine;
using System.Collections;

public class PlayerMobility : MonoBehaviour
{
    public float speed;
    private Rigidbody PlayerObject;

    void Start ()
    {
        PlayerObject = GetComponent<Rigidbody>();
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
        Vector3 movement = new Vector3(inputX, inputY, 0);
        PlayerObject.velocity = movement * speed;
    }
}