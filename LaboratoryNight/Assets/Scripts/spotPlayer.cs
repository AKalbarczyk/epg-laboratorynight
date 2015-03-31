using UnityEngine;
using System.Collections;

public class SpotPlayer : MonoBehaviour {

	public GameObject weaponFlash;
	public GameObject bullet;
	private const float WEAPON_FORCE = 120;

	private Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider myTrigger) {
		if(myTrigger.gameObject.name == "ArmyPilot"){
			Debug.Log("aa");
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
