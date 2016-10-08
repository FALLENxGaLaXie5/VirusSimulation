using UnityEngine;
using System.Collections;

public class DisplayText : MonoBehaviour
{
    public string destinationTag;
    public float range;
    GameObject[] destinations;
	// Use this for initialization
	void Start ()
    {
        destinations = GameObject.FindGameObjectsWithTag(destinationTag);
	}

    void Update()
    {
        checkIfNearDestination();
    }

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
