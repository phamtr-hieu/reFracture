using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Character : MonoBehaviour
{
    //public PlayerInput playerInput;
    private Rigidbody2D rb;
    [SerializeField] LayerMask ground;

    #region Vectors
    [SerializeField] private Vector2 moveInput;
    #endregion

    #region Floats
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float checkRadius;
    #endregion

    [SerializeField] private float jumpButtonTimer;
    [SerializeField] private float maxJumpButtonTimer;

    #region Bools

    [SerializeField] private Transform GroundCheck;
    public bool _jump;
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
        #region Move
        transform.position += new Vector3(moveInput.x, 0, 0) * Time.deltaTime * moveSpeed;
        #endregion

        #region Jump
        if (_jump)
        {
            jumpButtonTimer += Time.deltaTime;
        }
        if(jumpButtonTimer >= maxJumpButtonTimer)
        {
            _jump = false;
        }
        if(_jump)
        {
            rb.velocity = Vector2.up *jumpForce;
        }
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
        if(context.started && onGround)
        {
            _jump = true;
            
        } 
        else if(context.canceled)
        {
            _jump = false;
            jumpButtonTimer = 0;
        }
    }

	}

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, checkRadius );
    }

    
}
