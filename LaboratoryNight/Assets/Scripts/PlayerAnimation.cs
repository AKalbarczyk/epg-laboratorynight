using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    private Animation animation;
	void Start () 
    {
        animation = GetComponent<Animation>();
	}
	
	void Update () 
    {
        if (Input.GetKey(KeyCode.W))
        {
            animation.CrossFade("Walk");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animation.CrossFade("Strafe Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animation.CrossFade("Strafe Right");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animation.CrossFade("Run_backwards");
        }
        else 
        {
            animation.CrossFade("Idle");
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            animation.CrossFade("Idle Firing");
        }
	}
}
