using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] destinationBuildingsIn;
    public List<GameObject> destBuildings;
	// Use this for initialization
	void Start ()
    {
	}
	void Awake()
    {
        instance = this;
        destinationBuildingsIn = GameObject.FindGameObjectsWithTag("dest");
        destBuildings = new List<GameObject>();
        for (int i = 0; i < destinationBuildingsIn.Length; i++)
        {
            destBuildings.Add(destinationBuildingsIn[i]);
            Debug.Log(destBuildings[i].tag + " tag was added to dest buildings!");
        }

    }
	// Update is called once per frame
	void Update () {
	
	}
}
