using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedLoadoutCheck : MonoBehaviour
{
    [SerializeField] DropSlot[] dropSlots = new DropSlot[5];
    [SerializeField] Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }


    private void Update()
    {
        if (dropSlots[0].hasData && dropSlots[1].hasData && dropSlots[2].hasData && dropSlots[3].hasData && dropSlots[4].hasData)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
