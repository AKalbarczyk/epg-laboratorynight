using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour {

    private Text text;

	void Start () 
    {
        text = GetComponent<Text>();
	}
	
	void Update () 
    {
	
	}

    public void OnMouseEnter()
    {
        text.color = Color.magenta;
    }

    public void OnMouseExit()
    {
        text.color = Color.white;
    }

    void OnMouseDown()
    {
        
    }



    public void TextAction()
    {
        if (this.gameObject.name == "Start")
        {
            Application.LoadLevel(0);
        }

        else if (this.gameObject.name == "Exit")
        {
            Application.Quit();
        }
    }
}
