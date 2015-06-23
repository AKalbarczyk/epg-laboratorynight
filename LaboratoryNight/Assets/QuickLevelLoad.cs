using UnityEngine;
using System.Collections;

public class QuickLevelLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.F1))
        {
            Application.LoadLevel(1);
            WeaponController.shotGunCollected = true;
            WeaponController.laserCollected = false;
        }
        else if (Input.GetKey(KeyCode.F2))
        {
            Application.LoadLevel(2);
            WeaponController.shotGunCollected = true;
            WeaponController.laserCollected = true;
        }
        else if (Input.GetKey(KeyCode.F3))
        {
            Application.LoadLevel(3);
            WeaponController.shotGunCollected = true;
            WeaponController.laserCollected = true;
        }
	}
}
