using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] GameObject character;
    [SerializeField] public float delay;
    [SerializeField] public float damage;

    bool doDamage = false;
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("Damage");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Damage()
	{
        yield return new WaitForSeconds(delay);
        doDamage = true;
        Destroy(gameObject.transform.parent.gameObject,0.33f);
        print("gameObjet destroyed");
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
        if (doDamage && collision != null && collision.gameObject.CompareTag("Player"))
		{
            character.GetComponent<Character>().TakeDamage(damage);
            print("Player hit & damaged");
		}
            
	}

}
