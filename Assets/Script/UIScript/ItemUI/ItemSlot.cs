using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, 
    IBeginDragHandler, IDragHandler, IEndDragHandler, 
    IDropHandler,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private Image countBack;
    [SerializeField] private Color defualtColor;
    [SerializeField] private Color white;

    private SlotType type;
    private Item item;
    private bool isDrag;
    public void Setup(Item item, SlotType type)
    {
        this.type = type;
        if (item == null)
        {
            SetupNull();
            return;
        }
        this.item = item;
        icon.color = white;
        icon.sprite = item.data.itemIcon;
        countBack.enabled = true;
        countText.text = item.count.ToString();
    }
    private void SetupNull()
    {
        this.item = null;
        icon.color = defualtColor;
        countBack.enabled = false;
        countText.text = string.Empty;
    }

    //EventSystem
    #region Interface
    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        InDrag(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Drop(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDrag(eventData);
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
    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnter();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExit();
    }
    #endregion
    private void BeginDrag()
    {
        if (item == null)
            return;

        ItemSlotInstance.Instance.OnShowUI(item, type);
        ItemSlotInstance.Instance.preIndex = transform.GetSiblingIndex();
        isDrag = true;
    }
    private void InDrag(PointerEventData eventData)
    {
        if (!isDrag)
            return;

        Vector3 pos = eventData.position;
        ItemSlotInstance.Instance.transform.position = pos;
    }
    private void EndDrag(PointerEventData eventData)
    {
        if (!isDrag)
            return;
        isDrag = false;
        ItemSlotInstance.Instance.OffShowUi();

        switch (type)
        {
            case SlotType.Inven:
                
                int slotIndex = transform.GetSiblingIndex();
                Vector2 mousePos = eventData.position;

                if (ShopManager.Instance.isInUIRect(mousePos))
                    InventoryManager.Instance.SellItem(slotIndex, item.count);
                else if (!InventoryManager.Instance.isInUIRect(mousePos))
                    InventoryManager.Instance.DropItem(slotIndex, item.count);
                break;
            case SlotType.Shop:
                break;
            case SlotType.Drop:
                break;
            default:
                break;
        }
    }

    private void Drop(PointerEventData eventData)
    {
        if (!ItemSlotInstance.Instance.IsDrag)
            return;

        if (type != SlotType.Inven)
            return;
        switch (ItemSlotInstance.Instance.type)
        {
            case SlotType.Inven:
                ItemSlotInstance.Instance.sfxIndex = transform.GetSiblingIndex();
                InventoryManager.Instance.SwitchItem();
                break;
            case SlotType.Shop:
                InventoryManager.Instance.BuyItem(ItemSlotInstance.Instance.item);
                break;
            case SlotType.Drop:
                InventoryManager.Instance.GetItem(ItemSlotInstance.Instance.item);
                break;
            default:
                break;
        }
    }
    private void LeftClick()
    {
        
    }
    private void RightClick()
    {
        Item currentItem = ItemSlotInstance.Instance.item;
        int slotIndex = transform.GetSiblingIndex();

        switch (type)
        {
            case SlotType.Inven:
                if (ShopManager.Instance.IsShopping)
                    InventoryManager.Instance.SellItem(slotIndex, item.count);
                else
                    InventoryManager.Instance.UseItem(slotIndex);

                break;
            case SlotType.Shop:
                InventoryManager.Instance.BuyItem(item);

                break;
            case SlotType.Drop:
                InventoryManager.Instance.GetItem(item);
                DropManager.Instance.RemoveItemFromInfo(item);
                break;
            default:
                break;
        }
    }
    private void PointerEnter()
    {
        if (item != null)
            ToolTipUI.Instance.OnShowUI(item);
    }

    
    private void PointerExit()
    {
        if (item != null)
            ToolTipUI.Instance.OffShowUI();
    }
    private void OnDestroy()
    {
        if (item != null)
            ToolTipUI.Instance.OffShowUI();
    }
}
