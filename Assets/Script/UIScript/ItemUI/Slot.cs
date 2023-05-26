using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType
{
    Inven,
    Shop,
    Drop,
}


public abstract class Slot : MonoBehaviour,
    IPointerClickHandler, IDropHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        InDrag();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Drop();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDrag();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                LeftClick();
                break;
            case PointerEventData.InputButton.Right:
                RightClick();
                break;
            case PointerEventData.InputButton.Middle:
                break;
            default:
                break;
        }
    }

    public abstract void BeginDrag();
    public abstract void InDrag();
    public abstract void EndDrag();
    public abstract void Drop();
    public abstract void LeftClick();
    public abstract void RightClick();
}

