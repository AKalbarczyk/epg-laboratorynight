using UnityEngine;
using System.Collections;

public class TurretFrontController : MonoBehaviour {

    public GameObject rocket;
    public GameObject efx;

	void Start () 
    {
        StartCoroutine("ShootRocket");
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    private IEnumerator ShootRocket()
    {
        while (true)
        {
            GameObject obj = Instantiate(rocket, transform.position, Quaternion.identity) as GameObject;
            GameObject efxObj = Instantiate(efx, transform.position, Quaternion.identity) as GameObject;
            Destroy(efxObj, 0.3f);

            obj.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
            StartCoroutine("AccelerateRocket", obj);

            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator AccelerateRocket(GameObject obj)
    {
        yield return new WaitForSeconds(0.4f);
        obj.GetComponent<Rigidbody>().AddForce(transform.forward * 40f, ForceMode.Impulse);
    }

}
