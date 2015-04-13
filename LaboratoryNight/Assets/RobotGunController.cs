using UnityEngine;
using System.Collections;

public class RobotGunController : MonoBehaviour
{

    public GameObject rocket;
    public GameObject efx;
    private Transform player;
    float dist;
    bool startShooting = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        dist = 100F;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(player.transform);
        dist = Vector3.Distance(player.position, transform.position);
      //  Debug.Log(transform.position + " || " + player.transform + " || " + dist + " || " + startShooting);

        if (dist < 15F && !startShooting)
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
            GameObject efxObj = Instantiate(efx, transform.position, Quaternion.identity) as GameObject;
            Destroy(efxObj, 0.1f);

            obj.GetComponent<Rigidbody>().AddForce(transform.forward * 40f, ForceMode.Impulse);
            StartCoroutine("AccelerateRocket", obj);

            yield return new WaitForSeconds(1.2f);
        }
    }

    private IEnumerator AccelerateRocket(GameObject obj)
    {
        yield return new WaitForSeconds(0.2f);
        obj.GetComponent<Rigidbody>().AddForce(transform.forward * 40f, ForceMode.Impulse);
    }

}
