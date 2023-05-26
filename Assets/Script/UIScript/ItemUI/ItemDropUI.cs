using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropUI : UIWindow
{
    static public ItemDropUI Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private ShopSlot slotPrefab;
    [SerializeField] private Transform slotParent;

    public override void SetupUI()
    {
        base.SetupUI();
        ClearSlots();
    }
    public new void OnSwitchUI()
    {
        base.OnSwitchUI();
        transform.position = Input.mousePosition;
    }
    public void UpdateUI(List<Item> items)
    {
        ClearSlots();
        if (items.Count <= 0)
            return;

        foreach (Item item in items)
        {
            ShopSlot newSlot = Instantiate(slotPrefab, slotParent);
            newSlot.Setup(item, true);
        }
    }
    private void ClearSlots()
    {
        ShopSlot[] shopSlots = slotParent.GetComponentsInChildren<ShopSlot>();
        foreach (ShopSlot shopSlot in shopSlots)
            Destroy(shopSlot.gameObject);
    }
}
