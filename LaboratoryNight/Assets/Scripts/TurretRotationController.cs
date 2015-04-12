using UnityEngine;
using System.Collections;

public class TurretRotationController : MonoBehaviour {


    private GameObject player;
    private bool isPlayerInRange = false;
    private Light light;

	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        light = this.gameObject.GetComponent<Light>();
	}
	
	void Update () 
    {
        if (isPlayerInRange)
        {
            transform.LookAt(player.transform.position);
        }
       
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isPlayerInRange = true;
            light.color = Color.green;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
            light.color = Color.red;
        }
    }
}
