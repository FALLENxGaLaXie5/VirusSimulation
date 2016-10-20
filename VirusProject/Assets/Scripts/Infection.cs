using UnityEngine;
using System.Collections;

public class Infection : MonoBehaviour
{
    public GameObject playerReference;
    public bool infected;
    public Color currentColor;

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
	    if(!infected)
        {
            checkForInfection();
        }
        else
        {
            //if they are infected
            //progress infection
            renderer.color = Color.Lerp(currentColor, Color.red, Mathf.PingPong(Time.time, 1));
            //Color.Lerp
        }
	}
    void checkForInfection()
    {
        if(Vector3.Distance(transform.position, playerReference.transform.position) <= 0.5)
        {
            infected = true;
            Debug.Log("I've been infected!");
            //currentColor = Color.red;
            //renderer.color = currentColor;
            //renderer.material.SetColor("_NewColor", Color.red);
            //Destroy(gameObject);
        }
    }
    void progressInfection()
    {

    }
}
