using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : UIWindow
    
{
    [SerializeField] private Transform slotsParent;
    [SerializeField] private TMPro.TMP_Text goldText;
    [SerializeField] private Button sortButton;
    
    private ItemSlot[] slots;

    public override void SetupUI()
    {
        base.SetupUI();

        sortButton.onClick.AddListener(InventoryManager.Instance.SortInventory);

        slots = slotsParent.GetComponentsInChildren<ItemSlot>();
        foreach (ItemSlot slot in slots)
            slot.Setup(null,SlotType.Inven);

    }
    public void UpdateUI(Item[] items,int gold)
    {
        goldText.text = gold.ToString("###,##0");
        for (int i = 0; i < items.Length; i++)
            slots[i].Setup(items[i],SlotType.Inven);
    }
    
}
