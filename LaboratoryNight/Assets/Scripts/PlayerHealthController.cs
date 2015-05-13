using UnityEngine;
using System.Collections;

public class PlayerHealthController : MonoBehaviour {

    public GameObject healthBarObj;
    private float health;

	void Start () 
    {
        health = 1;
	}
	
	void Update () 
    {
	    
	}

    void TakeDamage(float damage)
    {
        this.health -= damage;
        healthBarObj.SendMessage("SetNewValue", this.health);
        Camera.main.SendMessage("CamShake");
    }

    void GainHealth(float health)
    {
        this.health += health;
        healthBarObj.SendMessage("SetNewValue", this.health);
    }
}
