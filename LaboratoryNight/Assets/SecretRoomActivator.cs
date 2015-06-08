using UnityEngine;
using System.Collections;

public class SecretRoomActivator : MonoBehaviour {
    public GameObject efx;
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
            GameObject obj = Instantiate(efx, transform.position + transform.right * 8 + transform.up *3, Quaternion.identity) as GameObject;
            
        }
    }
}
