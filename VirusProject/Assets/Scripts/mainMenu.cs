/**
Virus Simulation Project - Software Engineering Comp 350
mainMenu.cs
Purpose: Will load the main scene.

@author Joshua Steward
@author Zane Gittins
@author Zachery Van Es
@author Lowell Batacan
@author Jessica Perez
@author Andrew Walters

@version 1.0 11/7/2016
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    /**
    Will load the main scene.

    @param sceneIndex The index in the array of scenes to access
    */
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
