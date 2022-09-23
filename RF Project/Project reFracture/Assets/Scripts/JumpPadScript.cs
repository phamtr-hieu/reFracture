using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadScript : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] public bool jumpPadOn = false;
    public bool playerLaunched;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionStay2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Player")&& jumpPadOn)
		{
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpForce);
            playerLaunched = true;
            print("Player launched from Jump Pad");
		}
	}
    
    
}
