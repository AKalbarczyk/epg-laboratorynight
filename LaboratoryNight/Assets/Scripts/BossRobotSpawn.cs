using UnityEngine;
using System.Collections;

public class BossRobotSpawn : MonoBehaviour {

    public GameObject robotPrefab;
    public GameObject efx;

	void Start () 
    {
        InvokeRepeating("SpawnRobots", 6f, 13f);
	}
	
	void Update () {
	
	}

    private void SpawnRobots()
    {
        GameObject efx1 = Instantiate(efx, transform.position + transform.right * 8 + transform.up * 2, Quaternion.identity) as GameObject;
        //GameObject efx2 = Instantiate(efx, transform.position - transform.right * 8 + transform.up * 2, Quaternion.identity) as GameObject;
        Destroy(efx1, 0.5f);
       // Destroy(efx2, 0.5f);
        Instantiate(robotPrefab, transform.position + transform.right * 8, Quaternion.identity);
       // Instantiate(robotPrefab, transform.position - transform.right * 8, Quaternion.identity);
    }
}
