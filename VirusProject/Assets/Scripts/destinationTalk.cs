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
            else if(text.gameObject.tag == "schoolTag" && talk)
            {
                text.text = "School time bois and gurls!";
            }
            else if(text.gameObject.tag == "churchTag" && talk)
            {
                text.text = "Come get ya Jesus!";
            }
            else if (text.gameObject.tag == "clubTag" && talk)
            {
                text.text = "UNGST UNGST UNGST UNGST UNGST UNGST UNGST";
            }
            else if (text.gameObject.tag == "frysElectronicsTag" && talk)
            {
                text.text = "Electronics for everyone!";
            }
            else if (text.gameObject.tag == "homeTag" && talk)
            {
                text.text = "Finally home...";
            }
            else if (text.gameObject.tag == "gameStopTag" && talk)
            {
                text.text = "ALL DA GAMES!";
            }
            else if (talk == false)
            {
                text.text = "";
            }
        }
	}
}
