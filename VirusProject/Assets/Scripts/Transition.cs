/**
    Virus Simulation Project - Software Engineering Comp 350
    Transition.cs
    Purpose: Manages transitions from main menu to game and help menu.

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

public class Transition : MonoBehaviour
{
    //reference to quit menu UI object
    public Canvas quitMenu;

    //reference to button for starting the game
    public Button startText;

    //reference to button for exiting the game
    public Button exitText;

    /**
    Initialization Function. Initializes global variables. Similar to constructors.
    */
    void Start()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        quitMenu.enabled = false;
    }

    /**
    To enable quit menu if exit button is pressed from main menu.
    */
    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }

    /**
    To disable quit menu if no button is pressed on said menu.
    */
    public void NoPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }

    /**
    Transition to main scene.
    */
    public void transitionScene()
    {
        SceneManager.LoadScene("Main");
    }

    /**
    Transition to help scene.
    */
    public void transitionHelp()
    {
        SceneManager.LoadScene("HelpUI");
    }

    /**
    End game function.
    */
    public void quitGame()
    {
        Application.Quit();
    }
}