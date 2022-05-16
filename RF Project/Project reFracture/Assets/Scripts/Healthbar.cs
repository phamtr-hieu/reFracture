using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Character character;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        character.healthPoint = character.currentMaxHealth;
        slider.maxValue = character.healthPoint;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = character.healthPoint;
    }

    public void TakeDamage(float damage)
    {
        character.healthPoint -= damage;
    }
}
