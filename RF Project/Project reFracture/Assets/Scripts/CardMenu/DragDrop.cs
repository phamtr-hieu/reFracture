using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform origin;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    UICardData cd;

    public bool isInSlot = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        cd = GetComponent<UICardData>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (!isInSlot)
        {
            rectTransform.anchoredPosition = origin.anchoredPosition;
            cd.isSelected = false;
            cd.cardOrder = 0;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("click");
    }

}
