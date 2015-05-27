using UnityEngine;
using System.Collections;

public class LaserGunController : MonoBehaviour {

    public GameObject laserHit;
    private ArrayList colliderList = new ArrayList();
	void Start () {
	
	}
	
	void Update () {
	
	}

    public void StartApplyDamage()
    {
        InvokeRepeating("ApplyDamage", 0, 0.5f);
    }

    public void StopApplyDamage()
    {
        CancelInvoke("ApplyDamage");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            colliderList.Add(col);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            colliderList.Remove(col);
        }
    }

    private void ApplyDamage()
    {
        foreach (Collider col in colliderList)
        {
            if (col)
            {
                col.SendMessage("TakeDamage", 0.5f);
                GameObject obj = Instantiate(laserHit, col.gameObject.transform.position, col.gameObject.transform.rotation) as GameObject;
                Destroy(obj, 0.3f);
            }
        }
    }

}
