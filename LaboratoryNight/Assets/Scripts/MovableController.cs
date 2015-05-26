using UnityEngine;
using System.Collections;

public class MovableController : MonoBehaviour {

    public GameObject shatteredCube;

    private bool isThrown = false;
    private int hitCount = 0;
    private const int MAX_HIT_COUNT = 2;
    private Rigidbody rigidbody;

	void Start () 
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        this.shatteredCube = Resources.Load("ShatteredCube") as GameObject;
	}
	
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (isThrown) //prevent accidental collisions
        {
            if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Rocket")
            {
                hitCount++;

                if (col.gameObject.tag == "Enemy")
                {
                    col.gameObject.SendMessage("ReceiveHit");
                }

                if (hitCount == MAX_HIT_COUNT)
                {
                    GameObject shatter = Instantiate(shatteredCube, transform.position, Quaternion.identity) as GameObject;
                    shatter.transform.Rotate(0, Random.Range(0, 360), 0);
                    shatter.transform.Translate(0, -3.25f, 0);
                    
                    Renderer r;
                    r = this.gameObject.GetComponent<Renderer>();
                    r.enabled = false;

                    
                    Destroy(shatter, 5f);
                    Destroy(this.gameObject, 3f);
                }

                
            }
        }

        isThrown = false;
    }

    public void IsThrown()
    {
       // Debug.Log("IsThrown called");
        isThrown = true;
    }

    private void InvokedRemoveForces()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
    public void RemoveForces()
    {
        Invoke("InvokedRemoveForces", 1.5f);
    }
}
