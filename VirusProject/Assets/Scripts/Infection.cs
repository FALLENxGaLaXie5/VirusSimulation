using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Infection : MonoBehaviour
{
    public GameObject playerReference;
    public bool infected;
    public Color currentColor;
    GameManager gameManagerInstance;

    public float R;
    public float G;
    public float B;
    public float A;
    //private SpriteRenderer renderer;


    void OnTriggerEnter2D(Collider2D other)
    { 

        int prob = Random.Range(0, 100);

        if(infected && other.gameObject.tag == "AI" && prob <= 2)
        {
            //StartCoroutine(waitToInfect());
            Infection inf = other.gameObject.GetComponent<Infection>();
            inf.infected = true;
            Debug.Log("AI INFECTED AI!");
        }
    }

    IEnumerator waitToInfect()
    {
        yield return new WaitForSeconds(4f);
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        if(infected && other.gameObject.tag == "AI")
        {
            //Debug.Log("I touched another AI!");
            Infection objInf = other.gameObject.GetComponent<Infection>();
            GameObject newTimeObject = Instantiate
            peopleTouched.Add(new TimeObject(ref objInf));
        }
    }
    */
    /*
    void OnTriggerExit(Collider other)
    {
        if(infected && other.gameObject.tag == "AI")
        {
            for(int i = 0; i < peopleTouched.Count; i++)
            {
                if(peopleTouched[i].infObject == null)
                {
                    peopleTouched.RemoveAt(i);
                    continue;
                }
                else if(peopleTouched[i].infObject.gameObject.GetInstanceID() == other.gameObject.GetInstanceID())
                {
                    //Debug.Log("AI " + peopleTouched[i].infObject.gameObject.GetInstanceID() + " ran from AI!");
                    TimeObject timObj = peopleTouched[i];
                    timObj = null;
                    peopleTouched.RemoveAt(i);
                }
            }
        }
    }
    */

    // Use this for initialization
    void Start ()
    {
        gameManagerInstance = GameManager.instance;
        playerReference = GameObject.FindGameObjectWithTag("wallCheck");
        infected = false;
        //GetComponent<Renderer>() = GetComponent<SpriteRenderer>();
        //if(renderer.color != null)
        //{
            //currentColor = renderer.color;
        //}
        //peopleTouched = new List<TimeObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        progressInfection();
        
	}

    void checkForInfection()
    {
        if(Vector3.Distance(transform.position, playerReference.transform.position) <= 2.0)
        {
            infected = true;
            //Debug.Log("I've been infected!");
            //currentColor = Color.blue
          
            //renderer.color = currentColor;
            //renderer.material.SetColor("_NewColor", Color.red);
            //Destroy(gameObject);
        }
        else
        {
            //Debug.Log("Too far away!");
        }
    }

    void OnMouseDown()
    {
        //Debug.Log("I got clicked!");
        if(!infected)
        {
            checkForInfection();
        }
    }

    void OnMouseOver()
    {
        //Debug.Log("The Mouse is over me! HEEEPPP!!");
    }
    void progressInfection()
    {
        if (infected)
        {
            //if they are infected
            //progress infection
            //GetComponent<Renderer>().color = Color.Lerp(GetComponent<Renderer>().color, Color.red, 0.001f);
            //R = GetComponent<Renderer>().color.r;
            if (1 - R <= 0.05)
            {
                gameManagerInstance.aiList.Remove(gameObject);
                Destroy(gameObject);

            }
            //G = GetComponent<Renderer>().color.g;
            //B = GetComponent<Renderer>().color.b;
            //A = GetComponent<Renderer>().color.a;
        }
    }
}