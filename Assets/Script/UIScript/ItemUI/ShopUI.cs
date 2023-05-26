using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UIWindow
{
    [SerializeField] private Transform slotsParent;
    [SerializeField] private ShopSlot slotPrefab;

    public void UpdateUI(Item[] items)
    {
        foreach (ShopSlot slot in slotsParent.GetComponentsInChildren<ShopSlot>())
            Destroy(slot.gameObject);

        foreach (Item item in items)
        {
            ShopSlot newSlot = Instantiate(slotPrefab, slotsParent);
            newSlot.Setup(item);
        }

        OnShowUI();
    }
}
