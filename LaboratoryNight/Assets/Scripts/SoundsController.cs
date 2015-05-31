using UnityEngine;
using System.Collections;

public class SoundsController : MonoBehaviour {

    public AudioClip footsteps;

    public AudioClip rifleShot;
    public AudioClip shotgunShot;
    public AudioClip laserShot;

    public AudioClip robotShot;
    public AudioClip turretShot;
    public AudioClip flaskThrow;

    public AudioClip rocketHit;
    public AudioClip enemyHit;
    public AudioClip playerHit;

    public AudioClip pickUp;
    public AudioClip healthPickUp;
    public AudioClip manaPickUp;

    public AudioClip shield;
    public AudioClip blackHole;
    public AudioClip shockwave;

    public AudioClip doorOpening;

    public AudioClip switchWeapon;

    public AudioClip gravityGun;

    public AudioClip hackComputer;

    public AudioClip movableHit;

    public AudioClip flaskHit;

	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Footsteps()
    {
        AudioSource.PlayClipAtPoint(footsteps, Camera.main.transform.position, 0.2f);
    }

    public void RifleShot()
    {
        AudioSource.PlayClipAtPoint(rifleShot, Camera.main.transform.position, 0.5f);
    }

    public void ShotgunShot()
    {
        AudioSource.PlayClipAtPoint(shotgunShot, Camera.main.transform.position);
    }

    public void LaserShot()
    {
        AudioSource.PlayClipAtPoint(laserShot, Camera.main.transform.position);
    }

    public void RobotShot()
    {
        AudioSource.PlayClipAtPoint(robotShot, Camera.main.transform.position, 0.4f);
    }

    public void TurretShot()
    {
        AudioSource.PlayClipAtPoint(turretShot, Camera.main.transform.position);
    }

    public void FlaskThrow()
    {
        AudioSource.PlayClipAtPoint(flaskThrow, Camera.main.transform.position, 0.2f);
    }

    public void RocketHit()
    {
        AudioSource.PlayClipAtPoint(rocketHit, Camera.main.transform.position);
    }

    public void EnemyHit()
    {
        AudioSource.PlayClipAtPoint(enemyHit, Camera.main.transform.position);
    }

    public void PlayerHit()
    {
        AudioSource.PlayClipAtPoint(playerHit, Camera.main.transform.position);
    }

    public void PickUp()
    {
        AudioSource.PlayClipAtPoint(pickUp, Camera.main.transform.position);
    }

    public void HealthPickUp()
    {
        AudioSource.PlayClipAtPoint(healthPickUp, Camera.main.transform.position);
    }

    public void ManaPickUp()
    {
        AudioSource.PlayClipAtPoint(manaPickUp, Camera.main.transform.position);
    }

    public void Shield()
    {
        AudioSource.PlayClipAtPoint(shield, Camera.main.transform.position);
    }

    public void BlackHole()
    {
        AudioSource.PlayClipAtPoint(blackHole, Camera.main.transform.position);
    }

    public void Shockwave()
    {
        AudioSource.PlayClipAtPoint(shockwave, Camera.main.transform.position);
    }

    public void DoorOpening()
    {
        AudioSource.PlayClipAtPoint(doorOpening, Camera.main.transform.position);
    }

    public void SwitchWeapon()
    {
        AudioSource.PlayClipAtPoint(switchWeapon, Camera.main.transform.position);
    }

    public void GravityGun()
    {
        AudioSource.PlayClipAtPoint(gravityGun, Camera.main.transform.position);
    }

    public void HackComputer()
    {
        AudioSource.PlayClipAtPoint(hackComputer, Camera.main.transform.position);
    }

    public void MovableHit()
    {
        AudioSource.PlayClipAtPoint(movableHit, Camera.main.transform.position);
    }

    public void FlaskHit()
    {
        AudioSource.PlayClipAtPoint(flaskHit, Camera.main.transform.position);
    }
}
