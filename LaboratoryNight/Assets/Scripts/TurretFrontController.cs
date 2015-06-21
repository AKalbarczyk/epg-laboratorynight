using UnityEngine;
using System.Collections;

public class TurretFrontController : MonoBehaviour {

    public GameObject rocket;
    public GameObject efx;
    private Transform player;
    float dist;
    bool startShooting = false;

    public SoundsController sounds;
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sounds = GameCamera.FindObjectOfType<SoundsController>();
        dist = 100F;
       //StartCoroutine("ShootRocket");
       
        
	}
	
	// Update is called once per frame
	void Update () 
    {
       // Debug.Log(player.transform);
        if(!startShooting)
            dist = Vector3.Distance(player.position, transform.position);
        
        if (dist < 25f && !startShooting)
        {
            startShooting = true;
            StartCoroutine("ShootRocket");
        }
	}

    private IEnumerator ShootRocket()
    {
        
        while (startShooting)
        {
            GameObject obj = Instantiate(rocket, transform.position, new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w)) as GameObject;
            GameObject efxObj = Instantiate(efx, transform.position, Quaternion.identity) as GameObject;
            Destroy(efxObj, 0.3f);

            obj.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
            StartCoroutine("AccelerateRocket", obj);

            sounds.TurretShot();

            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator AccelerateRocket(GameObject obj)
    {
        yield return new WaitForSeconds(0.4f);
        if (obj)
            obj.GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
    }

}
