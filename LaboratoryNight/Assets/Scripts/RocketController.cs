using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {

    public GameObject explosion;
    private const float DAMAGE = 0.3f;

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
            Camera.main.SendMessage("CamShake");
            col.gameObject.SendMessage("TakeDamage", DAMAGE);
    
            GameObject efx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.5f);
            Destroy(this.gameObject, 0.1f);

        }


        else if (col.gameObject.name == "turretObj")
        {
            GameObject efx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.5f);
            Destroy(col.gameObject);
            Destroy(this.gameObject, 0.1f);
        }

        else if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.SendMessage("TakeDamage", DAMAGE + 0.2f);

            GameObject efx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.5f);
            Destroy(this.gameObject, 0.1f);
        }

    }

}
