using UnityEngine;
using System.Collections;

public class CollectWeapon : MonoBehaviour {

	// Use this for initialization
    private GameObject[] allEnemies;
    public WeaponController thisWeapon;
    public GameObject collectEfx;

    public SoundsController sounds;
	void Start () 
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        sounds = GameObject.FindObjectOfType<SoundsController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Shotgun")
        {
            thisWeapon.EnableShotgun();
            WeaponController.shotGunCollected = true;
            SendShotgunAmmoDropInfo();
            thisWeapon.UpdateShotgunAmmo(10);

            GameObject efx = Instantiate(collectEfx, col.transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.3f);

            sounds.CollectWeapon();

            Destroy(col.gameObject, 0.1f);
        }

        else if (col.gameObject.name == "Laser")
        {
            WeaponController.shotGunCollected = true;
            thisWeapon.EnableLaser();
            SendLaserAmmoDropInfo();
            thisWeapon.UpdateLaserAmmo(15);

            GameObject efx = Instantiate(collectEfx, col.transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.3f);

            sounds.CollectWeapon();

            Destroy(col.gameObject, 0.1f);
        }
    }

    public void SendShotgunAmmoDropInfo()
    {
        foreach (GameObject enemy in allEnemies)
        {
            if (enemy)
            {
                enemy.SendMessage("CanDropShotgunAmmo");
            }
        }
    }
    public void SendLaserAmmoDropInfo()
    {
        foreach (GameObject enemy in allEnemies)
        {
            if (enemy)
            {
                enemy.SendMessage("CanDropLaserAmmo");
            }
        }
    }
}
