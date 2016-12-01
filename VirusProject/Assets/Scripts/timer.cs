/**
    Virus Simulation Project - Software Engineering Comp 350
    timer.cs
    Purpose: Code to display in game timer.

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
using System.Collections;
using System.Collections.Generic;

public class timer : MonoBehaviour
{
    public Text timerLabel;

    private float time;
    void Start()
    {
        time = 0f;
    }


    /**
        Update is called at the beginning of every frame at run time.
        This means that all runnable code is ran at one point or another from here.
        Similar to main or runnable with frame by frame implementation.
        here, update will be used to update time and then use modulo arithmetic to keep track of minutes, seconds, and milliseconds.
    */
    void Update()
    {
        time += Time.deltaTime;

        var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = time % 60;//Use the euclidean division for the seconds.
        var fraction = (time * 100) % 100;

        //update the label value
        timerLabel.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
    }
}
