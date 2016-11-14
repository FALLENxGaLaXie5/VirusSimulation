/**
    Virus Simulation Project - Software Engineering Comp 350
    Transition2.cs
    Purpose: Manages transitions from game to main menu.

    @author Joshua Steward
    @version 1.0 11/7/2016
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition2 : MonoBehaviour
{
    //reference to quit menu UI object
    public Canvas quitMenu;

    /**
    Initialization Function. Initializes global variables. Similar to constructors.
    */
    void Start()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        quitMenu.enabled = false;
    }

    /**
    Update is called at the beginning of every frame at run time.
    This means that all runnable code is ran at one point or another from here.
    Similar to main or runnable with frame by frame implementation.
    Will check for exit keys.
*/
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            ExitPress();
        }
        if(quitMenu.enabled && Input.GetKeyDown("y"))
        {
            transitionScene();
        }
        else if(quitMenu.enabled && Input.GetKeyDown("n"))
        {
            NoPress();
        }
    }

    /**
    To enable quit menu if exit button is pressed from main menu.
    */
    public void ExitPress()
    {
        quitMenu.enabled = true;
    }

    /**
    To disable quit menu if no button is pressed on said menu.
    */
    public void NoPress()
    {
        quitMenu.enabled = false;
    }

    /**
    Transition to main menu scene.
    */
    public void transitionScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}