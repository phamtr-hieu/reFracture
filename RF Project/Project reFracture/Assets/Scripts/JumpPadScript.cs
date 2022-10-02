using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour
{
	[SerializeField] float jumpForce;
	[SerializeField] public bool jumpPadOn = false;
	[SerializeField] bool onJumpPad;
	[SerializeField] GameObject character;
	[SerializeField] float defaultJumpForce;
	// Start is called before the first frame update
	void Start()
	{
		character = GameObject.FindGameObjectWithTag("Player");
		defaultJumpForce = character.GetComponent<Character>().jumpForce;

	}

	// Update is called once per frame
	void Update()
	{
		if(!onJumpPad)
		{
			character.GetComponent<Character>().jumpForce = defaultJumpForce;
		}

		if (onJumpPad)
		{
			character.GetComponent<Character>().jumpForce = jumpForce;
		}
	}

	private void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Player") && jumpPadOn)
		{
			onJumpPad = true;
		}
		
		

		
	}

	private void OnCollisionExit2D(Collision2D collision)
	{

		Invoke("ResetJumpForce", 1);
	}

	void ResetJumpForce()
	{
		onJumpPad = false;
	}


}
