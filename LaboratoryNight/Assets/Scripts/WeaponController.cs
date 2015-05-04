﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public GUIBarScript gravGunBar;
    private const float GRAVGUN_INIT_VALUE = 1f;
    private const float GRAVGUN_RECHARGE_VALUE = 0.01f;
    private float gravGunValue = GRAVGUN_INIT_VALUE;
    
    private const float GRAVGUN_SHOOT_VALUE = 0.05f;
    private const float GRAVGUN_PULL_VALUE = 0.6f;
    private const float GRAVGUN_SHOCKWAVE_VALUE = 0.8f;
    
    public Text weaponModeText;
    public Text ammoText;

    public GameObject weaponFlash;
    public GameObject bullet;
    public GameObject gravityGunShot;
    public GameObject shotgunShot;
    public GameObject laserShot;
    public GameObject pullEffect; private GameObject pullObj; private bool isPullObjMoving = false; private Vector3 initPosition; private bool isPullWorking;
    public GameObject shockwaveEffect; private GameObject shockwaveObj; const int SHOCKWAVE_COUNT = 8;
    private Collider[] colsTrappedInPull;

    private const float WEAPON_FORCE = 120;
    private bool isShooting = false;

    private int gravityGunShootCount = 0;
    private const int MAX_GRAVITY_GUN_SHOOT_COUNT = 10;


    private enum WeaponMode { RIFLE, SHOTGUN, LASER };
    private WeaponMode[] WEAPON_MODE_ARR = { WeaponMode.RIFLE, WeaponMode.SHOTGUN, WeaponMode.LASER };
    private int weaponModeIndex = 0;
    private WeaponMode currWeaponMode = WeaponMode.RIFLE;

    private bool canUseSpecialMode = true;

    private int ammo = 30;
    private bool isShootingLaser = false;

	void Start ()
    {
        ammoText.text = "Ammo: " + ammo;
        weaponModeText.text = WeaponMode.RIFLE.ToString();
        gravGunBar.SetNewValue(GRAVGUN_INIT_VALUE);
        InvokeRepeating("RegenerateGravGun", 0.5f, 0.5f);
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
        CheckWeapon();
        CheckSkills();
	}

    public float GetGravGunValue()
    {
        return this.gravGunValue;
    }


    private void CheckSkills()
    {

        if (!isShooting)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GravityGunPull();
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GravityGunShockwave();
            }

            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //TODO?
            }

            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
    }

    public void UpdateGravGunBar(float value)
    {
        this.gravGunValue += value;
        this.gravGunBar.SetNewValue(gravGunValue);
    }

    private void UpdateAmmoBar(int value)
    {
        ammo += value;
        ammoText.text = "Ammo: " + ammo;
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
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if (weaponModeIndex == WEAPON_MODE_ARR.Length - 1)
            {
                weaponModeIndex = -1;
            }
            currWeaponMode = WEAPON_MODE_ARR[++weaponModeIndex];
            weaponModeText.text = currWeaponMode.ToString();
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (weaponModeIndex == 0)
            {
                weaponModeIndex = WEAPON_MODE_ARR.Length;
            }
            currWeaponMode = WEAPON_MODE_ARR[--weaponModeIndex];
            weaponModeText.text = currWeaponMode.ToString();
        }
        
    }
    private void CheckWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (currWeaponMode == WeaponMode.RIFLE)
            {
                if (ammo >= 1)
                {
                    ShootGravityGun();
                }
            }

            else if (currWeaponMode == WeaponMode.SHOTGUN)
            {
                if (ammo >= 4)
                {
                    ShootShotgun();
                }
            }
        }

        if (currWeaponMode == WeaponMode.LASER)
        {
            if (ammo >= 1)
            {
                ShootLaser();
            }
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

   //private void ShootRifle()
   //{
   //    Transform shotTranform = transform;
   //    GameObject shot = Instantiate(bullet, shotTranform.position, transform.rotation) as GameObject;
   //    shot.GetComponent<Rigidbody>().AddForce(shotTranform.forward * WEAPON_FORCE, ForceMode.Impulse);
   //    Destroy(shot, 0.3f);

   //    GameObject flash = Instantiate(weaponFlash, shotTranform.position, transform.rotation) as GameObject;
   //    Destroy(flash, 0.1f);
       
   //}

   private void ShootShotgun()
   {
       
       int shotgunShotCount = 4;
       GameObject[] shotArr = new GameObject[shotgunShotCount];
       for (int i = 0; i < shotgunShotCount; i++)
       {
           Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
           GameObject shot = Instantiate(shotgunShot, pos, transform.rotation) as GameObject;
           shotArr[i] = shot;
       }

       float positionFactor = -20f;
       foreach (GameObject shot in shotArr)
       {
           shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * (WEAPON_FORCE - 50f) + transform.right * positionFactor, ForceMode.Impulse);
           positionFactor += 10f;
           Destroy(shot, 1.2f);
       }

       GameObject flash = Instantiate(weaponFlash, transform.position, transform.rotation) as GameObject;
       Destroy(flash, 0.1f);
       UpdateAmmoBar(-5);
   
   }

   private void ShootLaser()
   {
       if (Input.GetKeyDown(KeyCode.Mouse0))
       {
           laserShot.SetActive(true);
           isShootingLaser = true;
           InvokeRepeating("ConsumeAmmo", 0, 0.5f);
           laserShot.SendMessage("StartApplyDamage");
       }
       
       if (Input.GetKeyUp(KeyCode.Mouse0))
       {
           if (ammo >= 3)
           {
               laserShot.SendMessage("StopApplyDamage");
               laserShot.SetActive(false);
               isShootingLaser = false;
               CancelInvoke("ConsumeAmmo");
           }
       }
   }

   public void AddAmmo(int amount)
   {
       UpdateAmmoBar(amount);
   }

   private void ConsumeAmmo()
   {
       if (ammo >= 3)
       {
           UpdateAmmoBar(-3);
       }
       else
       {
           laserShot.SetActive(false);
           isShootingLaser = false;
           CancelInvoke("ConsumeAmmo");
       }
   }

   private void ShootGravityGun()
   {
        gravityGunShootCount++;
        Transform shotTranform = transform;
        GameObject shot = Instantiate(gravityGunShot, shotTranform.position, transform.rotation) as GameObject;
        shot.GetComponent<Rigidbody>().AddForce(shotTranform.forward * (WEAPON_FORCE - 100), ForceMode.Impulse);

        StartCoroutine("GravityGunEffect", shot);

        Destroy(shot, 1.2f);

        UpdateAmmoBar(-1);
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
