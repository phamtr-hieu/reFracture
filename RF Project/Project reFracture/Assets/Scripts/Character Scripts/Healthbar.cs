using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] Enemy enemy;
    public Slider slider;
    public bool isEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        //character.healthPoint = character.currentMaxHealth;
        if(character != null)
		{
            slider.maxValue = character.healthPoint;
        }
           

        if (isEnemy)
        {
            slider.maxValue = enemy.healthPoints;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (character != null)
            slider.value = character.healthPoint;

        if(isEnemy)
		{
            slider.value = enemy.healthPoints;
		}
    }

    
}
