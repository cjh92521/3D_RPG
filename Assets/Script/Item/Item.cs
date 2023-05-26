using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Item
{
    public ItemData data;
    public int count;

    public Item(ItemData data,int count)
    {
        this.data = data;
        this.count = count;
    }
}
