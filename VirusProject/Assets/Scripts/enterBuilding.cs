using UnityEngine;
using System.Collections;

public class enterBuilding : MonoBehaviour
{
    private GameObject playerReference;
	// Use this for initialization
	void Start ()
    {
        playerReference = GameObject.FindGameObjectWithTag("wallCheck");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
