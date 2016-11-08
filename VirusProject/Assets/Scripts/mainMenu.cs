using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    /**
    Virus Simulation Project - Software Engineering Comp 350
    mainMenu.cs
    Purpose: Will load the main scene.

    @author Joshua Steward
    @version 1.0 11/7/2016
    */
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
