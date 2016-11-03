using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Infection : MonoBehaviour
{
    public GameObject playerReference;
    public bool infected;
    public Color currentColor;

    public float R;
    public float G;
    public float B;
    public float A;
    private SpriteRenderer renderer;

    List<GameObject> peopleTouched;

    void OnTriggerEnter(Collider other)
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
        playerReference = GameObject.FindGameObjectWithTag("wallCheck");
        infected = false;
        renderer = GetComponent<SpriteRenderer>();
        currentColor = renderer.color;
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
            //renderer.color = Color.Lerp(currentColor, Color.red, Mathf.PingPong(Time.time, 1));
            renderer.color = Color.Lerp(renderer.color, Color.red, 0.001f);
            R = renderer.color.r;
            if (1 - R <= 0.05)
            {
                Destroy(gameObject);
            }
            G = renderer.color.g;
            B = renderer.color.b;
            A = renderer.color.a;
            //Color.Lerp
        }
    }
}