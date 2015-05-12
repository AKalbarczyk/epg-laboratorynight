using UnityEngine;
using System.Collections;

public class CollectWeapon : MonoBehaviour {

	// Use this for initialization
    private GameObject[] allEnemies;
    public WeaponController thisWeapon;
	void Start () 
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");   
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Shotgun")
        {
            thisWeapon.EnableShotgun();
            SendShotgunAmmoDropInfo();
            thisWeapon.UpdateShoutgunAmmo(15);
            Destroy(col.gameObject, 0.1f);
        }

        else if (col.gameObject.name == "Laser")
        {
            thisWeapon.EnableLaser();
            SendLaserAmmoDropInfo();
            thisWeapon.UpdateLaserAmmo(15);
            Destroy(col.gameObject, 0.1f);
        }
    }

    private void SendShotgunAmmoDropInfo()
    {
        foreach (GameObject enemy in allEnemies)
        {
            if (enemy)
            {
                enemy.SendMessage("CanDropShotgunAmmo");
            }
        }
    }
    private void SendLaserAmmoDropInfo()
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
