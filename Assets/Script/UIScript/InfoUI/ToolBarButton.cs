using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolBarButton : MonoBehaviour,
    IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]private string buttonName;

    private KeyCode keyCode;
    private System.Action onClick;

    public void Setup(KeyCode keyCode, System.Action onClick)
    {
        this.keyCode = keyCode;
        this.onClick = onClick;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseTipUI.Instance.OnShowUI(ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseTipUI.Instance.OffShowUI();
    }
    public override string ToString()
    {
        return $"{buttonName} ({keyCode.ToString()})";
    }
}
