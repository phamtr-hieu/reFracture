using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTutorial : MonoBehaviour
{
    Animator anim;
    [SerializeField] Character character;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (character._attacking)
            anim.SetTrigger("isDone");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>() != null)
        {
            anim.SetTrigger("start");
        }
    }
}
