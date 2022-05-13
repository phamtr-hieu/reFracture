using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Character : MonoBehaviour
{
    //public PlayerInput playerInput;
    private Rigidbody2D rb;

    #region Vectors
    [SerializeField] private Vector2 moveInput;
    #endregion

    #region Floats
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    #endregion

    #region Bools
    [SerializeField] private bool _jump;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {  
        #region Move
        transform.position += new Vector3(moveInput.x,0,0) * Time.deltaTime * moveSpeed;
        #endregion

        #region Jump
        if(_jump)
        {
            rb.velocity = Vector2.up *jumpForce;
        }
        #endregion
    }

    void FixedUpdate()
    {
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
	}

    public void OnJump(InputAction.CallbackContext context)
	{
        if(context.started)
        {
            _jump = true;
        } 
        else if(context.canceled)
        {
            _jump = false;
        }

		
	}
}
