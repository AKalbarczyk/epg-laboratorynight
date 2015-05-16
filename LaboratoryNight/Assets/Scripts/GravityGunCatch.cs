using UnityEngine;
using System.Collections;

public class GravityGunCatch : MonoBehaviour {

    public WeaponController weaponController;

    private const float GRAVGUN_CATCH_VALUE = 0.1f;

    private ArrayList colliders = new ArrayList();

    private Rigidbody caughtRigidbody;
    private GameObject caughtObject;

    private const float OBJECT_HOLD_OFFSET = 2.5f;

    public GameObject gravityCatchEffect; 
    private GameObject gravityCatchEffectObj;

    public GameObject gravityCatchFailEffect;

    private bool isObjectGoingToPlayer = false;
    private bool isObjectPickedUp = false;

    private bool isIncreasingThrowPower = false;
    private float throwPower = 0;

    public GameObject gravityGunCatchTrace;
	void Start () 
    {
	
	}
	
	void Update () 
    {
        if (isIncreasingThrowPower)
        {
            IncreaseThrowPower();
        }

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
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                StartCoroutine("ActivateGravityGunCatchTrace");

                Collider c = GetNearestCollider();
                if (c && weaponController.GetGravGunValue() > GRAVGUN_CATCH_VALUE)
                {
                    
                    isObjectPickedUp = true;
                    caughtRigidbody = c.GetComponent<Rigidbody>();
                    caughtRigidbody.isKinematic = true;
                    caughtObject = caughtRigidbody.gameObject;

                    isObjectGoingToPlayer = true;
                    caughtObject.transform.parent = this.transform;

                    weaponController.UpdateGravGunBar(-GRAVGUN_CATCH_VALUE);
                    InitGravityCatchEffect();
                    isIncreasingThrowPower = true;
                }
                else
                {
                    colliders.Clear();
                    GameObject failEfx = Instantiate(gravityCatchFailEffect, transform.position, transform.rotation) as GameObject;
                    failEfx.transform.parent = transform;
                    Destroy(failEfx, 0.4f);
                    throwPower = 0;
                    isIncreasingThrowPower = false;
                }


            }
        }
        else //object picked up, ready to throw
        {
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                isIncreasingThrowPower = false;
                colliders.Clear();

                if (caughtRigidbody) //just in case...
                {
                    if (caughtRigidbody.tag == "Movable")
                    {
                        caughtRigidbody.gameObject.SendMessage("IsThrown");
                    }

                    isObjectGoingToPlayer = false;
                    DestroyGravityCatchEffect();
                    caughtRigidbody.isKinematic = false;
                    caughtRigidbody.transform.parent = null;

                    if (throwPower > 50)
                    {
                        throwPower = 50;
                    }

                    if (caughtRigidbody.tag == "Movable")
                    {
                        caughtRigidbody.AddForce(transform.forward * throwPower, ForceMode.Impulse);
                    }
                    else
                    {
                        caughtRigidbody.AddForce(transform.forward * 50, ForceMode.Impulse);
                    }
                    caughtRigidbody = null;
                    isObjectPickedUp = false;
                    throwPower = 0;
                    isIncreasingThrowPower = false;
                }
            }
        }
	}

    private IEnumerator ActivateGravityGunCatchTrace()
    {
        if (!this.gravityGunCatchTrace.activeSelf)
        {
            this.gravityGunCatchTrace.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            this.gravityGunCatchTrace.SetActive(false);
        }
    }
    private void IncreaseThrowPower()
    {
        if (Input.GetButton("Fire2"))
        {
            throwPower += Time.deltaTime;
            throwPower *= 1.2f;
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
