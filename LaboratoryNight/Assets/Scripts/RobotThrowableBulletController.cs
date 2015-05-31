using UnityEngine;
using System.Collections;

public class RobotThrowableBulletController : MonoBehaviour {

    private Rigidbody rigidbody;
    private Transform player;
    private bool isFlyingToPlayer = true;

    public GameObject hitEfx;

    private string thrownByTag;

	void Start () 
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("DisableFlyingToPlayer");
	}
	
	void FixedUpdate () 
    {
        if (isFlyingToPlayer)
        {
            transform.LookAt(player);
        }

        rigidbody.AddRelativeForce(Vector3.forward * 60f, ForceMode.Force);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.SendMessage("TakeDamage", 0.2f);
            GameObject efx = Instantiate(hitEfx, transform.position, transform.rotation) as GameObject;
            Destroy(efx, 0.4f);
            Destroy(this.gameObject, 0.1f);
        }

        if (col.gameObject.name.Contains("Plane"))
        {
            GameObject efx = Instantiate(hitEfx, transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.4f);
            Destroy(this.gameObject, 0.1f);
        }

        if (col.gameObject.tag == "Enemy" && col.gameObject != this.gameObject && !thrownByTag.Equals("Enemy")) //prevent self-hit
        {
            col.SendMessage("TakeDamage", 0.5f);
            GameObject efx = Instantiate(hitEfx, transform.position, transform.rotation) as GameObject;
            Destroy(efx, 0.4f);
        }
    }

    private IEnumerator DisableFlyingToPlayer()
    {
        yield return new WaitForSeconds(0.3f);
        isFlyingToPlayer = false;
    }

    public void BulletThrown(string objTag)
    {
        this.thrownByTag = objTag;
    }
}
