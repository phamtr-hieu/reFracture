using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadout : MonoBehaviour
{
    public static PlayerLoadout instance;

    public int[] attackList;
    [SerializeField] int attacklistSize = 5;
    [SerializeField] DropSlot[] dropSlots = new DropSlot[5];

    private void Awake()
    {
       
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        attackList = new int[attacklistSize];
    }

    public void WriteAttackList()
    {
        if (dropSlots[0].hasData && dropSlots[1].hasData && dropSlots[2].hasData && dropSlots[3].hasData && dropSlots[4].hasData)
        {
            for (int i = 0; i < attacklistSize; i++)
            {
                attackList[i] = dropSlots[i].cardData.cardID;
            }
        }
    }
}
