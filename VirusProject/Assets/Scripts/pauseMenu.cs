using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class pauseMenu : MonoBehaviour
{

    #region Attributs

    private bool isPaused = false; 

    #endregion

    #region Proprietes
    #endregion

    #region Constructeur
    #endregion

    #region Methodes

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            isPaused = true;


        if (isPaused) { Time.timeScale = 0f; }
        else { Time.timeScale = 1.0f; }


    }

    void OnGUI()
    {
        if (isPaused)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 60, 100, 40), "Continue"))
            {
                isPaused = false;
            }



            if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height / 2 + 60, 100, 40), "Quit"))
            {
                Application.Quit();
            }

        }
    }

    #endregion
}