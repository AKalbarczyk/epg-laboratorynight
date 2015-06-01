using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnlockSecondDoor : MonoBehaviour {
    public Text text;
    bool canPress;

    public GameObject bootEffect;
    public SoundsController sounds;
    // Use this for initialization
    void Start()
    {
        canPress = false;
        sounds = GameObject.FindObjectOfType<SoundsController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (canPress && (Input.GetKeyDown("space")))
        {
            SecondClosedDoor.isAllowed = true;
            SlidingDoorBlocked.unlockDoor = true;
            canPress = false;
            GameObject boot = Instantiate(bootEffect, transform.position + transform.up * 4, transform.rotation) as GameObject;
            text.text = "";
            sounds.HackComputer();
        }
    }

    void OnTriggerEnter(Collider target)
    {
        if ((target.gameObject.tag == "Player") && !SecondClosedDoor.isAllowed)
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
