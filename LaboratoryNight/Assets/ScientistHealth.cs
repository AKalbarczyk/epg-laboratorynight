using UnityEngine;
using System.Collections;

public class ScientistHealth : MonoBehaviour {
    
    private float health;
    private Rigidbody rigidbody;
    private bool canBeHit = true;

    public GameObject powerUp, powerUp2;
    public GameObject explosion;

    public GameObject onHit;

    private Transform player;

    void Start()
    {
        health = 1;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TakeDamage(float damage)
    {
        this.health -= (damage*2);


        if (this.health <= 0)
        {     
            GameObject obj;
            GameObject obj2;
            GameObject efx = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            Destroy(efx, 0.4f);

            if (Random.Range(0.0F, 1.0F) >= 0.45)
            obj = Instantiate(powerUp, transform.position + transform.up * 2 + transform.right *2, transform.rotation) as GameObject;
            
            if(Random.Range(0.0F,1.0F) >= 0.7) 
                obj2 = Instantiate(powerUp2, transform.position + transform.up * 2, transform.rotation) as GameObject;
            Destroy(this.gameObject, 0.05f);

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
