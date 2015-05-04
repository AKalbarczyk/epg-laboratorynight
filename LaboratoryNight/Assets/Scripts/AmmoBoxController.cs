using UnityEngine;
using System.Collections;

public class AmmoBoxController : MonoBehaviour {

	// Use this for initialization
    public GameObject efx;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.Find("Weapon").SendMessage("AddAmmo", 10);
            GameObject obj = Instantiate(efx, transform.position, transform.rotation) as GameObject;
            Destroy(obj, 0.5f);
            Destroy(this.gameObject, 0.5f);
        }
    }
}
