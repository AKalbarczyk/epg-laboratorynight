using UnityEngine;
using System.Collections;

public class OpenLockedDoor : MonoBehaviour
{
    
    public static bool allowOpen = false;
    private bool isDoorOpening = false;
    private bool isDoorClosing = false;

    private Vector3 basePosition;
    private Vector3 upPosition;

    void Start()
    {
        basePosition = transform.position;
        upPosition = new Vector3(basePosition.x, basePosition.y + 10, basePosition.z);
    }


    void Update()
    {

        if (isDoorOpening && allowOpen)
        {
            transform.position = Vector3.Lerp(transform.position, upPosition, 0.02f);
        }

        if (isDoorClosing)
        {
            transform.position = Vector3.Lerp(transform.position, basePosition, 0.02f);
        }

    }

    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (!isDoorOpening)
            {
                isDoorOpening = true;
                isDoorClosing = false;
            }
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (!isDoorClosing)
            {
                isDoorClosing = true;
                isDoorOpening = false;
            }
        }
    }


}
