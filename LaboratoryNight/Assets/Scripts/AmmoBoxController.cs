using UnityEngine;
using System.Collections;
public class AmmoBoxController : MonoBehaviour {

	// Use this for initialization
    public GameObject efx;

    const string RIFLE_AMMO_BOX_NAME = "rifle_ammo_box";
    const string SHOTGUN_AMMO_BOX_NAME = "shotgun_ammo_box";
    const string LASER_AMMO_BOX_NAME = "laser_ammo_box";

    public enum AmmoBoxType { RIFLE_AMMO, SHOTGUN_AMMO, LASER_AMMO };
    public AmmoBoxType ammoBoxType;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (ammoBoxType == AmmoBoxType.RIFLE_AMMO)
            {
                col.gameObject.transform.Find("Weapon").SendMessage("UpdateRifleAmmo", 10);
            }
            else if (ammoBoxType == AmmoBoxType.SHOTGUN_AMMO)
            {
                col.gameObject.transform.Find("Weapon").SendMessage("UpdateShotgunAmmo", 5);
            }
            else if (ammoBoxType == AmmoBoxType.LASER_AMMO)
            {
                col.gameObject.transform.Find("Weapon").SendMessage("UpdateLaserAmmo", 10);
            }
            else
            {
                Debug.Log("problem on AmmoBoxController collision");
                return;
            }
            
            GameObject obj = Instantiate(efx, transform.position, Quaternion.identity) as GameObject;

            Destroy(obj, 0.5f);
            Destroy(this.gameObject, 0.25f);


        }
    }
}
