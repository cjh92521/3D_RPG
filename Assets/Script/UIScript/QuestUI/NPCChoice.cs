using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

using QuestPart;

public class NPCChoice : MonoBehaviour, 
    IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text questName;
    private Quest quest;
    private bool isVendor;
    private string npcName;
    public void Setup(Quest quest)
    {
        this.quest = quest;
        group.alpha = 0.5f;
        questName.text = quest.questName;

        StateUI stateUI = QuestManager.Instance.GetIconStateUI(quest.state);
        icon.sprite = stateUI.sprite;
        icon.color = stateUI.color;

        isVendor = false;
    }
    public void Setup(string npcName)
    {
        this.npcName = npcName;
        group.alpha = 0.5f;

        isVendor = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!isVendor)
        {
            QuestManager.Instance.UpdateUI(quest);
            QuestManager.Instance.OnShowUI();
        }
        else
            ShopManager.Instance.SetupShopList(npcName);

        NPCManager.Instance.OffShowUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        group.alpha = 1f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        group.alpha = 0.5f;
    }

}
