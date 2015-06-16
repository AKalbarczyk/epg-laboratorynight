using UnityEngine;
using System.Collections;

public class ReturnToLevel : MonoBehaviour {

    // Use this for initialization
    public GameObject efx;
    GameObject player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = new Vector3(80.4f, 0f, 258.2f);
        }
    }

    public void LoadNextLevel()
    {
        Application.LoadLevel(Application.loadedLevel - 2);
    }
}
