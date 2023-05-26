using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    const string INVEN_FULL = "Inventory is full";
    const string GOLD_NOT_ENOUGH = "Gold is not enough";
    public static InventoryManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public const int SLOT_COUNT = 20;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private int gold;


    private Item[] items;

    public bool IsBagging => inventoryUI.IsOpen;

    private RectTransform rect;

    public System.Action<Item> onGetItemEvent;

    private void Start()
    {
        rect = inventoryUI.transform.GetComponent<RectTransform>();

        InputManager.Instance.AddKeyAction(KeyCode.B, SwitchInventory);

        items = new Item[SLOT_COUNT];
        inventoryUI.SetupUI();

        AddItem("mushroom");
        AddItem("radish");
        AddItem("cabbage");
    }
    private bool AddItem(string name,int count = 1)
    {
        foreach (Item item in items)
        {
            if (item != null && item.data.itemName.Equals(name))
            {
                item.count += count;
                return true;
            }
        }

        for (int i = 0; i < SLOT_COUNT; i++)
        {
            if (items[i] == null)
            {
                items[i] = ItemManager.Instance.GetItem(name);
                items[i].count = count;
                return true;
            }
        }
        AlertMessage.Instance.OnMessege(INVEN_FULL);
        return false;
    }
    private bool AddItem(Item itemAdd)
    {
        foreach (Item item in items)
        {
            if (item != null && item.data.itemName.Equals(itemAdd.data.itemName))
            {
                item.count += itemAdd.count;
                return true;
            }
        }

        for (int i = 0; i < SLOT_COUNT; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemAdd;
                return true;
            }
        }
        AlertMessage.Instance.OnMessege(INVEN_FULL);

        return false;
    }
    public void SwitchInventory()
    {
        UpdateUI();
        inventoryUI.OnSwitchUI();
    }
    public void OpenInventory()
    {
        UpdateUI();
        inventoryUI.OnShowUI();
    }
    public void SwitchItem()
    {
        int[] changeIndex = ItemSlotInstance.Instance.ChangeIndexs;
        Item temp = items[changeIndex[0]];
        items[changeIndex[0]] = items[changeIndex[1]];
        items[changeIndex[1]] = temp;

        UpdateUI();
    }
    public void GetItem(Item item)
    {
        onGetItemEvent?.Invoke(item);

        if (!AddItem(item))
            Debug.Log("GetItem fail");

        UpdateUI();

    }
    public void BuyItem(Item item)
    {
        if (CostGold(item.count * item.data.price))
            GetItem(item);
        else
            Debug.Log("BuyItem fail");

        UpdateUI();
    }
    public void SellItem(int slotIndex,int count = 1)
    {
        Item item = items[slotIndex];
        item.count -= count;
        GetGold(count * item.data.price);

        if (item.count <= 0)
            items[slotIndex] = null;

        UpdateUI();
    }
    public void DropItem(int slotIndex,int count = 1)
    {
        Item item = items[slotIndex];
        item.count -= count;

        if (item.count <= 0)
            items[slotIndex] = null;

        UpdateUI();
    }
    public void UseItem(int slotIndex)
    {
        Item item = items[slotIndex];
        if (item == null)
            return;

        item.count --;
        if (item.count <= 0)
            items[slotIndex] = null;

        Debug.Log($"useItem {item.data.itemName}");
        UpdateUI();
    }
    private bool CostGold(int amount)
    {
        if (gold - amount < 0)
        {
            AlertMessage.Instance.OnMessege(GOLD_NOT_ENOUGH);
            return false;
    }
        else
        {
            gold -= amount;
            return true;
        }
    }
    private void GetGold(int amount)
    {
        gold += amount;
    }
    private void UpdateUI()
    {
        inventoryUI.UpdateUI(items, gold);
    }
    public void SortInventory()
    {
        Dictionary<string,int> pairs = new Dictionary<string,int>();
        var sort = items.Where(item => item != null).OrderBy(item => item.data.id);
        foreach (Item item in sort)
        {
            if (pairs.ContainsKey(item.data.itemName))
                pairs[item.data.itemName] += item.count;
            else
                pairs.Add(item.data.itemName, item.count);
        }
        var newItems = pairs.Select(pair => ItemManager.Instance.GetItem(pair.Key, pair.Value));
        Queue<Item> temp = new Queue<Item>(newItems);
        for (int i = 0; i < SLOT_COUNT; i++)
            items[i] = temp.Count() > 0 ? temp.Dequeue() : null;

        UpdateUI();
    }

    public int GetItemCount(string itemName)
    {
        if (items == null)
            return 0;

        int count = 0;

        for (int i = 0; i < items.Length; i++)
        {
            Item item = items[i];
            if (item != null && item.data.itemName.Equals(itemName))
                count += item.count;
        }
        return count;
    }

    public bool isInUIRect(Vector2 mousePos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rect, mousePos);
    }
}
