using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    private Animation animation;
    private GameObject player;
    private const float ANIMATION_SPEED = 2.5f;
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");

        animation = GetComponent<Animation>();
        animation["Walk"].speed = ANIMATION_SPEED;
        animation["Strafe Left"].speed = ANIMATION_SPEED;
        animation["Strafe Right"].speed = ANIMATION_SPEED;
        animation["Run_backwards"].speed = ANIMATION_SPEED;
        animation["Walk Firing"].speed = ANIMATION_SPEED;
	}
	
	void Update () 
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animation.CrossFade("Walk");
        }
        //if (Input.GetKey(KeyCode.W) )
        //{
        //    animation.CrossFade("Walk");
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    animation.CrossFade("Strafe Left");
        //}
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    animation.CrossFade("Strafe Right");
        //}
        //else if (Input.GetKey(KeyCode.S))
        //{
        //    animation.CrossFade("Run_backwards");
        //}
        else 
        {
            animation.CrossFade("Idle Aim");
        }

        if (Input.GetKey(KeyCode.Mouse0) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))) 
        {
            animation.Play("Walk Firing");
        }

        if (Input.GetKey(KeyCode.Mouse0) && (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)))
        {
            animation.Play("Idle Firing");
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animation.Play("Jump");
        }
	}
}
