using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    static public ItemManager Instance { get; private set; }
    [SerializeField] private ItemSheet itemSheet;
    private void Awake()
    {
        Instance = this;
        itemSheet.Setup();
    }
    public Item GetItem(string itemName,int count = 1)
    {
        Item item = new Item(itemSheet[itemName],count);
        return item;
    }

}
