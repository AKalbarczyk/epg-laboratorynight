using UnityEngine;
using System.Collections;

public class PlayerHealthController : MonoBehaviour {

    public GUIBarScript healthBar;
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
        healthBar.SetNewValue(this.health);
        Camera.main.SendMessage("CamShake");
    }

    void GainHealth(float health)
    {
        this.health += health;
        healthBar.SetNewValue(this.health);
    }
}
