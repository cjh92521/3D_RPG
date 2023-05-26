using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using QuestPart;
public class QuestProcess : MonoBehaviour
{
    static readonly string FORMAT = "- {0} : {1} / {2}";
    static readonly string FORMAT_FINISH = "- Go to {0}";

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text processText;

    private Quest quest;
    public void Setup(Quest quest)
    {
        this.quest = quest;
        nameText.text = quest.questName;
        UpdateUI();

        quest.onProcess += UpdateUI;

    }
    public void UpdateUI()
    {
        Process process = quest.process;
        processText.text = process.isFinish ?
            string.Format(FORMAT_FINISH, quest.ender.NPCName) :
            string.Format(FORMAT, quest.targetName, process.current, process.total);
    }
}
