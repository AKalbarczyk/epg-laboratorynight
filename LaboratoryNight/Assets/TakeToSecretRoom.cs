using UnityEngine;
using System.Collections;

public class TakeToSecretRoom : MonoBehaviour {
    GameObject player;
        
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {       
            GameObject tp = GameObject.FindGameObjectWithTag("tpSpot");            
            player = GameObject.FindGameObjectWithTag("Player");           
            player.transform.position = new Vector3(380.9f,0f,173f);
        }
    }

   
}
