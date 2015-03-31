using UnityEngine;
using System.Collections;

public class GravityShotController : MonoBehaviour {

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}


    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Movable")
        {
         //   Debug.Log("collided with gravGun");
            Rigidbody r = col.gameObject.GetComponent<Rigidbody>();
            StartCoroutine("DisableGravity", r);
            r.AddForce(Vector3.up * 1.2f + transform.forward * 7f, ForceMode.Impulse);
            
        }
    }

    private IEnumerator DisableGravity(Rigidbody r)
    {
        //float previousMass = r.mass;
        //r.mass /= 5f;
        r.useGravity = false;
        yield return new WaitForSeconds(0.5f);
        r.useGravity = true;
    }
}
