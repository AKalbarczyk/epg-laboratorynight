using UnityEngine;
using System.Collections;

public class LaserGunController : MonoBehaviour {

	// Use this for initialization
    public GameObject laserHit;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.SendMessage("TakeDamage", 0.4f);
            GameObject obj = Instantiate(laserHit, col.gameObject.transform.position, col.gameObject.transform.rotation) as GameObject;
            Destroy(obj, 0.5f);
        }
    }
}
