using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    //SHAKE VALUES: 5/400/2

    private bool camShake = false;
    

    private CameraShake camShakeScript;


    //OLD (top-down) CAMERA

    private const bool oldCameraEnabled = true;
    private Vector3 cameraTarget;

    private const float CAMERA_Z_OFFSET = 5f;
    private const float CAMERA_X_OFFSET = 0f;
    private Transform target;
    private float camY;
    private Vector3 originalPos;

    private bool camShakeOld = false;
    public float shake = 5f;
    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 400f;
    public float decreaseFactor = 2f;

    void Start()
    {
        camShakeScript = GetComponent<CameraShake>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        camY = transform.position.y;
	}

	void Update () 
    {
        if (oldCameraEnabled)
        {
            originalPos = transform.position;

            CheckCamShake();

            cameraTarget = new Vector3(target.position.x - CAMERA_X_OFFSET, camY, target.position.z - CAMERA_Z_OFFSET);
            transform.position = Vector3.Lerp(transform.position, cameraTarget, Time.deltaTime * 8);
        }
        else
        {
            CheckMouseMovement();
        }
	}

    private void CheckCamShake()
    {
        if (camShakeOld)
        {

            if (shake > 4.5f)
            {
                transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                shake -= Time.deltaTime * decreaseFactor;
            }

            else
            {
                shake = 5f;
                transform.localPosition = originalPos;
                camShakeOld = false;
            }

        }
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
        if (!oldCameraEnabled)
        {
            if (!camShakeScript.enabled)
            {
              //  camShakeScript.enabled = true;
            }
        }
        else
        {
            camShakeOld = true;
        }
    }
}
