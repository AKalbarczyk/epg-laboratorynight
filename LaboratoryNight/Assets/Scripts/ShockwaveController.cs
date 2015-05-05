using UnityEngine;
using System.Collections;

public class ShockwaveController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(10f, transform.position, 5f);
            col.SendMessage("TakeDamage", 0.5f);
        }
    }
}
