using UnityEngine;
using System.Collections;

public class RobotBulletController : MonoBehaviour {

	// Use this for initialization

    public GameObject hitEfx;
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.SendMessage("TakeDamage", 0.1f);
            GameObject efx = Instantiate(hitEfx, transform.position, transform.rotation) as GameObject;
            Destroy(efx, 0.4f);
            Destroy(this.gameObject, 0.5f);
        }
        if (col.gameObject.tag == "NotWalkable" || col.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        else if (col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject, 0.1f);
        }
      
    }
}
