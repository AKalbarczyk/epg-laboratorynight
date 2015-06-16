using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour {

    public GameObject healthBarObj;
    private float health;
    private bool shieldActive = false;

    public SoundsController sounds;
    public Text endGameText;

    private EndGameScreen endGameScreen;

	void Start () 
    {
        health = 1;
        sounds = GameObject.FindObjectOfType<SoundsController>();
        this.endGameText.text = "";
        this.endGameScreen = GameObject.FindObjectOfType<EndGameScreen>();
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
            sounds.PlayerHit();
            Camera.main.SendMessage("CamShake");

            if (this.health <= 0)
            {
                this.endGameText.text = "You're dead.";
               // this.endGameScreen.EndScene(Application.loadedLevel);
                Application.LoadLevel(Application.loadedLevel);
            }

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
