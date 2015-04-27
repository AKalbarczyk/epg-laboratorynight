using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour 
{
    private float rotationSpeed = 15;
    private const float ROTATION_SPEED = 50;
	private float walkSpeed = 10;
	private float runSpeed = 8;

    private const float WALK_FORWARD_SPEED = 20;
    private const float WALK_L_R_SPEED = WALK_FORWARD_SPEED / 16f;

	private Quaternion targetRotation;

	private CharacterController controller;

	void Start () 
    {
		controller = GetComponent<CharacterController>();
	}

	void Update () 
    {

       // ApplyMovement();
       // ApplyRotation();
        ApplyMovementThirdPerson();
        ApplyRotationThirdPerson();


	}

    private void ApplyMovementThirdPerson()
    {
        float translation = Input.GetAxis("Vertical") * WALK_FORWARD_SPEED;
        float translationLeftRight = Input.GetAxis("Horizontal") * WALK_L_R_SPEED;
        
        controller.Move((transform.right * translationLeftRight) + (transform.forward * translation) * Time.deltaTime);
    }

    private void ApplyRotationThirdPerson()
    {
        float rotation = Input.GetAxis("Mouse X") * ROTATION_SPEED;
        rotation *= Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }

    private void ApplyMovement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Vector3 motion = input;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        motion *= walkSpeed;
        //motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        controller.Move(motion * Time.deltaTime);

        //controller.Move(transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * walkSpeed);
    }


    /**
     * source: http://wiki.unity3d.com/index.php?title=LookAtMouse 
     * 
     */
    private void ApplyRotation()
    {

        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
