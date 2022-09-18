using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Character : MonoBehaviour
{
	//public PlayerInput playerInput;
	private Rigidbody2D rb;
	private Animator anim;
	[SerializeField] Healthbar healthbar;

	#region Character Stats
	public float currentMaxHealth;
	public float healthPoint;


	#endregion

	#region Attack Vars
	public PlayerAttackManager attackManager;
	public Transform attackPlacement;
	public Vector2 hitboxSize;

	int attackID = 0;
	//[SerializeField] float attackTimer = 1.5f;
	//float lastAttackTime;
	#endregion
	[SerializeField] LayerMask ground;

	#region Vectors
	[SerializeField] private Vector2 moveInput;
	#endregion

	#region Floats
	[SerializeField] private float moveSpeed;
	[SerializeField] private float jumpForce;
	[SerializeField] private float checkRadius;
	[SerializeField] public float health = 100;
	[SerializeField] float lastAtkAnimLength;
	#endregion

	[SerializeField] private float jumpButtonTimer;
	[SerializeField] private float maxJumpButtonTimer;

	#region Bools
	public bool _jump;
	[SerializeField] bool jumpTimerStart;
	public bool _facingRight = true;
	public bool _movable = true;
	bool countAnimLength;

	[SerializeField] private Transform GroundCheck;

	[SerializeField] private bool onGround;
	#endregion

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{

		if (Keyboard.current.slashKey.wasPressedThisFrame)
		{
			Debug.Log("took damage");
			healthbar.TakeDamage(20);
		}
		#region Move
		if (_movable)
		{
			transform.position += new Vector3(moveInput.x, 0, 0) * Time.deltaTime * moveSpeed;
		}
		anim.SetFloat("moveInput", Mathf.Abs(moveInput.x));
		#endregion

		#region Jump
		if (jumpTimerStart)
		{
			jumpButtonTimer += Time.deltaTime;
		}

		if (_jump && jumpButtonTimer <= maxJumpButtonTimer)
		{
			rb.velocity = Vector2.up * jumpForce;
			//print("jumping");
		}
		else
		{
			jumpButtonTimer = 0;
			jumpTimerStart = false;
			_jump = false;
		}
		#endregion

		#region Attack
		if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Card"))
		{
			lastAtkAnimLength = anim.GetCurrentAnimatorStateInfo(0).length;
			countAnimLength = true;
		}
		if (countAnimLength)
		{
			lastAtkAnimLength -= Time.deltaTime;
		}
		if (lastAtkAnimLength <= 0)
		{
			countAnimLength = false;
		}


		//lastAttackTime += Time.deltaTime;
		//if (lastAttackTime > attackTimer)
		//{
		//    attackID = 1;
		//    lastAttackTime = 0;
		//}
		#endregion
	}

	void FixedUpdate()
	{
		#region Ground Check
		onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, ground);
		#endregion
		/*#region Move
        if(moveInput.x !=0)
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y );
        }
        #endregion*/
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
		if (moveInput.x > 0 && !_facingRight && _movable)
		{
			flip();
			_facingRight = true;
		}
		else if (moveInput.x < 0 && _facingRight && _movable)
		{
			flip();
			_facingRight = false;
		}
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		if (context.started && onGround)
		{
			_jump = true;
			jumpTimerStart = true;
			anim.SetBool("isJumping", true);

		}
		else if (context.canceled)
		{
			_jump = false;
			//jumpButtonTimer = 0;
			anim.SetBool("isJumping", false);

		}
	}

	public void OnAttack(InputAction.CallbackContext context)
	{
		if (context.started && lastAtkAnimLength <= 0)
		{
			attackID = attackManager.attackQueue[0];
			anim.SetTrigger("attack" + attackID);
			attackManager.OnAttack();
			//print(attackID);

			//lastAttackTime = 0;
			//attackID++;
			//if (attackID > 3)
			//    attackID = 1;
		}
	}

	void flip()
	{
		this.transform.Rotate(0, 180, 0);
	}



	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(GroundCheck.position, checkRadius);

		Gizmos.DrawWireCube(attackPlacement.position, hitboxSize);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			healthPoint -= 5;
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);
		}

	}
	public IEnumerator Stopping(float time)
	{
		yield return new WaitForSeconds(time);
		rb.velocity = Vector2.zero;
	}



	IEnumerator WaitForAnimToAttack()
	{
		yield return new WaitForSeconds(lastAtkAnimLength);
	}
}
