using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : StateMachineBehaviour
{
	Character character;
	[SerializeField] float damage;
	[SerializeField] Vector2 attackPlacement;
	[SerializeField] Vector2 hitbox;
	[SerializeField] float flyForce;
	//[SerializeField] bool _spinning = false;

	GameObject enemy;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		character = animator.GetComponent<Character>();
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		character.attackPlacement.localPosition = attackPlacement;
		character.hitboxSize = hitbox;
		//character._movable = false;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Collider2D hit = Physics2D.OverlapBox(character.attackPlacement.position, character.hitboxSize, 0);
		Debug.Log(hit);
		if (hit != null)
		{
			if (hit.CompareTag("Enemy") && enemy != null)
			{

				enemy.GetComponent<Enemy>().healthPoints -= damage;
				Debug.Log("Spin attack hit enemy");

			}
		}

		character.GetComponent<Rigidbody2D>().velocity = new Vector2(0, flyForce);
		//animator.SetBool("attack3", true);
		//if(spinnedTime <= 0)
		//{
		//	animator.SetBool("attack3", false);

		//}
		//else if(spinnedTime > 0)
		//{
		//	animator.SetBool("attack3", true);
		//}

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		character._movable = true;
	}

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
