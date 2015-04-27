using UnityEngine;
using System.Collections;

public class MovableController : MonoBehaviour {

    public GameObject shatteredCube;

    private bool isThrown = false;
    private int hitCount = 0;
    private const int MAX_HIT_COUNT = 2;
	void Start () {
	
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

              //  Debug.Log("Movable hit enemy. HitCount: " + hitCount);
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
}
