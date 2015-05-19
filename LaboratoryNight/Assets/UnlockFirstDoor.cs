using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnlockFirstDoor : MonoBehaviour {
    public Text text;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {            
            text.text = "press space to hack the computer";
            if (Input.GetKey(KeyCode.Space))
            {
                text.text = "asdddddddddddddddddddd";
            }
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {
            text.text = "";
        }
    }
}
