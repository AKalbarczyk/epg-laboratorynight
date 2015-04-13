using UnityEngine;
using System.Collections;

public class RobotHealth : MonoBehaviour {

    private float health;
    private Rigidbody rigidbody;
    private bool canBeHit = true;
	void Start () 
    {
        health = 1;
        rigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void TakeDamage(float damage)
    {
        Debug.Log("Robot health: " + health);
        this.health -= damage;

        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Movable")
        {
            if (canBeHit)
            {
                StartCoroutine("CannotBeHit");
                TakeDamage(0.3f);
                rigidbody.AddForce(col.gameObject.transform.forward * 3f, ForceMode.Impulse);
            }
        }
    }

    private IEnumerator CannotBeHit()
    {
        if (this.canBeHit)
        {
            this.canBeHit = false;
            yield return new WaitForSeconds(0.3f);
            this.canBeHit = true;
        }
    }

}
