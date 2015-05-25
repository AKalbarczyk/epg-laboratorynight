using UnityEngine;
using System.Collections;

public class HealthpackController : MonoBehaviour {

    private const float HEALTH_BOOST = 0.3f;
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
            col.SendMessage("GainHealth", HEALTH_BOOST);
            GameObject obj = Instantiate(efx, transform.position, transform.rotation) as GameObject;
            Destroy(obj, 0.3f);
            Destroy(this.gameObject, 0.1f);
        }
    }
}
