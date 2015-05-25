using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnlockThirdDoor : MonoBehaviour {

    public Text text;
    bool canPress;

    public GameObject bootEffect;

    // Use this for initialization
    void Start()
    {
        canPress = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (canPress && (Input.GetKeyDown("space")))
        {
            ThirdClosedDoor.isAllowed = true;
            canPress = false;
            GameObject boot = Instantiate(bootEffect, transform.position + transform.up * 4, transform.rotation) as GameObject;
            text.text = "";
        }
    }

    void OnTriggerEnter(Collider target)
    {
        if ((target.gameObject.tag == "Player") && !ThirdClosedDoor.isAllowed)
        {
            text.text = "press space to hack the computer";
            canPress = true;
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {
            text.text = "";
            canPress = false;
        }
    }
}
