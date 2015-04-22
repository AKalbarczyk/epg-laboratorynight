using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

    Transform goal;
    NavMeshAgent agent;
    float dist;
    bool noticePlayer = false;
    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Player").transform;
        dist = 100f;        
    }

    void Update()
    {
        if (!noticePlayer)        
            dist = Vector3.Distance(goal.position, transform.position);           
        
        if (dist < 35f && !noticePlayer)
        {
            if (!this.gameObject.name.Contains("Floor"))
            {
                animator.SetBool("spotPlayer", true);
            }

            noticePlayer = true;
            StartCoroutine("MoveToPlayer");
        }
    }

    IEnumerator MoveToPlayer()
    {
       // yield return new WaitForSeconds(0.5f);
        while (true)
        {
            agent.destination = goal.position;
            yield return new WaitForSeconds(0.25f);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Movable")
            animator.SetBool("spotPlayer", true);
            StartCoroutine("MoveToPlayer");
    }

    
}
