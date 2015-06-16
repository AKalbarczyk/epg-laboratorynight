using UnityEngine;
using System.Collections;

public class BossRocketController : MonoBehaviour {

    public GameObject explosion;
    private const float DAMAGE = 0.2f;

    private bool canBeDestroyed = true;
    private bool isMovingToPlayer = true;
    private GameObject player;
    private Rigidbody rigidbody;
    private SoundsController sounds;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = GetComponent<Rigidbody>();
        sounds = GameObject.FindObjectOfType<SoundsController>();
    }

    void Update()
    {
        //if (isMovingToPlayer)
        //{
        //    Vector3 direction = (player.transform.position - transform.position).normalized;
        //    rigidbody.MovePosition(transform.position + direction * 20 * Time.deltaTime);
        //}

        //DisableMoveToPlayer();
    }

    private void DisableMoveToPlayer()
    {
        if (isMovingToPlayer)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 5)
            {
                this.isMovingToPlayer = false;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject, 0.1f);
            col.gameObject.SendMessage("TakeDamage", DAMAGE);

            GameObject efx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.2f);

            sounds.RocketHit();
        }


        else if (col.gameObject.name.Contains("Turret"))
        {
            GameObject efx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.5f);
            Destroy(col.gameObject);
            Destroy(this.gameObject, 0.1f);
        }

        else if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.SendMessage("TakeDamage", DAMAGE + 0.2f);

            GameObject efx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.5f);
            Destroy(this.gameObject, 0.1f);

            sounds.RocketHit();
        }
        else if (col.gameObject.name.Contains("Cube"))
        {
            GameObject efx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.5f);
            Destroy(this.gameObject, 0.1f);

            sounds.RocketHit();
        }

        else if (col.gameObject.tag == "Wall")
        {
            GameObject efx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.3f);
            Destroy(this.gameObject, 0.1f);

            sounds.RocketHit();
        }
        else if (col.gameObject.name.Contains("Plane"))
        {
            GameObject efx = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(efx, 0.5f);
            Destroy(this.gameObject, 0.1f);

            sounds.RocketHit();
        }

        

        

    }

}
