using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("wallCheck");
    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
