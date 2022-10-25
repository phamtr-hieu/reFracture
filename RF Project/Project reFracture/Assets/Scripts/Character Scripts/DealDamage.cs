using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] public float delay;
    [SerializeField] public float damage;
    [SerializeField] LayerMask ground;

    bool doDamage = false;
    bool damaged;
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("Damage");
        RaycastHit2D hit = Physics2D.Raycast(transform.parent.position, Vector2.down, Mathf.Infinity, ground);
        float distanceToGround = hit.distance;
        float newYposition = transform.position.y - distanceToGround;
        gameObject.transform.position = new Vector2(transform.position.x, newYposition);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), character.GetComponent<Collider2D>(), true);
        
        


    }

    // Update is called once per frame
    void Update()
    {
        print(delay);
    }

    IEnumerator Damage()
	{
        yield return new WaitForSeconds(delay);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), character.GetComponent<Collider2D>(), false);
        doDamage = true;
        damaged = false;
        Destroy(gameObject.transform.parent.gameObject,0.58f);
        print("gameObjet destroyed");
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
        
        if (doDamage && collision != null && collision.gameObject.CompareTag("Player") && !damaged)
		{
            character.GetComponent<Character>().TakeDamage(damage);
            damaged = true;
            print("Player hit & damaged");

		}
            
	}

    

}
