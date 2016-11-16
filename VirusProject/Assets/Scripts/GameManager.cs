/**
    Virus Simulation Project - Software Engineering Comp 350
    GameManager.cs
    Purpose: Manages game properties such as current AI statistics, building data, and scores.

    @author Joshua Steward
    @version 1.0 11/7/2016
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //initial count of number of AI
    private int initialCount;

    //current number of AI
    public int count;

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

    //score UI text reference
    public Text scoreText;


    /**
    Initialization Function. Initializes global variables. Similar to constructors.
    */
    void Start ()
    {
        AI = GameObject.FindGameObjectsWithTag("AI");
        aiList = new List<GameObject>();
        populateAI();
        initialCount = AI.Length;
        count = initialCount;
        scoreText.text = "The Living: " + count + "\nPopulation: " + initialCount;
	}

    /**
    Initialization Function.
        Unlike start, awake will initialize on start of game, instead of when this component is initialazed. So, it is called before start.
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
        Update is called at the beginning of every frame at run time.
        This means that all runnable code is ran at one point or another from here.
        Similar to main or runnable with frame by frame implementation.
        Will update the count of number of AI
    */
    void Update ()
    {
        count = aiList.Count;
        scoreText.text = "The Living: " + count + "\nPopulation: " + initialCount;
        if(count < 2)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    /**
        Initially populates the list of AI from objects array.
    */
    void populateAI()
    {
        for(int i = 0; i < AI.Length; i++)
        {
            aiList.Add(AI[i]);
        }
    }
}