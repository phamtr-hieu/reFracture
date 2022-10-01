using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour
{
	[SerializeField] float jumpForce;
	[SerializeField] public bool jumpPadOn = false;
	[SerializeField] GameObject character;
	// Start is called before the first frame update
	void Start()
	{
		character = GameObject.FindGameObjectWithTag("Player");
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnCollisionStay2D(Collision2D col)
	{
		print("colliding");
		if (col.gameObject.CompareTag("Player") && jumpPadOn && character.GetComponent<Character>()._jump )
		{
			character.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
			print("Player launched from Jump Pad");
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		//if(jumpPadOn)
		character.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

		//if (collision.gameObject.CompareTag("Player") && jumpPadOn && character.GetComponent<Character>()._jump)
		//{
		//	collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
		//	print("Player launched from Jump Pad");
		//}
		
	}


}
