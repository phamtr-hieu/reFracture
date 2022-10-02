using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAttack : StateMachineBehaviour
{
    Character character;
    [SerializeField] float damage;
    [SerializeField] Vector2 attackPlacement;
    [SerializeField] Vector2 hitbox;
    [SerializeField] float knockbackForce;
    [SerializeField] float knockbackTime;
    [SerializeField] float animTimer;
    [SerializeField] float timerOffset;
    bool damageDealt = false;
    IEnumerator coroutine;

    float gravity;
    Rigidbody2D rb;

    GameObject enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = animator.GetComponent<Character>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        character.attackPlacement.localPosition = attackPlacement;
        character.hitboxSize = hitbox;

        character._movable = false;
        character._flipable = false;
        character._attacking = true;

        animTimer = stateInfo.length; 
        animTimer -= timerOffset;
        damageDealt = false;

        rb = character.GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animTimer -= Time.deltaTime;
        if (animTimer <= 0)
        {
            Collider2D hit = Physics2D.OverlapBox(character.attackPlacement.position, character.hitboxSize, 0, LayerMask.GetMask("Enemy"));
            Debug.Log(hit);
            if (hit != null)
            {
                if (hit.CompareTag("Enemy") && enemy != null && !damageDealt)
                {
                    hit.GetComponent<Enemy>().TakeDamage(damage, 0.5f);
                    Debug.Log("Smash attack hit enemy");
                    damageDealt = true;

                    if (character._facingRight)
                        hit.GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockbackForce, ForceMode2D.Impulse);
                    else
                        hit.GetComponent<Rigidbody2D>().AddForce(Vector2.left * knockbackForce, ForceMode2D.Impulse);
                }
            }

            //Knockback direction
            //if (character._facingRight)
            //{
            //    character.GetComponent<Rigidbody2D>().AddForce(new Vector2(-knockbackForce, 0), ForceMode2D.Impulse);
            //}
            //else if (!character._facingRight)
            //{
            //    character.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockbackForce, 0), ForceMode2D.Impulse);
            //}

            //Stopping
            coroutine = character.Stopping(knockbackTime);
            character.StartCoroutine(coroutine);


        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animTimer = 0;
        character._movable = true;
        character._flipable = true;
        IEnumerator endAttack = character.EndAttack();
        character.StartCoroutine(endAttack);

        rb.gravityScale = character.gravityScale;
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
