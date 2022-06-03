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

    int attackID = 0;
    [SerializeField] float attackTimer = 1.5f;
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
    #endregion

    [SerializeField] private float jumpButtonTimer;
    [SerializeField] private float maxJumpButtonTimer;

    #region Bools
    public bool _jump;
    [SerializeField] bool jumpTimerStart;
    private bool _facingRight = true;

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
        transform.position += new Vector3(moveInput.x, 0, 0) * Time.deltaTime * moveSpeed;
        #endregion

        #region Jump
        if(jumpTimerStart)
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
        onGround = Physics2D.OverlapCircle(GroundCheck.position,checkRadius,ground);
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
        if (moveInput.x > 0 && !_facingRight)
        {
            flip();
            _facingRight = true;
        }
        else if (moveInput.x < 0 && _facingRight)
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
        }
        else if (context.canceled)
	    {
            _jump = false;
            //jumpButtonTimer = 0;
            Debug.Log("jump button released");
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attackID = attackManager.attackQueue[0];
            anim.SetTrigger("attack" + attackID);
            print(attackID);
            attackManager.OnAttack();

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
        Gizmos.DrawWireSphere(GroundCheck.position, checkRadius );
    }

    
}
