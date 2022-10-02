using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICardData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int cardID;
    public int cardOrder;
    public bool isSelected;

    [SerializeField] Animator anim;
    [SerializeField] string animName;
    [SerializeField] TooltipHandler tooltip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetTrigger(animName);
        tooltip.isShowing = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.isShowing = false;
    }


}
