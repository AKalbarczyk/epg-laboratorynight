using UnityEngine;
using System.Collections;

public class ShootAtPlayer : MonoBehaviour {
    private Transform target, myPosition;
    //shooting stuff
    public GameObject weaponFlash;
    public GameObject bullet;
    private const float WEAPON_FORCE = 120;
    bool initFire = true;
	// Use this for initialization
	void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
        myPosition = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(target.position, transform.position) < 15F & initFire)
        {
            StartCoroutine("Shoot",0.6f);
            initFire = false;
        }
	}

    IEnumerator Shoot(float wait)
    {
        while (true)
        {
            ShootRifle();
            yield return new WaitForSeconds(wait);
            ShootRifle();
        }
    }

     private void ShootRifle()
    {
        Transform shotTranform = transform;
        GameObject shot = Instantiate(bullet, shotTranform.position, transform.rotation) as GameObject;
        shot.GetComponent<Rigidbody>().AddForce(shotTranform.forward * WEAPON_FORCE, ForceMode.Impulse);
        Destroy(shot, 0.3f);

        GameObject flash = Instantiate(weaponFlash, shotTranform.position, transform.rotation) as GameObject;
        Destroy(flash, 0.3f);

    }
}
