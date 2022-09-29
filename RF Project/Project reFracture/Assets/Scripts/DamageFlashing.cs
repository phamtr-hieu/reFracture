using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageFlashing : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Material flashMat;
    [SerializeField] Material ogMat;
    [SerializeField] public Coroutine flashCoroutine;

    [SerializeField] bool _flashed = true;
    // Start is called before the first frame update
    void Start()
    {
        ogMat = sr.material;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageFlash(float duration)
    {
        //if (flashCoroutine != null)
        //{
        //	StopCoroutine(flashCoroutine);
        //}

        if (_flashed)
            flashCoroutine = StartCoroutine(FlashCoroutine(duration));


    }

    IEnumerator FlashCoroutine(float duration)
    {
        _flashed = false;
        sr.material = flashMat;
        yield return new WaitForSeconds(duration);
        sr.material = ogMat;
        _flashed = true;
        flashCoroutine = null;
        print("flashed");
    }
}
