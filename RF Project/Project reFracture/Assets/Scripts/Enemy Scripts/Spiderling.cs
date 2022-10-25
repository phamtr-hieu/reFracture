using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiderling : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float _speed;
    [SerializeField] float _damage;
    [SerializeField] float changeDirTimer;
    float _changeDirTimer;
    bool _facingLeft = true;

    Rigidbody2D rb;
    Vector2 dir;
   // GameObject character;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeDir();
        //character = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _changeDirTimer -= Time.deltaTime;
        transform.position += new Vector3(dir.x, 0, 0) * Time.deltaTime * _speed;
        if (_changeDirTimer <= 0)
		{
            ChangeDir();
            _changeDirTimer = changeDirTimer;
		}
    }

    void ChangeDir()
	{
        
        int rng;
        rng = Random.Range(0, 2);
        
        switch(rng)
		{
            case 0:
                dir.x = 1;
                if (_facingLeft)
                {
                    Flip();
                    _facingLeft = false;
                }
                break;

            case 1:
                dir.x = -1;
                if (!_facingLeft)
                {
                    Flip();
                    _facingLeft = true;
                }
                break;

		}

       
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
            col.gameObject.GetComponent<Character>().TakeDamage(_damage);
		}
	}

    public void Flip()
    {
        this.transform.Rotate(0, 180, 0);
    }
}
