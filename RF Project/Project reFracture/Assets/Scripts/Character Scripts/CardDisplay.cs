using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Sprite[] cards = new Sprite[4];
    public int cardID = 0;
    public Image display;

    Sprite DisplayCard(int CID)
    {
        return (cards[CID - 1]);
    }

    private void Update()
    {
        if (cardID >= 0)
		{
            display.sprite = DisplayCard(cardID);
        }
        
            
    }
}
