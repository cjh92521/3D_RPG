using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private ItemSlot slot;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;

    public void Setup(Item item,bool isDrop = false)
    {
        slot.Setup(item,isDrop? SlotType.Drop: SlotType.Shop);
        nameText.text = item.data.itemName.ToUpper();
        priceText.text = item.data.price.ToString();
    }

}
