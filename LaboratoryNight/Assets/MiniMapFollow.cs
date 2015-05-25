using UnityEngine;
using System.Collections;

public class MiniMapFollow : MonoBehaviour {

    public Transform target;
    public GameObject camera;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
	}

    
}
