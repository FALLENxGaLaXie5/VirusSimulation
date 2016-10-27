using UnityEngine;
using System.Collections;

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
	// Use this for initialization
	void Start ()
    {
        playerReference = GameObject.FindGameObjectWithTag("wallCheck");
        infected = false;
        renderer = GetComponent<SpriteRenderer>();
        currentColor = renderer.color;
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
            Debug.Log("I've been infected!");
            //currentColor = Color.blue
          
            //renderer.color = currentColor;
            //renderer.material.SetColor("_NewColor", Color.red);
            //Destroy(gameObject);
        }
        else
        {
            Debug.Log("Too far away!");
        }
    }

    void OnMouseDown()
    {
        Debug.Log("I got clicked!");
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
