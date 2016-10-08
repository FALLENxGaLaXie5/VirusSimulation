using UnityEngine;
using System.Collections;

public class destinationTalk : MonoBehaviour
{
    public bool talk;
    // Use this for initialization
    TextMesh text;
    GameObject text1;
    void Start ()
    {
        text = gameObject.GetComponentInChildren(typeof(TextMesh)) as TextMesh;
        talk = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(text != null)
        {
            if (text.gameObject.tag == "coffeeTag" && talk)
            {
                text.text = "I'm a coffee shop!";
            }
            else if (text.gameObject.tag == "foodTag" && talk)
            {
                text.text = "I gotz food for ya!";
            }
            else if (talk == false)
            {
                text.text = "";
            }
        }
	}
}
