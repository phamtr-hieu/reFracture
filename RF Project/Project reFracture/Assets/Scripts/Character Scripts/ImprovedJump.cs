using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedJump : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Character character;

    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (rb.velocity.y < 0)
        //{
        //    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        //}
        //else if (rb.velocity.y > 0 && character._jump == false)
        //{
        //    rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        //}




    }
}
