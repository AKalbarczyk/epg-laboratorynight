using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecondClosedDoor : MonoBehaviour {

    public Text text;
    private bool isDoorOpening = false;
    private bool isDoorClosing = false;

    public static bool isAllowed;

    private Vector3 basePosition;
    private Vector3 upPosition;

    public SoundsController sounds;

    void Start()
    {

        basePosition = transform.position;
        upPosition = new Vector3(basePosition.x, basePosition.y + 10, basePosition.z);
        sounds = GameObject.FindObjectOfType<SoundsController>();
    }


    void Update()
    {

        if (isDoorOpening)
        {
            transform.position = Vector3.Lerp(transform.position, upPosition, Time.deltaTime * 3f);
        }

        if (isDoorClosing)
        {
            transform.position = Vector3.Lerp(transform.position, basePosition, 0.02f);
        }

    }

    void OnTriggerEnter(Collider target)
    {
        if ((target.gameObject.tag == "Player" && isAllowed) || target.gameObject.tag == "Enemy")
        {
            if (!isDoorOpening)
            {
                isDoorOpening = true;
                isDoorClosing = false;
            }
        }
        if (target.gameObject.tag == "Player" && !isAllowed)
        {
            text.text = "you must unlock the second first";
            Debug.Log("test");
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.gameObject.tag == "Player" || target.gameObject.tag == "Enemy")
        {
            if (!isDoorClosing)
            {
                isDoorClosing = true;
                isDoorOpening = false;
            }

        }
        if (target.gameObject.tag == "Player")
            text.text = "";
    }
}
