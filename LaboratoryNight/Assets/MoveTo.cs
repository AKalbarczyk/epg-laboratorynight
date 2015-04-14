using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

    Transform goal;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine("MoveToPlayer");
    }

    IEnumerator MoveToPlayer()
    {
        while (true)
        {
            agent.destination = goal.position;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {

    }
}
