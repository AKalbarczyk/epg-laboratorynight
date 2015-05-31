using UnityEngine;
using System.Collections;

public class AddEnergy : MonoBehaviour {

	// Use this for initialization
    public GameObject collectEfx;
    private const float ENERGY_BOOST_VALUE = 0.4f;
    public SoundsController sounds;
	void Start () {
        sounds = GameObject.FindObjectOfType<SoundsController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            col.transform.Find("Weapon").gameObject.SendMessage("UpdateGravGunBar", ENERGY_BOOST_VALUE);
            GameObject obj = Instantiate(collectEfx, transform.position, Quaternion.identity) as GameObject;
            Destroy(obj, 1f);
            this.gameObject.GetComponent<Renderer>().enabled = false;
            sounds.ManaPickUp();
            Destroy(this.gameObject, 1f);   
        }
    }
}
