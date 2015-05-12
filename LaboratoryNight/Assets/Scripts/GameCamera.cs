using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    //SHAKE VALUES: 5/400/2

    private bool camShake = false;

    private CameraShake camShakeScript;
    void Start()
    {
        camShakeScript = GetComponent<CameraShake>();
	}

	void Update () 
    {
        CheckMouseMovement();
	}

    private void CheckMouseMovement()
    {
        float mouseMovement = Input.GetAxis("Mouse Y") * Time.deltaTime * 3f;
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + mouseMovement);
        transform.Translate(transform.forward * mouseMovement);

        Mathf.Clamp(transform.localPosition.z, -4, -1);
    }

    private void CamShake()
    {
        if (!camShakeScript.enabled)
        {
            camShakeScript.enabled = true;
        }
    }
}
