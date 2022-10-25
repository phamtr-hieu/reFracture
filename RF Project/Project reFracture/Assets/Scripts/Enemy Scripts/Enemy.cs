using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    //Enemy Stat
    [Header("Settings")]
    [SerializeField] bool _isViktor;

    [Header("Attacks Cooldowns")]
    [SerializeField] float MaxClawCooldown;
    [SerializeField] float _clawCooldown;
    [Space]
    [SerializeField] float MaxBiteCooldown;
    [SerializeField] float _biteCooldown;
    [Space]
    [SerializeField] float MaxLaserCooldown;
    [SerializeField] float MinLaserCooldown;
    [SerializeField] float _laserCooldown;
    [Space]
    [SerializeField] float MaxLightningCooldown;
    [SerializeField] float MinLightningCooldown;
    [SerializeField] float _lightningCooldown;
    [Space]

    [Header("Spiderling Stats")]
    [SerializeField] float changeDirTimer;
    float _changeDirTimer;

    [Header("Stats")]
    #region Floats
    public float healthPoints;
    public float chaseDistance;
    public float stopDistance;
    public float playerToEnemyDistance;
    public float chaseSpeed;
    public float atkRange;
    public float timeBtwAttacks;
    public float startTimeBtwAttacks;



    #endregion
    [Space]


    [Header("Others")]
    [SerializeField] Character character;
    public GameObject player;

    [SerializeField] public Transform hitboxPos;
    public Transform enemyPos;
    [SerializeField] public Vector2 hitboxSize;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] DamageFlashing damageFlashing;
    [SerializeField] GameObject healthbar;

    public bool facingLeft = true;
    public bool hasMet = false;
    Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        damageFlashing.GetComponent<DamageFlashing>();
        //slider.maxValue = healthPoints;
        //currentIdleTime = idleTime;


    }

    void Update()
    {

        if (healthPoints <= 0)
        {
            OnDeath();
        }

        #region Spiderling Move Handling
        if (!_isViktor)
        {
            _changeDirTimer -= Time.deltaTime;
            transform.position += new Vector3(dir.x, 0, 0) * Time.deltaTime * chaseSpeed;
            if (_changeDirTimer <= 0)
            {
                ChangeDir();
                _changeDirTimer = changeDirTimer;
            }
        }
        #endregion
        //Countdown Idle time

        //Countdown Idle time
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        //{
        //	idleTime -= Time.deltaTime;
        //}

        #region Cooldown Handling
        if (_isViktor)
        {
            if (_clawCooldown > 0)
                _clawCooldown -= Time.deltaTime;

            if (_biteCooldown > 0)
                _biteCooldown -= Time.deltaTime;

            if (_laserCooldown > 0)
                _laserCooldown -= Time.deltaTime;
        }
        #endregion

        if (playerToEnemyDistance >= chaseDistance * 2 && hasMet)
        {
            anim.SetTrigger("Blink");
        }


    }
    public bool PlayerInEnemyChaseRange(Vector2 enemy)
    {
        playerToEnemyDistance = Vector2.Distance(enemy, player.transform.position);


        if (playerToEnemyDistance <= chaseDistance && playerToEnemyDistance >= stopDistance)
        {
            healthbar.SetActive(true);
            hasMet = true;
            return true;
        }
        else
            return false;
    }

    public bool PlayerInEnemyAttackRange(Vector2 enemy)
    {
        playerToEnemyDistance = Vector2.Distance(enemy, player.transform.position);


        if (playerToEnemyDistance <= atkRange)
        {
            return true;
        }
        else
            return false;
    }

    public bool isFacingPlayer(Vector2 enemy, Vector2 player)
    {
        if (player.x < enemy.x)
        {
            if (facingLeft)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (player.x > enemy.x)
        {
            if (!facingLeft)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public bool PlayerIsTooFarFromEnemy(Vector2 enemy)
    {
        playerToEnemyDistance = Vector2.Distance(enemy, player.transform.position);

        if (playerToEnemyDistance >= chaseDistance * 2)
        {
            return true;
        }
        else
            return false;
    }

    public void Attack()
    {
        //RaycastHit2D hit = Physics2D.BoxCast(hitboxPos.position, hitboxSize, 0, transform.position);
        //Debug.LogWarning("Enemy hit:" + hit);
        //if (hit)
        //{
        //	character.healthPoint -= enemyDamage;
        //	print("player took damage");
        //}
    }

    public void ChooseNextAttack(Vector2 enemy)
    {
        if (playerToEnemyDistance < 7 && _clawCooldown <= 0)
        {
            anim.SetTrigger("Claw");
            _clawCooldown = Random.Range(0, MaxClawCooldown);
            return;
        }
        if (playerToEnemyDistance >= 7 && playerToEnemyDistance <= 12 && _biteCooldown <= 0)
        {
            anim.SetTrigger("Bite");
            _biteCooldown = Random.Range(0, MaxBiteCooldown);
            return;
        }
        if (playerToEnemyDistance >= 12 && playerToEnemyDistance <= 14 && _laserCooldown <= 0)
        {
            int dice = Random.Range(0, 2);

            switch (dice)
            {
                case 0:
                    anim.SetTrigger("Laser");
                    _laserCooldown = Random.Range(MinLaserCooldown, MaxLaserCooldown);
                    break;

                case 1:
                    anim.SetTrigger("Lightning");
                    _lightningCooldown = Random.Range(MinLightningCooldown, MaxLightningCooldown);
                    break;

            }
            return;
        }
    }

    public void OnDeath()
    {

        anim.SetTrigger("Die");
        if (_isViktor)
        {
            Destroy(this.gameObject, 7);
            healthbar.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
        //print("Enemy Died");


    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawWireCube(hitboxPos.position, hitboxSize);
        //Gizmos.DrawWireSphere(hitboxPos.position, hitboxSize.x);
    }

    public void TakeDamage(float damage, float damageFlash)
    {
        if (this != null)
        {
            healthPoints -= damage;
            damageFlashing.DamageFlash(0.1f);

        }
    }

    public void Flip()
    {
        this.GetComponent<Transform>().Rotate(0, 180, 0);
        //transform.Rotate(0, 180, 0);
    }

    public IEnumerator Stopping(float time)
    {
        yield return new WaitForSeconds(time);
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //	if (col.gameObject.CompareTag("Player") && !_isViktor)
    //	{
    //		col.gameObject.GetComponent<Character>().TakeDamage(_damage);
    //	}
    //}

    void ChangeDir()
    {

        int rng;
        rng = Random.Range(0, 2);

        switch (rng)
        {
            case 0:
                dir.x = 1;
                if (facingLeft)
                {
                    Flip();
                    facingLeft = false;
                }
                break;

            case 1:
                dir.x = -1;
                if (!facingLeft)
                {
                    Flip();
                    facingLeft = true;
                }
                break;

        }
    }
}



