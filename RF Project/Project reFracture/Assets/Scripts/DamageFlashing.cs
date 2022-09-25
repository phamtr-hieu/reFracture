using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlashing : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Material flashMat;
    Coroutine flashCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageFlash(float duration, Material ogMat)
	{
        if(flashCoroutine!=null)
		{
            StopCoroutine(flashCoroutine);
		}
        flashCoroutine = StartCoroutine(FlashCoroutine(duration, ogMat));
	}

    IEnumerator FlashCoroutine(float duration, Material ogMat)
	{
        sr.material = flashMat;
        yield return new WaitForSeconds(duration);
        sr.material = ogMat ;
	}
}
