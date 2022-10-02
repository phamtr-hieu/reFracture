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
    [SerializeField] float tickRate;

    float timer = 0;
    int frameTimer = 0;

    //[SerializeField] bool _spinning = false;

    GameObject enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = animator.GetComponent<Character>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        character.attackPlacement.localPosition = attackPlacement;
        character.hitboxSize = hitbox;
        character._flipable = false;
        character._attacking = true;

        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D hit = Physics2D.OverlapBox(character.attackPlacement.position, character.hitboxSize, 0, LayerMask.GetMask("Enemy"));

        timer += Time.deltaTime;
        frameTimer++;

        if (frameTimer % tickRate == 0)
        {
            if (hit != null)
            {
                if (hit.CompareTag("Enemy") && enemy != null)
                {

                    hit.GetComponent<Enemy>().TakeDamage(damage, 0.1f);
                    Debug.Log("Spin attack hit enemy");

                }
            }
        }

        character.GetComponent<Rigidbody2D>().velocity = new Vector2(0, flyForce);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character._flipable = true;
        IEnumerator endAttack = character.EndAttack();
        character.StartCoroutine(endAttack);
    }
}
