using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UppercutSlash : StateMachineBehaviour
{
    Character character;
    [SerializeField] float damage;
    [SerializeField] float pushForce;
    [SerializeField] Vector2 attackPlacement;
    [SerializeField] Vector2 hitbox;
    [SerializeField] float dashForce;
    [SerializeField] float beginTime, endTime, tickRate;

    float timer;
    int frameTimer = 0;

    bool hasLeaped = false;

    GameObject enemy;

    float gravity;
    Rigidbody2D rb;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = animator.GetComponent<Character>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        character._movable = false;
        character._flipable = false;
        character._attacking = true;

        character.attackPlacement.localPosition = attackPlacement;
        character.hitboxSize = hitbox;

        IEnumerator stopping = character.Stopping(endTime/60);
        character.StartCoroutine(stopping);

        rb = character.GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        timer = 0;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D hit = Physics2D.OverlapBox(character.attackPlacement.position, character.hitboxSize, 0, LayerMask.GetMask("Enemy"));

        timer += Time.deltaTime;
        frameTimer++;

        if (!hasLeaped && timer > beginTime)
        {
            leap();
        }

        if (timer > beginTime && frameTimer % tickRate == 0 && timer < endTime)
        {
            if (hit != null)
            {
                if (hit.CompareTag("Enemy") && enemy != null)
                {
                    hit.GetComponent<Enemy>().TakeDamage(damage, 0.1f);

                    hit.GetComponent<Rigidbody2D>().AddForce(Vector2.up * pushForce, ForceMode2D.Impulse);
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
        hasLeaped = false;

        rb.gravityScale = character.gravityScale;
    }

    void leap()
    {
        character.GetComponent<Rigidbody2D>().velocity = new Vector2(0, dashForce);
        hasLeaped = true;
    }
}
