using UnityEngine;
using System.Collections;

public class simPerson : MonoBehaviour
{

	// Use this for initialization
	public Transform target;
    private GameObject gO;
    public float speed;


    public GameObject[] patrolPoints;
    public string wayPointTrack;

    //Used for patrol
    private int wayPointNumber;
    public bool oncePerCycle = true;


    void Start()
    {
        patrolPoints = GameObject.FindGameObjectsWithTag(wayPointTrack);
        wayPointNumber = Random.Range(0, patrolPoints.Length);
    }

    void Update()
    {
        AILerp lerp = gameObject.GetComponent<AILerp>();
        lerp.enabled = true;
        Patrol();
     }

    public void Patrol()
	{
       
		//Classic Patrol Behaviour
		GameObject currentWaypoint = patrolPoints [wayPointNumber];
		AILerp lerp = gameObject.GetComponent<AILerp> ();
		lerp.speed = speed;
		lerp.rotationSpeed = 30;
		lerp.target = currentWaypoint.transform;

		if (Vector3.Distance (transform.position, currentWaypoint.transform.position) <= 0.5) 
		{
			wayPointNumber++;
			if (wayPointNumber >= patrolPoints.Length) 
			{
					wayPointNumber = 0;
			}

		}
	}

}
