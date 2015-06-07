using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

	// Use this for initialization

    public GameObject onHit;
    public GameObject onRocketHit;
    public GameObject weakBossEffect;
    private GameObject efx;
    private bool canDamageBoss = false;

    public GUIBarScript bossHealthBar;

    private int rocketHitCount = 0;

    private float health = 1;

    public MoveTo moveToScript;
    public TurnAtPlayer turnAtPlayer;
    private NavMeshAgent navMeshAgent;
    public BossGunController bossGun;

    private SoundsController sounds;

	void Start () {
        onHit = Resources.Load("Explosion02") as GameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();
        sounds = GameObject.FindObjectOfType<SoundsController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ResetHitCount()
    {
        rocketHitCount = 0;
        canDamageBoss = false;
        Destroy(efx);
        moveToScript.enabled = true;
        navMeshAgent.enabled = true;
        turnAtPlayer.enabled = true;
        bossGun.enabled = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Rocket")
        {
            sounds.RocketHit();

            rocketHitCount++;
            GameObject e = Instantiate(onRocketHit, transform.position + (transform.up * 2), transform.rotation) as GameObject;
            Destroy(e, 0.4f);
            if (rocketHitCount == 3)
            {
                canDamageBoss = true;
                efx = Instantiate(weakBossEffect, transform.position + (transform.up * 3), Quaternion.identity) as GameObject;
                moveToScript.enabled = false;
                navMeshAgent.enabled = false;
                turnAtPlayer.enabled = false;
                bossGun.enabled = false;
                Invoke("ResetHitCount", 2f);
            }
        }

        if (col.gameObject.name.Contains("bullet"))
        {
            if (canDamageBoss)
            {
                TakeDamage();
                GameObject e = Instantiate(onHit, transform.position + (transform.up * 2), transform.rotation) as GameObject;
                Destroy(e, 0.4f);
            }
        }

    }

    private void UpdateHealth(float health)
    {
        this.health += health;

        if (this.health <= 0)
        {
            GameObject e = Instantiate(onHit, transform.position + (transform.up * 2), transform.rotation) as GameObject;
            Destroy(e, 0.4f);
            Destroy(this.gameObject);
        }

        this.bossHealthBar.SetNewValue(this.health);
    }

    private void TakeDamage()
    {
        UpdateHealth(-0.05f);
    }
}
