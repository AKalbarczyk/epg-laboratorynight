using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour 
{
    private float rotationSpeed = 15;
    private const float ROTATION_SPEED = 50;
	private float walkSpeed = 10;
	private float runSpeed = 8;

    private const float WALK_FORWARD_SPEED = 10;
    private const float WALK_L_R_SPEED = WALK_FORWARD_SPEED / (2*WALK_FORWARD_SPEED);

	private Quaternion targetRotation;

	private CharacterController controller;

    private Vector3 startPosition;

    private const bool oldCameraEnabled = true;

	void Start () 
    {
		controller = GetComponent<CharacterController>();
        Invoke("GetPositionY", 1f);
	}

    private void GetPositionY()
    {
        startPosition = transform.position;
    }

	void Update () 
    {
        if (oldCameraEnabled)
        {
            ApplyMovement();
            ApplyRotation();
        }
        else
        {
            ApplyMovementThirdPerson();
            ApplyRotationThirdPerson();
        }
        RestrictPosition();

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

    private void RestrictPosition()
    {
        if (startPosition != null)
        {
            if (transform.position.y > startPosition.y)
            {
                transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
            }
        }
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
