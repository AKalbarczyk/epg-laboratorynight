using UnityEngine;
using System.Collections;

public class TurretRotationController : MonoBehaviour {


    private GameObject player;
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () 
    {
        transform.LookAt(player.transform.position);
	}
}
