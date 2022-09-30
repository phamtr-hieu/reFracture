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
    //[SerializeField] float dashSpeed;

    float gravity;

    GameObject enemy;

    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = animator.GetComponent<Character>();

        character._attacking = true;

        rb = character.GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        enemy = GameObject.FindGameObjectWithTag("Enemy");

        character.attackPlacement.localPosition = attackPlacement;
        character.hitboxSize = hitbox;

        if (enemy != null)
            Physics2D.IgnoreCollision(character.GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>(), true);

        IEnumerator stopping = character.Stopping(0.35f);
        character.StartCoroutine(stopping);

        //character.transform.localPosition = new Vector3(dash.x,dash.y) * Time.deltaTime * dashSpeed;
        if (character._facingRight)
        {
            character.GetComponent<Rigidbody2D>().velocity = new Vector3(dashForce, 0);
        }
        else
        {
            character.GetComponent<Rigidbody2D>().velocity = new Vector3(-dashForce, 0);
        }

        Collider2D hit = Physics2D.OverlapBox(character.attackPlacement.position, character.hitboxSize, 0, LayerMask.GetMask("Enemy"));
        Debug.Log(hit);
        if (hit != null)
        {
            if (hit.CompareTag("Enemy") && enemy != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage, 0.5f);
                Debug.Log("Dash Attack hit enemy");
            }
        }

    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.gravityScale = character.gravityScale;
        IEnumerator endAttack = character.EndAttack();
        character.StartCoroutine(endAttack);

        if (enemy != null)
            Physics2D.IgnoreCollision(character.GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>(), false);
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
