using UnityEngine;
using System.Collections;

public class QuickLevelLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.F1))
        {
            Application.LoadLevel(1);
        }
        else if (Input.GetKey(KeyCode.F2))
        {
            Application.LoadLevel(2);
        }
        else if (Input.GetKey(KeyCode.F3))
        {
            Application.LoadLevel(3);
        }
	}
}
