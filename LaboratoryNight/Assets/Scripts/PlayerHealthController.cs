using UnityEngine;
using System.Collections;

public class PlayerHealthController : MonoBehaviour {

    public GameObject healthBarObj;
    private float health;
    private bool shieldActive = false;

	void Start () 
    {
        health = 1;
	}
	
	void Update () 
    {
	    
	}

    public void ShieldActivated()
    {
        shieldActive = true;
    }

    public void ShieldDeactivated()
    {
        shieldActive = false;
    }

    void TakeDamage(float damage)
    {
        if (!shieldActive)
        {
            this.health -= damage;
            healthBarObj.SendMessage("SetNewValue", this.health);
            Camera.main.SendMessage("CamShake");
        }
    }

    void GainHealth(float health)
    {
        if (this.health + health > 1)
        {
            this.health = 1;
        }
        else
        {
            this.health += health;
        }
        healthBarObj.SendMessage("SetNewValue", this.health);
    }
}
