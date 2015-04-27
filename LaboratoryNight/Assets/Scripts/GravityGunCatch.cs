using UnityEngine;
using System.Collections;

public class GravityGunCatch : MonoBehaviour {

    private ArrayList colliders = new ArrayList();

    private Rigidbody caughtRigidbody;
    private GameObject caughtObject;

    private const float OBJECT_HOLD_OFFSET = 2.5f;

    public GameObject gravityCatchEffect; 
    private GameObject gravityCatchEffectObj;

    public GameObject gravityCatchFailEffect;

    private bool isObjectGoingToPlayer = false;
    private bool isObjectPickedUp = false;
	void Start () 
    {
	
	}
	
	void Update () 
    {

        if (!caughtObject && gravityCatchEffectObj)
        {
            isObjectGoingToPlayer = false;
            isObjectPickedUp = false;
            DestroyGravityCatchEffect();
            colliders.Clear();
        }

        if (isObjectGoingToPlayer)
        {
            StartCoroutine("DisableObjectGoingToPlayer");
            if (caughtObject)
            {
                caughtObject.transform.position = Vector3.Lerp(caughtObject.transform.position, transform.position + transform.forward * OBJECT_HOLD_OFFSET, Time.deltaTime * 5f);
            }
        }

        if (!isObjectPickedUp)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Collider c = GetNearestCollider();
                if (c)
                {
                    isObjectPickedUp = true;
                    caughtRigidbody = c.GetComponent<Rigidbody>();
                    caughtRigidbody.isKinematic = true;
                    caughtObject = caughtRigidbody.gameObject;

                    isObjectGoingToPlayer = true;
                    caughtObject.transform.parent = this.transform;

                    InitGravityCatchEffect();
                }
                else
                {
                    colliders.Clear();
                    GameObject failEfx = Instantiate(gravityCatchFailEffect, transform.position, transform.rotation) as GameObject;
                    failEfx.transform.parent = transform;
                    Destroy(failEfx, 0.4f);
                }


            }
        }
        else //object picked up, ready to throw
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                colliders.Clear();

                if (caughtRigidbody) //just in case...
                {
                    caughtRigidbody.gameObject.SendMessage("IsThrown");

                    isObjectGoingToPlayer = false;
                    DestroyGravityCatchEffect();
                    caughtRigidbody.isKinematic = false;
                    caughtRigidbody.transform.parent = null;
                    caughtRigidbody.AddForce(transform.forward * 50f, ForceMode.Impulse);
                    caughtRigidbody = null;
                    isObjectPickedUp = false;
                }
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Rigidbody>() && col.gameObject.tag != "Enemy") //what can be caught?
        {
            colliders.Add(col);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (colliders.Contains(col))
        {
            colliders.Remove(col);
        }
    }

    private void InitGravityCatchEffect()
    {
        gravityCatchEffectObj = Instantiate(gravityCatchEffect, transform.position + transform.forward * OBJECT_HOLD_OFFSET, transform.rotation) as GameObject;
        gravityCatchEffectObj.transform.parent = transform;
    }

    private Collider GetNearestCollider()
    {
        float minDist = 9999, currDist = 9999;
        Collider nearest = null;
        Vector3 myPosition = transform.position;

        foreach (Collider c in colliders)
        {
            if (c)
            {
                currDist = Vector3.Distance(myPosition, c.gameObject.transform.position);
                if (currDist < minDist)
                {
                    minDist = currDist;
                    nearest = c;
                }
            }

        }
        return nearest;
    }

    private IEnumerator DisableObjectGoingToPlayer()
    {
        yield return new WaitForSeconds(1f);

        if (isObjectGoingToPlayer)
        {
            isObjectGoingToPlayer = false;
        }
    }

    private void DestroyGravityCatchEffect()
    {
        Destroy(gravityCatchEffectObj);
    }


}
