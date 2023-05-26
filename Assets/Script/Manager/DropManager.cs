using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Drop
{
    public string itemName;
    public float probability;
    public Drop(string itemName, float probability)
    {
        this.itemName = itemName;
        this.probability = probability;
    }
}
public class DropInfo
{
    public List<Item> items { get; set; }
    public DropInfo(List<Drop> dropList)
    {
        items = new List<Item>();
        foreach (Drop drop in dropList)
        {
            if (Random.Range(0f, 100f) < drop.probability)
                items.Add(ItemManager.Instance.GetItem(drop.itemName));
        }
    }
}
public class DropManager : MonoBehaviour
{
    static public DropManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    const string FILE_PATH = "DropData/{0}CSV";

    [SerializeField] private ItemDropUI dropUI;

    private Dictionary<string, List<Drop>> dropInfos;
    private DropInfo current;
    private void Start()
    {
        current = null;
        dropInfos = new Dictionary<string, List<Drop>>();
        AddDropInfo("slime");
        AddDropInfo("mushroom");
        dropUI.SetupUI();
    }
    private void AddDropInfo(string monsterName)
    {
        string csv = Resources.Load<TextAsset>(string.Format(FILE_PATH, monsterName)).text;
        string[] rows = csv.Split('\n');

        List<Drop> drops = new List<Drop>();
        foreach (string row in rows)
        {
            if (string.IsNullOrEmpty(row.Trim()))
                continue;

            string[] datas = row.Split(',');
            string itemName = datas[0].Trim();
            float probability = float.Parse(datas[1].Trim());

            drops.Add(new Drop(itemName, probability));
        }
        dropInfos.Add(monsterName, drops);
    }
    public DropInfo GetDropInfo(string monsterName)
    {
        List<Drop> list = dropInfos[monsterName];
        return new DropInfo(list);
    }



    public void OnSwitchUI(DropInfo dropInfo)
    {
        this.current = dropInfo;

        dropUI.UpdateUI(current.items);
        dropUI.OnSwitchUI();
    }
    public void RemoveItemFromInfo(Item item)
    {
        current.items.Remove(item);
        dropUI.UpdateUI(current.items);
        if (current.items.Count <= 0)
            dropUI.OffShowUI();
    }
}