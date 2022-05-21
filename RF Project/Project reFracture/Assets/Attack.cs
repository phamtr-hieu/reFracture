using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour
{
    Enemy enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        GameObject player = enemy.player;

    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 pos = animator.GetComponent<Transform>().position;
        if (enemy.PlayerInEnemyAttackRange(pos) && enemy.timeBtwAttacks <= 0)
        {
            animator.SetBool("isAttacking", true);
            enemy.Attack();
            enemy.timeBtwAttacks = enemy.starttimeBtwAttacks;
        }
        else
        {
            enemy.timeBtwAttacks -= Time.deltaTime;
        }

        if(!enemy.PlayerInEnemyAttackRange(pos))
		{
            animator.SetBool("isAttacking", false);
        }

        #region Flip
        if (pos.x < enemy.player.transform.position.x && enemy.facingLeft)
        {
            enemy.Flip();
            enemy.facingLeft = false;
        }
        else if (pos.x > enemy.player.transform.position.x && !enemy.facingLeft)
        {
            enemy.Flip();
            enemy.facingLeft = true;
        }
        #endregion
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
