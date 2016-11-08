using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpTransition : MonoBehaviour
{
    /**
        Virus Simulation Project - Software Engineering Comp 350
        HelpTransition.cs
        Purpose: Will be called by button to transition back to title scene

        @author Joshua Steward
        @version 1.0 11/7/2016
    */
    public void transHelp()
    {
        SceneManager.LoadScene("TitleScene");
    }

}
