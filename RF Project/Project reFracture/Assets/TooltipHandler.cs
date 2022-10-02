using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TooltipHandler : MonoBehaviour
{
    RectTransform rect;
    CanvasGroup group;
    [SerializeField] CanvasScaler canvas;
    [SerializeField] Vector2 mouseOffset;
    [SerializeField] Vector2 mousePosition;
    [SerializeField] float opacityRate = 0.1f;
    [SerializeField] Vector2 constraint;

    public bool isShowing = false;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();

        group.alpha = 0;
    }

    void Update()
    {
        mousePosition = new Vector2((Mouse.current.position.ReadValue().x / Screen.width) - 0.5f, (Mouse.current.position.ReadValue().y / Screen.height) - 0.5f);

        #region constraint
        if(mousePosition.x > constraint.x)
        {
            mousePosition.x = constraint.x;
        }
        if(mousePosition.x < -constraint.x)
        {
            mousePosition.x = -constraint.x;
        }

        if (mousePosition.y > constraint.y)
        {
            mousePosition.y = constraint.y;
        }
        if (mousePosition.y < -constraint.y)
        {
            mousePosition.y = -constraint.y;
        }
        #endregion

        rect.anchoredPosition = mousePosition * canvas.referenceResolution + mouseOffset;

        if (isShowing)
        {
            if (group.alpha < 1)
                group.alpha += opacityRate;
        }
        else
        {
            if (group.alpha > 0)
                group.alpha -= opacityRate;
        }
    }
}
