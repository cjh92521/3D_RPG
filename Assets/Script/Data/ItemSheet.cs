using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ItemSheet", fileName = "ItemSheet")]
public class ItemSheet : ScriptableObject
{
    const string CSVFileName = "ItemResources/ItemCSV";
    const string ItemIconPath = "ItemResources/ItemIcons/{0}";
    [SerializeField] private Dictionary<string,ItemData> itemDatas;
    public ItemData this[string name]
    {
        get
        {
            return itemDatas[name];
        }
    }

    public void Setup()
    {
        itemDatas = new Dictionary<string, ItemData>();
        List<Dictionary<string, object>> csvRead = CSVReader.Read(CSVFileName);
        for (int i = 0; i < csvRead.Count; i++)
        {
            int id = int.Parse(csvRead[i]["id"].ToString());
            string name = csvRead[i]["name"].ToString();
            string contents = csvRead[i]["contents"].ToString();
            int price = int.Parse(csvRead[i]["price"].ToString());
            Sprite sprite = Resources.Load<Sprite>(string.Format(ItemIconPath, name));

            ItemData newItemData = new ItemData() { id = id, itemName = name, contents = contents, price = price, itemIcon = sprite };
            itemDatas.Add(name, newItemData);
        }
    }

}
[System.Serializable]public struct ItemData
{
    public Sprite itemIcon;
    public int id;
    public string itemName;
    public string contents;
    public int price;
}
