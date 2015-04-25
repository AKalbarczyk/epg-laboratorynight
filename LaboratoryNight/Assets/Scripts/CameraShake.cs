using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private Transform camTransform;

    // How long the object should shake for.
    public float shake = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

   // private GameCamera gameCameraScript;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }

        //if (gameCameraScript == null)
        //{
        //    gameCameraScript = GetComponent<GameCamera>();
        //}
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
        shake = 1;
        //shakeAmount = 0.3f;
        //decreaseFactor = 1.9;
    }

    void Update()
    {
        if (shake > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0f;
            camTransform.localPosition = originalPos;
            StartCoroutine("DisableSelf");
        }
    }

    private IEnumerator DisableSelf()
    {
        yield return new WaitForSeconds(0.3f);
        this.enabled = false;
    }
}
