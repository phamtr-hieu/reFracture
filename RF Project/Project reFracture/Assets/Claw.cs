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
    [SerializeField] float beginTime, endTime, tickRate;

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

        //Vector2 pos = enemy.transform.position;
        //if (enemy.PlayerInEnemyAttackRange(pos) && enemy.isFacingPlayer(pos, enemy.player.transform.position))
        //{
        //}
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D hit = Physics2D.OverlapBox(enemy.hitboxPos.position, enemy.hitboxSize, 0, LayerMask.GetMask("Player"));

        timer += Time.deltaTime;
        frameTimer++;

        if (timer > beginTime && frameTimer % tickRate == 0 && timer < endTime)
        {
            if (hit != null)
            {
                if (hit.CompareTag("Player"))
                {
                    character.GetComponent<Character>().TakeDamage(damage);
                    Debug.Log("Claw hit " + hit + " for " + damage + " damage");
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
