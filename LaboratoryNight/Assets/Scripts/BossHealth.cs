using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

	// Use this for initialization
    public GameObject weakBossEffect;
    private GameObject efx;
    private bool canDamageBoss = false;

    public GUIBarScript bossHealthBar;

    private int rocketHitCount = 0;

    private float health = 1;

    public MoveTo moveToScript;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ResetHitCount()
    {
        rocketHitCount = 0;
        canDamageBoss = false;
        Destroy(efx);
        moveToScript.noticePlayer = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Rocket")
        {
            rocketHitCount++;
            if (rocketHitCount == 3)
            {
                canDamageBoss = true;
                efx = Instantiate(weakBossEffect, transform.position, Quaternion.identity) as GameObject;
                moveToScript.noticePlayer = false;
            }
        }

    }

    private void UpdateHealth(float health)
    {
        this.health += health;
        this.bossHealthBar.SetNewValue(health);
    }

    public void TakeDamage(float damage)
    {
        if (canDamageBoss)
        {
            UpdateHealth(-0.05f);
        }
    }
}
