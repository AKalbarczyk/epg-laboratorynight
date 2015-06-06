using UnityEngine;
using System.Collections;

public class BossGunController : MonoBehaviour {

    private Transform target, myPosition;
    //shooting stuff
    public GameObject weaponFlash;
    public GameObject smallRocket;
    private const float WEAPON_FORCE = 20;
    bool initFire = true;
    // Use this for initialization
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
        myPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(target.position, transform.position) < 19F & initFire)
        {
            StartCoroutine("Shoot", 2f);
            initFire = false;
        }
    }

    IEnumerator Shoot(float wait)
    {
        while (true)
        {
            ShootRifle();
            yield return new WaitForSeconds(wait);
        }
    }

    private void ShootRifle()
    {
        Transform shotTranform = transform;
        GameObject shot = Instantiate(smallRocket, shotTranform.position, transform.rotation) as GameObject;
        shot.GetComponent<Rigidbody>().AddForce(shotTranform.forward * WEAPON_FORCE, ForceMode.Impulse);
        //StartCoroutine("AccelerateRocket", shot);

        //GameObject flash = Instantiate(weaponFlash, shotTranform.position, transform.rotation) as GameObject;
        //Destroy(flash, 0.3f);

    }

    private IEnumerator AccelerateRocket(GameObject shot)
    {
        yield return new WaitForSeconds(0.4f);
        if (shot)
            shot.GetComponent<Rigidbody>().AddForce(transform.forward * WEAPON_FORCE * 2, ForceMode.Impulse);
    }
}
