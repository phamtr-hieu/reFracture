using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : StateMachineBehaviour
{
	Character character;
	[SerializeField] float blinkDistance;


	GameObject enemy;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		character = animator.GetComponent<Character>();
		enemy = GameObject.FindGameObjectWithTag("Enemy");

		//character._attacking = true;

		//character.transform.localPosition = new Vector3(dash.x,dash.y) * Time.deltaTime * dashSpeed;
		if (character._facingRight)
		{
			character.transform.localPosition +=  Vector3.right * blinkDistance;
		}
		else
		{
			character.transform.localPosition += Vector3.left * blinkDistance;
		}



	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//character._attacking = false;
	}



}
