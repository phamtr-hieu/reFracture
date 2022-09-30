using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UICardData cardData;
    public bool hasData;
    [SerializeField] int sequence; // must start from 1, 0 means is not in order

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
}
