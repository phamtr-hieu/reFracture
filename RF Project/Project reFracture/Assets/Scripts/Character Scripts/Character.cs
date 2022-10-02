using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;

public class Character : MonoBehaviour
{
    //public PlayerInput playerInput;
    private Rigidbody2D rb;

    private Animator anim;
    [SerializeField] Healthbar healthbar;

    #region Character Stats
   // public float currentMaxHealth;
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
    [SerializeField] public float jumpForce;
    [SerializeField] private float checkRadius;
    [SerializeField] float lastAtkAnimLength;
    [SerializeField] private float jumpButtonTimer;
    [SerializeField] private float maxJumpButtonTimer;
    public float gravityScale = 4.5f;
    #endregion

    #region Bools
    public bool _jump;
    [SerializeField] bool jumpTimerStart;
    public bool _facingRight = true;
    public bool _movable = true;
    public bool _flipable = true;
    public bool _invulnerable = false;
    public bool _attacking = false;

    [SerializeField] private Transform GroundCheck;

    [SerializeField] private bool onGround;
    [SerializeField] bool attackable = true;
    #endregion

    #region damage stuff
    [SerializeField] GameObject damageVolume;
    CinemachineImpulseSource impulse;
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        impulse = GetComponent<CinemachineImpulseSource>();
        healthbar.slider.maxValue = healthPoint;
    }

    // Update is called once per frame
    void Update()
    {
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
            anim.SetBool("isJumping", false);
        }

        anim.SetFloat("fallVelocity", rb.velocity.y);
        anim.SetBool("onGround", onGround);
        #endregion

        #region Animation Time Counting

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Card"))
        {
            attackable = false;
            StartCoroutine("WaitForAnimToAttack");
        }

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
            if (_flipable)
            {
                flip();
                _facingRight = true;
            }
        }
        else if (moveInput.x < 0 && _facingRight && _movable)
        {
            if (_flipable)
            {
                flip();
                _facingRight = false;
            }

        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (_movable)
        {
            if (context.started && onGround)
            {
                _jump = true;
                jumpTimerStart = true;
                rb.velocity = Vector2.up * jumpForce;

                anim.SetBool("isJumping", true);

            }
            else if (context.canceled)
            {
                _jump = false;
                jumpButtonTimer = 0;
                anim.SetBool("isJumping", false);

            }
        }

    }

    public void OnAttack(InputAction.CallbackContext context)
    {

        if (!_attacking)
        {
            if (context.started && attackable)
            {
                attackID = attackManager.attackQueue[0];
                anim.SetTrigger("attack" + attackID);
                attackManager.OnAttack();
            }
        }
    }

    void flip()
    {
        this.transform.Rotate(0, 180, 0);
    }

    public void TakeDamage(float damage)
    {

        if (!_invulnerable)
        {
            healthPoint -= damage;
            anim.SetTrigger("isHurt");
            impulse.GenerateImpulse();
            StartCoroutine(Damaged(0.5f));

            GetComponent<DamageFlashing>().DamageFlash(0.1f);
        }

    }

    IEnumerator Damaged(float t)
    {
        damageVolume.SetActive(true);
        _movable = false;

        yield return new WaitForSeconds(t);

        damageVolume.SetActive(false);
        _movable = true;
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
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            //healthPoint -= 5;
            //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);
        }

    }
    public IEnumerator Stopping(float time)
    {
        yield return new WaitForSeconds(time);
        rb.velocity = Vector2.zero;
    }



    IEnumerator WaitForAnimToAttack()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        attackable = true;
    }

    public IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.2f);
        _attacking = false;
    }

    public void OnDeath()
	{
        _invulnerable = true;
        _flipable = false;
        moveSpeed = 0;
        attackable = false;
        anim.SetTrigger("isDead");
        Destroy(this, 3.5f);
	}
}
