using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public GUIBarScript gravGunBar;
    private const float GRAVGUN_INIT_VALUE = 0.3f;
    private const float GRAVGUN_RECHARGE_VALUE = 0.01f;
    private float gravGunValue = GRAVGUN_INIT_VALUE;
    private const float GRAVGUN_SHOOT_VALUE = 0.05f;
    private const float GRAVGUN_PULL_VALUE = 0.6f;
    private const float GRAVGUN_SHOCKWAVE_VALUE = 0.8f;
    
    public Text weaponModeText;
    private const string SHOOT_TEXT = "GravityGun Mode: Shoot";
    private const string PULL_TEXT = "GravityGun Mode: Black Hole";
    private const string SHOCKWAVE_TEXT = "GravityGun Mode: Shockwave";

    public GameObject weaponFlash;
    public GameObject bullet;
    public GameObject gravityGunShot;
    public GameObject pullEffect; private GameObject pullObj; private bool isPullObjMoving = false; private Vector3 initPosition; private bool isPullWorking;
    public GameObject shockwaveEffect; private GameObject shockwaveObj; const int SHOCKWAVE_COUNT = 8;
    private Collider[] colsTrappedInPull;

    private const float WEAPON_FORCE = 120;
    private bool isShooting = false;

    private int gravityGunShootCount = 0;
    private const int MAX_GRAVITY_GUN_SHOOT_COUNT = 10;


    private enum WeaponMode { STANDARD, PULL, SHOCKWAVE }
    private WeaponMode currWeaponMod = WeaponMode.STANDARD;
    private bool canUseSpecialMode = true;

	void Start ()
    {
        gravGunBar.SetNewValue(GRAVGUN_INIT_VALUE);
        InvokeRepeating("RegenerateGravGun", 0.5f, 0.5f);

        weaponModeText.text = SHOOT_TEXT;
	}
	
	void Update () 
    {

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
                    if (c && pullObj && (c.gameObject.tag == "Movable" || c.gameObject.tag == "Enemy"))
                    {
                        c.gameObject.transform.position = Vector3.Lerp(c.gameObject.transform.position, pullObj.transform.position, 0.07f);
                    }
                }
            }
        }

        CheckWeaponMode();

        if (Input.GetAxisRaw("Fire2") != 0)
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
	}

    private void UpdateGravGunBar(float value)
    {
        this.gravGunValue += value;
        this.gravGunBar.SetNewValue(gravGunValue);
    }

    private void RegenerateGravGun()
    {
        if (gravGunValue < GRAVGUN_INIT_VALUE)
        {
            UpdateGravGunBar(GRAVGUN_RECHARGE_VALUE);
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
            weaponModeText.text = SHOOT_TEXT;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currWeaponMod = WeaponMode.PULL;
            Debug.Log("WeaponMode: PULL");
            weaponModeText.text = PULL_TEXT;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currWeaponMod = WeaponMode.SHOCKWAVE;
            Debug.Log("WeaponMode: SHOCKWAVE");
            weaponModeText.text = SHOCKWAVE_TEXT;
        }
    }

    private void GravityGunPull()
    {
        if (gravGunValue >= GRAVGUN_PULL_VALUE)
        {
            UpdateGravGunBar(-GRAVGUN_PULL_VALUE);
            initPosition = transform.position;
            pullObj = Instantiate(pullEffect, initPosition, transform.rotation) as GameObject;
            DisablePullObj();
            StartCoroutine("WeaponModeCooldown");
            isPullObjMoving = true;
        }
    }

    private IEnumerator WeaponModeCooldown()
    {
        canUseSpecialMode = false;
        yield return new WaitForSeconds(3f);
        canUseSpecialMode = true;
    }

    private void GravityGunShockwave()
    {
        if (gravGunValue >= GRAVGUN_SHOCKWAVE_VALUE)
        {
            UpdateGravGunBar(-GRAVGUN_SHOCKWAVE_VALUE);

            StartCoroutine("WeaponModeCooldown");
            StartCoroutine("WaitAndFireShockwave");
        }
    }

    private IEnumerator WaitAndFireShockwave()
    {

        initPosition = transform.position;
        Vector3 forward = transform.forward;

        for (int i = 1; i < SHOCKWAVE_COUNT; i++)
        {
            shockwaveObj = Instantiate(shockwaveEffect, initPosition + forward * i * 7, Quaternion.identity) as GameObject; //Vector3.forward behaves strange
        /*zmiana*/       shockwaveObj.GetComponent<Rigidbody>().AddForce(forward * (WEAPON_FORCE - 100), ForceMode.Impulse);
        
            StartCoroutine("GravityGunEffect", shockwaveObj);
            Destroy(shockwaveObj, 0.4f);
            yield return new WaitForSeconds(0.2f);

        }
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
       if (gravGunValue >= GRAVGUN_SHOOT_VALUE)
       {
           UpdateGravGunBar(-GRAVGUN_SHOOT_VALUE);

           gravityGunShootCount++;
           Transform shotTranform = transform;
           GameObject shot = Instantiate(gravityGunShot, shotTranform.position, transform.rotation) as GameObject;
           shot.GetComponent<Rigidbody>().AddForce(shotTranform.forward * (WEAPON_FORCE - 100), ForceMode.Impulse);

           StartCoroutine("GravityGunEffect", shot);

           Destroy(shot, 1.2f);
       }
   }

   private IEnumerator GravityGunEffect(GameObject shot)
   {
       yield return new WaitForSeconds(0.32f);
       if (shot)
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
