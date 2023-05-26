using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    static public ShopManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private ShopUI shopUI;
    private RectTransform rect;
    private List<Item> items;
    public Item[] Items { get { return items.ToArray(); } }
    public bool IsShopping => shopUI.IsOpen;
    private void Start()
    {
        shopUI.SetupUI();
    }
    public void SetupShopList(string fileName)
    {
        rect = shopUI.transform.GetComponent<RectTransform>();

        items = new List<Item>();
        TextAsset textAsset = Resources.Load<TextAsset>($"NPC/{fileName}");
        string[] rows = textAsset.text.Split('\n');
        foreach (string row in rows)
        {
            if (string.IsNullOrEmpty(row.Trim()))
                continue;
            string[] splits = row.Split(',');
            string itemName = splits[0];
            int count = int.Parse(splits[1]);

            Item newItem = ItemManager.Instance.GetItem(itemName, count);
            items.Add(newItem);
        }

        shopUI.UpdateUI(Items);
        InventoryManager.Instance.OpenInventory();
    }
    public bool isInUIRect(Vector2 mousePos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rect, mousePos);
    }
}
