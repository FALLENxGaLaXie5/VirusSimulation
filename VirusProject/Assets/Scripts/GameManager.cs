using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] destBuildings;
	// Use this for initialization
	void Start ()
    {
        destBuildings = GameObject.FindGameObjectsWithTag("dest");
	}
	void Awake()
    {
        instance = this;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
