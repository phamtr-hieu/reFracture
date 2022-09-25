using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : StateMachineBehaviour
{
    Enemy enemy;
    GameObject character;
    [SerializeField] Vector2 hitboxPos;
    [SerializeField] Vector2 hitbox;
    [SerializeField] float damage;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        GameObject player = enemy.player;
        character = GameObject.FindGameObjectWithTag("Player");
        enemy.hitboxPos.localPosition = hitboxPos;
        enemy.hitboxSize = hitbox;

        Vector2 pos = enemy.transform.position;
        if (enemy.PlayerInEnemyAttackRange(pos) && enemy.isFacingPlayer(pos, enemy.player.transform.position))
        {
            RaycastHit2D hit = Physics2D.BoxCast(hitboxPos, hitbox, 0, enemy.transform.position);
            if (hit)
            {
                character.GetComponent<Character>().TakeDamage(damage);
                Debug.Log("Bite hit " + hit + " for " + damage + " damage");
            }
        }
    }

     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
