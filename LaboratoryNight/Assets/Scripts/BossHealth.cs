using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

	// Use this for initialization

    public GameObject onHit;
    public GameObject onRocketHit;
    public GameObject weakBossEffect;
    private GameObject efx;
    private bool canDamageBoss = false;
    public GameObject shield;

    public UIBarScript bossHealthBar;

    private int rocketHitCount = 0;

    private float health = 1;

    public BossMoveTo moveToScript;
    public TurnAtPlayer turnAtPlayer;
    private NavMeshAgent navMeshAgent;
    public BossGunController bossGun;

    private SoundsController sounds;

    private EndGameScreen endGame;

	void Start () {
        onHit = Resources.Load("Explosion02") as GameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();
        sounds = GameObject.FindObjectOfType<SoundsController>();
        endGame = GameObject.FindObjectOfType<EndGameScreen>();
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
        shield.SetActive(true);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Rocket")
        {
            sounds.RocketHit();

            rocketHitCount++;
            GameObject e = Instantiate(onRocketHit, transform.position + (transform.up * 2), transform.rotation) as GameObject;
            Destroy(e, 0.4f);
            if (rocketHitCount == 1)
            {
                canDamageBoss = true;
                efx = Instantiate(weakBossEffect, transform.position + (transform.up * 3), Quaternion.identity) as GameObject;
                moveToScript.enabled = false;
                navMeshAgent.enabled = false;
                turnAtPlayer.enabled = false;
                shield.SetActive(false);
                bossGun.enabled = false;
                Invoke("ResetHitCount", 2f);
            }
        }

        if (col.gameObject.name.Contains("shot_prefab"))
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

            endGame.Invoke("EndSceneFinal", 1.5f);

            Destroy(e, 0.4f);
            Destroy(this.gameObject);
        }

        Debug.Log("BOSS HEALTH: " + this.health);

        this.bossHealthBar.UpdateValue(this.health);
    }

    private void TakeDamage()
    {
        UpdateHealth(-0.04f);
    }
}
