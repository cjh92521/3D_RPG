using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotInstance : MonoBehaviour
{
    static public ItemSlotInstance Instance { get; private set; }
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private GameObject panel;

    public Item item;
    public int preIndex;
    public int sfxIndex;
    public int[] ChangeIndexs => new int[] {preIndex,sfxIndex};
    public bool IsDrag => panel.activeSelf;

    public SlotType type;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        OffShowUi();
    }

    public void OnShowUI(Item item,SlotType type)
    {
        panel.SetActive(true);
        this.type = type;
        this.item = item;
        icon.sprite = item.data.itemIcon;
        countText.text = item.count.ToString();

    }
    public void OffShowUi()
    {
        panel.SetActive(false);
        this.preIndex = -1;

    }

}
