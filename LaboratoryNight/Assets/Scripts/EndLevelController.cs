using UnityEngine;
using System.Collections;

public class EndLevelController : MonoBehaviour {

	// Use this for initialization
    public GameObject efx;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject obj = Instantiate(efx, col.transform.position, Quaternion.identity) as GameObject;
            Invoke("LoadNextLevel", 3f);
        }
    }

    public void LoadNextLevel()
    {
        Application.LoadLevel("TestScene");
    }
}
