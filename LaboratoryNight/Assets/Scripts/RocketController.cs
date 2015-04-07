using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {

    public GameObject explosion;

    private bool canBeDestroyed = true;
	void Start () 
    {
	    
	}
	
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject efx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.5f);
            Destroy(this.gameObject, 0.1f);

            //more player logic (health etc.)
        }


        else if (col.gameObject.name == "turret")
        {
            GameObject efx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.5f);
            Destroy(col.gameObject);
            Destroy(this.gameObject, 0.1f);
        }

    }

}
