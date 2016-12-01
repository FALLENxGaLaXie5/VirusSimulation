/**
    Virus Simulation Project - Software Engineering Comp 350
    GameManager.cs
    Purpose: Transitions to help scene.

    @author Joshua Steward
    @author Zane Gittins
    @author Zachery Van Es
    @author Lowell Batacan
    @author Jessica Perez
    @author Andrew Walters

    @version 1.0 11/7/2016
*/



using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class transHelp : MonoBehaviour
{
    public void transTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
