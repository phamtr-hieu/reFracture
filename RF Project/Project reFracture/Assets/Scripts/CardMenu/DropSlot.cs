using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UICardData cardData;
    public bool hasData;
    [SerializeField] GameObject cardLoadoutManager;
    PlayerLoadout loadout;
    [SerializeField] int sequence; // must start from 1, 0 means is not in order
    [SerializeField] GameObject[] cards = new GameObject[8];

    void Start()
    {
        cardLoadoutManager = GameObject.FindGameObjectWithTag("LoadoutManager");
        loadout = cardLoadoutManager.GetComponent<PlayerLoadout>();

        if (loadout.hasLoadout)
        {
            ReadCardData();
        }
    }
    void Update()
    {
        if (cardData != null)
        {
            hasData = true;
        }
        else 
        {
            hasData = false;
        }

        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            cardData = eventData.pointerDrag.GetComponent<UICardData>();
            WriteCardData();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
            eventData.pointerDrag.GetComponent<DragDrop>().isInSlot = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<DragDrop>().isInSlot = false;
            cardData = null;
        }

    }

    void WriteCardData()
    {
        cardData.cardOrder = sequence;
        cardData.isSelected = true;
    }

    public void ReadCardData()
    {
        int cardID = loadout.attackList[sequence - 1];
        GameObject slottedCard = cards[cardID - 1];

        slottedCard.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        cardData = slottedCard.GetComponent<UICardData>();
    }
}
