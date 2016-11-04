using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private int initialCount;
    private int count;
    private GameObject[] AI;
    public List<GameObject> aiList;
    public static GameManager instance;
    public GameObject[] destinationBuildingsIn;
    public List<GameObject> destBuildings;

	// Use this for initialization
	void Start ()
    {
        AI = GameObject.FindGameObjectsWithTag("AI");
        aiList = new List<GameObject>();
        populateAI();
        initialCount = AI.Length;
        count = initialCount;
	}
	void Awake()
    {
        instance = this;
        destinationBuildingsIn = GameObject.FindGameObjectsWithTag("dest");
        destBuildings = new List<GameObject>();
        for (int i = 0; i < destinationBuildingsIn.Length; i++)
        {
            destBuildings.Add(destinationBuildingsIn[i]);
        }

    }
	// Update is called once per frame
	void Update ()
    {
        count = aiList.Count;
	}

    void populateAI()
    {
        for(int i = 0; i < AI.Length; i++)
        {
            aiList.Add(AI[i]);
        }
    }
}