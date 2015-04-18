using UnityEngine;
using System.Collections;

public class RobotThrowerGunController : MonoBehaviour {

    public GameObject rocket;
    //public GameObject efx;
    private Transform player;
    float dist;
    bool startShooting = false;

	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        dist = 100F;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        dist = Vector3.Distance(player.position, transform.position);

        if (dist <25F && !startShooting)
        {
            startShooting = true;
            StartCoroutine("ShootRocket");
        }
	}

    private IEnumerator ShootRocket()
    {

        while (startShooting)
        {
            GameObject obj = Instantiate(rocket, transform.position, transform.rotation) as GameObject;

            obj.GetComponent<Rigidbody>().AddForce(transform.up * 30f, ForceMode.Impulse);
            Destroy(obj, 5f);

            yield return new WaitForSeconds(1);
        }
    }
}
