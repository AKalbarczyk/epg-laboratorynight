using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {
    public Animator animatedEntity;
    private string[] animClipNameGroup;
    public int walkType;

    public Transform player;
	// Use this for initialization
	void Start () {
        walkType = 3;
        //  animatedEntity = GameObject.Find("Robot Kyle").transform.GetComponent<Animator>();

        animClipNameGroup = new string[] {
			"Basic_Run_01",
			"Basic_Run_02",
			"Basic_Run_03",
			"Basic_Walk_01",
			"Basic_Walk_02",
			"Etc_Walk_Zombi_01"
		};
	}
	
	// Update is called once per frame
	void Update () {
        if (walkType > 5 || walkType < 0)
            walkType = 3;
	    if (Vector3.Distance(player.position, transform.position) < 40F )
            animatedEntity.Play(animClipNameGroup[walkType]);
	}
}
