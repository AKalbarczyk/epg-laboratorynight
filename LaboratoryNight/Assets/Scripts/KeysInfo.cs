using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeysInfo : MonoBehaviour {

	// Use this for initialization
    private RawImage img;
    public Text text;

	void Start () {
        img = GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.Space))
        {
            img.enabled = false;
            text.enabled = false;
            Destroy(this.gameObject);
        }
	
	}
}
