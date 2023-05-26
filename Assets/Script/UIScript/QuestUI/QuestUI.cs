using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using QuestPart;

public class QuestUI : UIWindow
{
    const string ACCEPT_QUEST = "Accept";
    const string FINISH_QUEST = "FINISH";

    [SerializeField] private Button acceptButton;
    [SerializeField] private TMP_Text acceptButtonText;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TMP_Text cancelButtonText;

    [SerializeField] private TMP_Text questName;
    [SerializeField] private TMP_Text questDescript;

    private Quest quest;
    public override void SetupUI()
    {
        base.SetupUI();
        acceptButton.onClick.AddListener(QuestInterface);
        acceptButton.onClick.AddListener(OffShowUI);
        cancelButton.onClick.AddListener(OffShowUI);
    }
    public void UpdateUI(Quest quest)
    {
        this.quest = quest;

        questName.text = quest.questName;
        questDescript.text = quest.questDescription;

        switch (quest.state)
        {
            case QuestState.Acceptable:
                acceptButton.gameObject.SetActive(true);
                acceptButtonText.text = ACCEPT_QUEST;
                break;
            case QuestState.Incomplete:
                acceptButton.gameObject.SetActive(false);
                acceptButtonText.text = FINISH_QUEST;
                break;
            case QuestState.Complete:
                acceptButton.gameObject.SetActive(true);
                acceptButtonText.text = FINISH_QUEST;
                break;
            case QuestState.None:
                break;
            default:
                break;
        }
    }
    private void QuestInterface()
    {
        if (quest == null)
            return;

        quest.QuestStateMotion();
    }
}
