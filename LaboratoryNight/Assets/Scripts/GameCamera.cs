using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	private Vector3 cameraTarget;
    private const float CAMERA_Z_OFFSET = 0.2f;
	private Transform target;

    void Start()
    {
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update () {
        cameraTarget = new Vector3(target.position.x, transform.position.y, target.position.z - CAMERA_Z_OFFSET);
		transform.position = Vector3.Lerp(transform.position,cameraTarget,Time.deltaTime * 8);
	}
}
