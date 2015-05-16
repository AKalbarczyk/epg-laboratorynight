using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {

	// Use this for initialization
    public GameObject explosion;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "RobotBullet")
        {
            Destroy(col.gameObject, 0.2f);
        }

        else if (col.gameObject.tag == "Rocket")
        {
            GameObject obj = Instantiate(explosion, col.transform.position, col.transform.rotation) as GameObject;
            Destroy(obj, 0.7f);
            Destroy(col.gameObject, 0.3f);

        }
    }
}
