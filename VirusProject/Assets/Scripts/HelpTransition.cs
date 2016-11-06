using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpTransition : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
	
	}
	public void transHelp()
    {
        SceneManager.LoadScene("TitleScene");
    }
	// Update is called once per frame
	void Update () {
	
	}
}
