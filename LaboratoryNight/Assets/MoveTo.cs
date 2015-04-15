using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

    Transform goal;
    NavMeshAgent agent;
    float dist;
    bool noticePlayer = false;
    
    
    void Start()
    {       
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Player").transform;
        dist = 100f;        
    }

    void Update()
    {
        if (!noticePlayer)        
            dist = Vector3.Distance(goal.position, transform.position);           
        
        if (dist < 15f && !noticePlayer)
        {
            noticePlayer = true;
            StartCoroutine("MoveToPlayer");
        }
    }

    IEnumerator MoveToPlayer()
    {
        while (true)
        {
            agent.destination = goal.position;
            yield return new WaitForSeconds(0.5f);
        }
    }

    
}
