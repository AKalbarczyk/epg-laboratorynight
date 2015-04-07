using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public GameObject weaponFlash;
    public GameObject bullet;
    public GameObject gravityGunShot;
    public GameObject gravityCatchEffect; private GameObject gravityCatchEffectObj;
    public GameObject gravityCatchFailEffect;
    public GameObject pullEffect; private GameObject pullObj; private bool isPullObjMoving = false; private Vector3 initPosition; private bool isPullWorking;
    public GameObject shockwaveEffect; private GameObject shockwaveObj; const int SHOCKWAVE_COUNT = 8;
    private Collider[] colsTrappedInPull;
    public LayerMask layerMask = -1;

    private const float WEAPON_FORCE = 120;
    private bool isShooting = false;

    
    private bool isObjectPickedUp = false;
    private bool isObjectGoingToPlayer = false;

    private float gravityGunRange = 15;
    private Rigidbody caughtRigidbody;
    private GameObject caughtObject;
    private Transform caughtRigidbodyTransformParent;
    private const float OBJECT_HOLD_OFFSET = 5f;
   
    private int gravityGunShootCount = 0;
    private const int MAX_GRAVITY_GUN_SHOOT_COUNT = 10;

    private enum WeaponMode { STANDARD, PULL, SHOCKWAVE }
    private WeaponMode currWeaponMod = WeaponMode.STANDARD;
    private bool canUseSpecialMode = true;

	void Start ()
    {
	
	}
	
	void Update () 
    {

        if (isObjectGoingToPlayer)
        {
            StartCoroutine("DisableObjectGoingToPlayer");
            caughtObject.transform.position = Vector3.Lerp(caughtObject.transform.position, transform.position + transform.forward * OBJECT_HOLD_OFFSET, Time.deltaTime * 5f);
        }

        if (isPullObjMoving)
        {
            StartCoroutine("DisablePullObjMoving");
            pullObj.transform.position = Vector3.Lerp(pullObj.transform.position, initPosition + pullObj.transform.forward * 15f, 0.05f);
        }

        if (isPullWorking)
        {
            if (colsTrappedInPull != null)
            {
                foreach (Collider c in colsTrappedInPull)
                {
                    if (c && pullObj && c.gameObject.tag == "Movable")
                    {
                        c.gameObject.transform.position = Vector3.Lerp(c.gameObject.transform.position, pullObj.transform.position, 0.07f);
                    }
                }
            }
        }

        CheckWeaponMode();

        if (Input.GetAxisRaw("Fire1") != 0)
        {
            if (!isShooting)
            {
                if (currWeaponMod == WeaponMode.STANDARD)
                {
                    ShootGravityGun();
                }

                else if (currWeaponMod == WeaponMode.PULL && canUseSpecialMode)
                {
                    GravityGunPull();
                }

                else if (currWeaponMod == WeaponMode.SHOCKWAVE && canUseSpecialMode)
                {
                    GravityGunShockwave();
                }

                isShooting = true;
            }
        }

        else
        {
            isShooting = false;
        }

        GravityGunCatch();

	}

    private IEnumerator DisableObjectGoingToPlayer()
    {
        yield return new WaitForSeconds(1f);

        if (isObjectGoingToPlayer)
        {
            isObjectGoingToPlayer = false;
        }
    }

    private IEnumerator DisablePullObjMoving()
    {
        colsTrappedInPull = null;
        yield return new WaitForSeconds(1f);
        isPullObjMoving = false;

        colsTrappedInPull = Physics.OverlapSphere(pullObj.transform.position, 5f);
        isPullWorking = true;
    }

    private void DisablePullObj()
    {
        Destroy(pullObj, 7f);
    }

    private void CheckWeaponMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currWeaponMod = WeaponMode.STANDARD;
            Debug.Log("WeaponMode: STANDARD");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currWeaponMod = WeaponMode.PULL;
            Debug.Log("WeaponMode: PULL");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currWeaponMod = WeaponMode.SHOCKWAVE;
            Debug.Log("WeaponMode: SHOCKWAVE");
        }
    }

    private void GravityGunPull()
    {
        initPosition = transform.position;
        pullObj = Instantiate(pullEffect, initPosition, transform.rotation) as GameObject;
        DisablePullObj();
        StartCoroutine("WeaponModeCooldown");
        isPullObjMoving = true;
    }

    private IEnumerator WeaponModeCooldown()
    {
        canUseSpecialMode = false;
        yield return new WaitForSeconds(3f);
        canUseSpecialMode = true;
    }

    private void GravityGunShockwave()
    {
        StartCoroutine("WeaponModeCooldown");
        StartCoroutine("WaitAndFireShockwave");
    }

    private IEnumerator WaitAndFireShockwave()
    {

        initPosition = transform.position;
        Vector3 forward = transform.forward;

        for (int i = 1; i < SHOCKWAVE_COUNT; i++)
        {
            shockwaveObj = Instantiate(shockwaveEffect, initPosition + forward * i * 7, Quaternion.identity) as GameObject; //Vector3.forward behaves strange
            Destroy(shockwaveObj, 0.4f);
            yield return new WaitForSeconds(0.2f);

        }
    }
    private void GravityGunCatch()
    {
        if (!isObjectPickedUp)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, gravityGunRange, layerMask))
                {
                    if (hit.rigidbody) //did RaycastHit hit any rigidbody? (TODO: narrow down to rigidbodies tagged with Movable)
                    {

                        

                        isObjectPickedUp = true;
                        caughtRigidbody = hit.rigidbody;
                        caughtRigidbody.isKinematic = true;
                        caughtObject = caughtRigidbody.gameObject;

                        //caughtRigidbodyTransformParent = caughtRigidbody.transform.parent;
                        //caughtObject.transform.position = transform.position + transform.forward * OBJECT_HOLD_OFFSET;
                        isObjectGoingToPlayer = true;
                        caughtObject.transform.parent = this.transform;

                        InitGravityCatchEffect();

                    }
                }

                else
                {
                    GameObject failEfx = Instantiate(gravityCatchFailEffect, transform.position, transform.rotation) as GameObject;
                    failEfx.transform.parent = transform;
                    Destroy(failEfx, 0.4f);
                }
            }


        }
        else //object picked up, ready to throw
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (caughtRigidbody) //just in case...
                {
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

    private void InitGravityCatchEffect()
    {
        gravityCatchEffectObj = Instantiate(gravityCatchEffect, transform.position + transform.forward * OBJECT_HOLD_OFFSET, transform.rotation) as GameObject;
        gravityCatchEffectObj.transform.parent = transform;
    }

    private void DestroyGravityCatchEffect()
    {
        Destroy(gravityCatchEffectObj);
    }
   private void ShootRifle()
   {
       Transform shotTranform = transform;
       GameObject shot = Instantiate(bullet, shotTranform.position, transform.rotation) as GameObject;
       shot.GetComponent<Rigidbody>().AddForce(shotTranform.forward * WEAPON_FORCE, ForceMode.Impulse);
       Destroy(shot, 0.3f);

       GameObject flash = Instantiate(weaponFlash, shotTranform.position, transform.rotation) as GameObject;
       Destroy(flash, 0.1f);
       
   }

   private void ShootGravityGun()
   {
       gravityGunShootCount++;
       Transform shotTranform = transform;
       GameObject shot = Instantiate(gravityGunShot, shotTranform.position, transform.rotation) as GameObject;
       shot.GetComponent<Rigidbody>().AddForce(shotTranform.forward * (WEAPON_FORCE - 100), ForceMode.Impulse);
       StartCoroutine("GravityGunEffect", shot);
       
       Destroy(shot, 1.2f);
   }

   private IEnumerator GravityGunEffect(GameObject shot)
   {
       yield return new WaitForSeconds(0.32f);
       shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * WEAPON_FORCE, ForceMode.Impulse);
   }

   private IEnumerator GravityGunWait()
   {
       isShooting = true;
       yield return new WaitForSeconds(0.5f);
       isShooting = false;
   }

   private IEnumerator GravityGunCooldown()
   {
       isShooting = true;
       yield return new WaitForSeconds(3f);
       gravityGunShootCount = 0;
       isShooting = false;
   }
}
