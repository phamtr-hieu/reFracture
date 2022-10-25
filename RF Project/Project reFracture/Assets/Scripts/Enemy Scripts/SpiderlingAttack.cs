using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderlingAttack : StateMachineBehaviour
{
    Enemy enemy;
    GameObject character;
    [SerializeField] Vector2 hitboxPos;
    [SerializeField] Vector2 hitbox;
    [SerializeField] float damage;
    [SerializeField] float tickRate;

    float timer;
    int frameTimer = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;

        enemy = animator.GetComponent<Enemy>();
        GameObject player = enemy.player;
        character = GameObject.FindGameObjectWithTag("Player");
        enemy.hitboxPos.localPosition = hitboxPos;
        enemy.hitboxSize = hitbox;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D hit = Physics2D.OverlapBox(enemy.hitboxPos.position, enemy.hitboxSize, 0, LayerMask.GetMask("Player"));

        timer += Time.deltaTime;
        frameTimer++;

        if (frameTimer % tickRate == 0)
        {
            if (hit != null)
            {
                if (hit.CompareTag("Player"))
                {
                    character.GetComponent<Character>().TakeDamage(damage);
                    Debug.Log("Spiderling hit " + hit + " for " + damage + " damage");
                }
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
