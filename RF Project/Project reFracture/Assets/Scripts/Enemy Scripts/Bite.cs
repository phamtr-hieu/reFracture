using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : StateMachineBehaviour
{
    Enemy enemy;
    GameObject character;
    [SerializeField] Vector2 hitboxPos;
    [SerializeField] Vector2 hitbox;
    [SerializeField] float damage;
    [SerializeField] float lungeDistance;


    [SerializeField] float beginTime, endTime, tickRate;

    float timer;
    int frameTimer = 0;


    bool _lunged;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        character = GameObject.FindGameObjectWithTag("Player");
        enemy.hitboxPos.localPosition = hitboxPos;
        enemy.hitboxSize = hitbox;
        _lunged = false;

        timer = 0;
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D hit = Physics2D.OverlapBox(enemy.hitboxPos.position, enemy.hitboxSize, 0, LayerMask.GetMask("Player"));

        timer += Time.deltaTime;
        frameTimer++;

        if(timer > beginTime && !_lunged)
        {
            if (character.transform.position.x < enemy.transform.position.x)
            {
                enemy.GetComponent<Rigidbody2D>().velocity = Vector2.left * lungeDistance;
            }
            else
            {
                enemy.GetComponent<Rigidbody2D>().velocity = Vector2.right * lungeDistance;
            }

            _lunged = true;
        }

        if (timer > beginTime && frameTimer % tickRate == 0 && timer < endTime)
        {
            if (hit != null)
            {
                if (hit.CompareTag("Player"))
                {
                    character.GetComponent<Character>().TakeDamage(damage);
                    Debug.Log("Bite hit " + hit + " for " + damage + " damage");
                }
            }
        }

    }
}


