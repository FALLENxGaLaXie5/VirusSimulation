using UnityEngine;
using System.Collections;

public class TimeObject : MonoBehaviour
{
    public bool running = false;
    public float timer { get; set; }
    public Infection infObject;
    public bool potentialInfection = false;
    public bool waitDone = false;
    public TimeObject(ref Infection objectPassed)
    {
        timer = 0f;
        infObject = objectPassed;
        if (!running && !waitDone)
        {
            StartCoroutine(waitToInfect());
        }
        if (waitDone)
        {
            objectPassed.infected = true; waitDone = false;
        }
        if (objectPassed.infected)
        {
            Debug.Log("AI INFECTED AI!!!");
        }
    }
    void Start()
    {
        //waitToInfect();
    }

    IEnumerator waitToInfect()
    {
        running = true;
        yield return new WaitForSeconds(4f);
        waitDone = true;
        running = false;
        //infObject.infected = true;
        //Debug.Log("AI INFECTED AI!!!");
    }
    /*
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1f)
        {
            infObject.infected = true;
            Debug.Log("AI INFECTED AI!!!!");
        }
    }
    */
}
