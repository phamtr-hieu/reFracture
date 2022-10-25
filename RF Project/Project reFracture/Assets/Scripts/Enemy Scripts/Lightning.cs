using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : StateMachineBehaviour
{
	[SerializeField] GameObject lightning;
	//Enemy enemy;
	GameObject character;
	Vector3 target;
	[SerializeField] float damage;
	[SerializeField] float delay;
	//[SerializeField] float lightningOffset;
	// [SerializeField] float lightningHeight = 5.26f;

	int frameTimer = 0;
	[SerializeField] float tickRate;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		character = GameObject.FindGameObjectWithTag("Player");


		lightning.GetComponentInChildren<DealDamage>().damage = damage;
		lightning.GetComponentInChildren<DealDamage>().delay = delay;

		


	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		frameTimer++;

		if (frameTimer % tickRate == 0)
		{
			target = character.transform.position;
			int rng = Random.Range(0, 2);
			switch(rng)
			{
				case 0:
					Instantiate(lightning, target, Quaternion.identity);
					Instantiate(lightning, new Vector2( target.x + 2f,target.y), Quaternion.identity);
					break;
				case 1:
					Instantiate(lightning, target, Quaternion.identity);
					Instantiate(lightning, new Vector2(target.x - 2f, target.y), Quaternion.identity);
					break;
			}
			
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove()
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that processes and affects root motion
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK()
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that sets up animation IK (inverse kinematics)
	//}
}
