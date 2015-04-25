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
        

	}

    private void CamShake()
    {
        if (!camShakeScript.enabled)
        {
            camShakeScript.enabled = true;
        }
    }
}
