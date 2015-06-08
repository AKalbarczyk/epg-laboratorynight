using UnityEngine;
using System.Collections;

public class TakeToSecretRoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
         //   GameObject obj = Instantiate(efx, transform.position, Quaternion.identity) as GameObject;
            Invoke("LoadNextLevel", 0.6f);
        }
    }

    public void LoadNextLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 2);
    }
}
