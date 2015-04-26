using UnityEngine;
using System.Collections;

public class TurnAtPlayer : MonoBehaviour {
    private Transform target, myPosition;
    public int rotationSpeed;

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
        myPosition = transform; 
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(target.position, transform.position) < 25F)
        {
            myPosition.rotation = Quaternion.Slerp(myPosition.rotation, Quaternion.LookRotation(target.position - myPosition.position), rotationSpeed * Time.deltaTime);
        }
   } 
}
