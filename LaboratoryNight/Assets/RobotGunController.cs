using UnityEngine;
using System.Collections;

public class RobotGunController : MonoBehaviour
{

    public GameObject rocket;
    //public GameObject efx;
    private Transform player;
    float dist;
    bool startShooting = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dist = 100F;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!startShooting)
            dist = Vector3.Distance(player.position, transform.position);

        if (dist < 25F && !startShooting)
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

            obj.GetComponent<Rigidbody>().AddForce(transform.forward * 100f, ForceMode.Impulse);
            Destroy(obj, 0.5f);

            yield return new WaitForSeconds(0.4f);
        }
    }

}
