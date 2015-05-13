using UnityEngine;
using System.Collections;

public class RobotHealth : MonoBehaviour {

    public GameObject manaPowerUp;
    public GameObject rifleAmmo;
    public GameObject shotgunAmmo;
    public GameObject laserAmmo;

    private bool canDropShotgunAmmo = false;
    private bool canDropLaserAmmo = false;

    private float health;
    private Rigidbody rigidbody;
    private bool canBeHit = true;

    public GameObject explosion;
    public GameObject onHit;

    private Transform player;

	void Start () 
    {
        health = 1;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        manaPowerUp = Resources.Load("powerUp") as GameObject;
        rifleAmmo = Resources.Load("ammo_box_rifle") as GameObject;
        shotgunAmmo = Resources.Load("ammo_box_shotgun") as GameObject;
        laserAmmo = Resources.Load("ammo_box_laser") as GameObject;

        onHit = Resources.Load("Explosion02") as GameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void CanDropShotgunAmmo()
    {
        this.canDropShotgunAmmo = true;
    }

    public void CanDropLaserAmmo()
    {
        this.canDropLaserAmmo = true;
    }

    void TakeDamage(float damage)
    {
        this.health -= damage;

        
        if (this.health <= 0)
        {
            GameObject efx = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            Destroy(efx, 0.4f);

            if (Random.Range(0.0F, 1.0F) >= 0.7) //30% drop chance 
            {
                GameObject obj = Instantiate(manaPowerUp, transform.position, transform.rotation) as GameObject;
            }

            if (Random.Range(0.0F, 1.0F) >= 0.2) //80%
            {
                GameObject obj2 = Instantiate(rifleAmmo, transform.position + transform.right * 2, transform.rotation) as GameObject;
                
            }

            if (canDropShotgunAmmo)
            {
                if (Random.Range(0.0F, 1.0F) >= 0.5) //50%
                {
                    GameObject obj2 = Instantiate(shotgunAmmo, transform.position + transform.right * 4, transform.rotation) as GameObject;
                    return;
                }
            }
            if (canDropLaserAmmo)
            {
                if (Random.Range(0.0F, 1.0F) >= 0.5) //50%
                {
                    GameObject obj2 = Instantiate(laserAmmo, transform.position +  transform.right * -2, transform.rotation) as GameObject;
                    return;
                }
            }
            
            Destroy(this.gameObject);

        }
    }

    public void ReceiveHit()
    {
        TakeDamage(0.6f);
        GameObject efx = Instantiate(onHit, transform.position + (transform.up * 2), transform.rotation) as GameObject;
        Destroy(efx, 0.4f);
        //rigidbody.AddForce(col.gameObject.transform.forward * 5f, ForceMode.Impulse);
        StartCoroutine("RemoveForces");
    }

    private IEnumerator CannotBeHit()
    {
       
        this.canBeHit = false;
        yield return new WaitForSeconds(0.5f);
        this.canBeHit = true;
        
    }

    private IEnumerator RemoveForces()
    {
        yield return new WaitForSeconds(1f);
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        transform.LookAt(player);
    }

}
