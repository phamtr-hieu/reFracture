using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : StateMachineBehaviour
{
    Character character;
    [SerializeField] float damage;
    [SerializeField] float pushForce;
    [SerializeField] Vector2 attackPlacement;
    [SerializeField] Vector2 hitbox;
    [SerializeField] float beginTime, endTime, tickRate;

    GameObject enemy;

    float gravity;
    Rigidbody2D rb;

    float timer;
    int frameTimer = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = animator.GetComponent<Character>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        character._movable = false;
        character._flipable = false;
        character._attacking = true;

        character.attackPlacement.localPosition = attackPlacement;
        character.hitboxSize = hitbox;

        rb = character.GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        timer = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = Vector2.zero;

        Collider2D hit = Physics2D.OverlapBox(character.attackPlacement.position, character.hitboxSize, 0, LayerMask.GetMask("Enemy"));

        timer += Time.deltaTime;
        frameTimer++;

        if (timer > beginTime && frameTimer % tickRate == 0 && timer < endTime)
        {
            if (hit != null)
            {
                if (hit.CompareTag("Enemy") && enemy != null)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damage, 0.1f);

                    if(character._facingRight)
                        enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.right * pushForce, ForceMode2D.Impulse);
                    else
                        enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.left * pushForce, ForceMode2D.Impulse);
                }
            }
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character._movable = true;
        character._flipable = true;
        IEnumerator endAttack = character.EndAttack();
        character.StartCoroutine(endAttack);

        rb.gravityScale = character.gravityScale;
    }
}
