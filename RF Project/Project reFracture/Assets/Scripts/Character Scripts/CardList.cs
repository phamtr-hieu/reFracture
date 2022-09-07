using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardList : MonoBehaviour
{
    int[] cardList = new int[5];
    // Start is called before the first frame update
    void Start()
    {
        //Quick attack
        cardList[0] = 1;

        //Uppercut
        cardList[1] = 2;

        //Spin
        cardList[2] = 3;

        cardList[3] = 4;
        cardList[4] = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
