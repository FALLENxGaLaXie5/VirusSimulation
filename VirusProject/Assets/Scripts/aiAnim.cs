using UnityEngine;
using System.Collections;

public class aiAnim : MonoBehaviour
{
    Animator anim;
    simPerson sim;
	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        sim = GetComponentInParent<simPerson>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        checkForState();
	}
    void checkForState()
    {
        if (sim.myState == simPerson.State.Walking)
        {
            anim.SetBool("running", false);
        }
        else if (sim.myState == simPerson.State.Detection)
        {
            anim.SetBool("running", true);
        }
    }
}
