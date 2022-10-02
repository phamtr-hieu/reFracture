using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutPurger : MonoBehaviour
{
    [SerializeField] GameObject cardLoadoutManager;
    PlayerLoadout loadout;

    void Start()
    {
        cardLoadoutManager = GameObject.FindGameObjectWithTag("LoadoutManager");
        loadout = cardLoadoutManager.GetComponent<PlayerLoadout>();

        for (int i = 0; i < loadout.attackList.Length; i++)
        {
            loadout.attackList[i] = 0;
            loadout.hasLoadout = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
