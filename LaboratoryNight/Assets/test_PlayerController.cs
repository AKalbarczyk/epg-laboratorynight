using UnityEngine;
using System.Collections;

public class test_PlayerController : MonoBehaviour {

	// Use this for initialization

	private float mouseTreshold = 0.3f;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.W)) {
			transform.Translate(transform.forward * 1f);
		}

		if (Input.GetKey(KeyCode.A)) {
			transform.Translate(-transform.right * 1f);
		}

		if (Input.GetKey(KeyCode.S)) {
			transform.Translate(-transform.forward * 1f);
		}

		if (Input.GetKey(KeyCode.D)) {
			transform.Translate(transform.right * 1f);
		}

		if (Input.GetAxis ("Mouse X") > mouseTreshold || Input.GetAxis ("Mouse X") < -mouseTreshold)
			transform.Rotate (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Input.GetAxis("Mouse X"), transform.eulerAngles.z)); 

	
	}
}
