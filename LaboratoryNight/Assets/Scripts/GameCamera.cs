using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	private Vector3 cameraTarget;
    private const float CAMERA_Z_OFFSET = 0.2f;
	private Transform target;
    private float camY;

    private bool camShake = false;
    public float shake = 5f;
    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 400f;
    public float decreaseFactor = 2f;
    private Vector3 originalPos; 

    void Start()
    {
		target = GameObject.FindGameObjectWithTag("Player").transform;
        camY = transform.position.y;
	}

	void Update () 
    {
        originalPos = transform.position;

        CheckCamShake();

        cameraTarget = new Vector3(target.position.x, camY, target.position.z - CAMERA_Z_OFFSET);
		transform.position = Vector3.Lerp(transform.position,cameraTarget,Time.deltaTime * 8);
	}

    private void CheckCamShake()
    {
        if (camShake)
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
                camShake = false;
            }

        }
    }

    private void CamShake()
    {
        camShake = true;
    }
}
