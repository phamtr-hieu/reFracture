using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : StateMachineBehaviour
{
    Character character;
    [SerializeField] float damage;
    [SerializeField] Vector2 attackPlacement;
    [SerializeField] Vector2 hitbox;
    [SerializeField] float dashForce;
    [SerializeField] float dashSpeed;

    GameObject enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = animator.GetComponent<Character>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        character.attackPlacement.localPosition = attackPlacement;
        character.hitboxSize = hitbox;

        //character.transform.localPosition = new Vector3(dash.x,dash.y) * Time.deltaTime * dashSpeed;
        if(character._facingRight)
		{
            character.transform.position += new Vector3(dashForce, 0);
        }
        else
		{
            character.transform.position += new Vector3(-dashForce, 0);
        }
        
        bool hit = Physics2D.OverlapBox(character.attackPlacement.position, character.hitboxSize, 0);
        if (hit)
        {
            enemy.GetComponent<Enemy>().healthPoints -= damage;
            //Debug.Log("attack hit enemy");
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
