using UnityEngine;
using System.Collections;

public class faceMouse : MonoBehaviour
{

    /**
        Virus Simulation Project - Software Engineering Comp 350
        faceMouse.cs
        Purpose: Update is called at the beginning of every frame at run time.
        This means that all runnable code is ran at one point or another from here.
        Similar to main or runnable with frame by frame implementation.
        Update will change player's rotation to reflect mouse direction. 

        @author Joshua Steward
        @version 1.0 11/7/2016
    */
    void Update()
    {
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);
    }
}
