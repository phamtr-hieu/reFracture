using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator anim;
    public GameObject bar;
    [SerializeField] Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.StartPlayback();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
        {
            this.gameObject.SetActive(false);
            return;
        }

        //this.transform.position = enemy.transform.position;

        //float ratio = 1 - enemy.healthPoints / enemy.maxHealth;
        //anim.Play("EnemyHealthBar", 0, ratio);

        
    }
}
