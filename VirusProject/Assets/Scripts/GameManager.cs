using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //initial count of number of AI
    private int initialCount;

    //current number of AI
    private int count;

    //array of AI objects
    private GameObject[] AI;

    //list of AI objects
    public List<GameObject> aiList;

    //instance - static instance of GameManager class
    public static GameManager instance;

    //array of destination buildings
    public GameObject[] destinationBuildingsIn;

    //list of destination buildings
    public List<GameObject> destBuildings;

    /**
    Virus Simulation Project - Software Engineering Comp 350
    GameManager.cs
    Purpose: Initialization Function. Initializes global variables. Similar to constructors.

    @author Joshua Steward
    @version 1.0 11/7/2016
    */
    void Start ()
    {
        AI = GameObject.FindGameObjectsWithTag("AI");
        aiList = new List<GameObject>();
        populateAI();
        initialCount = AI.Length;
        count = initialCount;
	}

    /**
    Virus Simulation Project - Software Engineering Comp 350
    GameManager.cs
    Purpose: Initialization Function.
        Unlike start, awake will initialize on start of game, instead of when this component is initialazed. So, it is called before start.

    @author Joshua Steward
    @version 1.0 11/7/2016
*/
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

    /**
        Virus Simulation Project - Software Engineering Comp 350
        GameManager.cs
        Purpose: Update is called at the beginning of every frame at run time.
        This means that all runnable code is ran at one point or another from here.
        Similar to main or runnable with frame by frame implementation.
        Will update the count of number of AI

        @author Joshua Steward
        @version 1.0 11/7/2016
    */
    void Update ()
    {
        count = aiList.Count;
	}

    /**
        Virus Simulation Project - Software Engineering Comp 350
        GameManager.cs
        Purpose: Initially populates the list of AI from objects array.

        @author Joshua Steward
        @version 1.0 11/7/2016
    */
    void populateAI()
    {
        for(int i = 0; i < AI.Length; i++)
        {
            aiList.Add(AI[i]);
        }
    }
}