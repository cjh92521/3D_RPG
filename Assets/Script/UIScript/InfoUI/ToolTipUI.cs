using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolTipUI : MonoBehaviour
{
    public static ToolTipUI Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    const string ITEM_FORMAT = "Gold : {0}";
    const string SKILL_FORMAT = "Rage : {0}";
    enum TYPE
    {
        SKILL,
        ITEM,
    }

    [SerializeField] private GameObject panel;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private TMP_Text paramsText;
    [SerializeField] private TMP_Text desText;

    [SerializeField] private Color skillColor;
    [SerializeField] private Color itemColor;
    [SerializeField] private Color desColor;

    void Start()
    {
        OffShowUI();
    }

    public void OnShowUI(Item item)
    {
        panel.SetActive(true);
        Setup(item.data.itemName, TYPE.ITEM, string.Format(ITEM_FORMAT,item.data.price), item.data.contents,item.data.itemIcon);
    }
    public void OnShowUI(Skill skill)
    {
        panel.SetActive(true);
        Setup(skill.info.name, TYPE.SKILL, string.Format(SKILL_FORMAT, skill.info.energyCost), skill.info.description,skill.info.icon);
    }
    private void Setup(string name,TYPE type,string param,string des,Sprite sprite)
    {
        nameText.text = name;
        icon.sprite = sprite;

        switch (type)
        {
            case TYPE.SKILL:
                typeText.text = "Skill";
                nameText.color = skillColor;
                break;
            case TYPE.ITEM:
                typeText.text = "Item";
                nameText.color = itemColor;
                break;
            default:
                break;
        }

        paramsText.text = param;
        desText.text = des;
        desText.color = desColor;

    }

    public void OffShowUI()
    {
        panel.SetActive(false); 
        nameText.text = string.Empty;
        typeText.text = string.Empty;
        paramsText.text = string.Empty;
        desText.text = string.Empty;
    }


}
