using UnityEngine;
using System.Collections;

public class MiniMapFollow : MonoBehaviour {

    public Transform target;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
	}
}
