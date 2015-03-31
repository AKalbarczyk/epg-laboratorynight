using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour 
{
    
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Movable")
        {
          //  Debug.Log("aa");
            OpenLockedDoor.allowOpen = true;

        }
    }
}
