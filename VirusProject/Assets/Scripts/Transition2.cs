using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition2 : MonoBehaviour
{
    public Canvas quitMenu;
    //public Button startText;
    //public Button exitText;

    void Start()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        //startText = startText.GetComponent<Button>();
        //exitText = exitText.GetComponent<Button>();
        quitMenu.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            ExitPress();
        }
        if(quitMenu.enabled && Input.GetKeyDown("y"))
        {
            transitionScene();
        }
        else if(quitMenu.enabled && Input.GetKeyDown("n"))
        {
            NoPress();
        }
    }

    public void ExitPress()
    {
        quitMenu.enabled = true;
        //startText.enabled = false;
        //exitText.enabled = false;
    }

    public void NoPress()
    {
        quitMenu.enabled = false;
        //startText.enabled = true;
        //exitText.enabled = true;
    }

    public void transitionScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}