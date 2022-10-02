using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExitChurch : MonoBehaviour
{
    [SerializeField] Light2D churchLight;
    [SerializeField] float outsideIntensity, insideIntensity;
    bool isOutside;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOutside)
        {
            if (churchLight.intensity < outsideIntensity)
                churchLight.intensity += 0.01f;
        }
        else
        {
            if (churchLight.intensity > insideIntensity)
                churchLight.intensity -= 0.01f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>() != null)
        {
            anim.SetTrigger("isDone");
            isOutside = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>() != null)
        {
            isOutside = false;
        }
    }
}
