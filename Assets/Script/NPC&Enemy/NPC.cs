using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using QuestPart;

public class NPC : NonPlayer, IMouseClick
{
    [SerializeField] public string NPCName;
    [SerializeField] public string conversation;
    [SerializeField] public bool isVendor;
    [SerializeField] public NPCQuestAlert questAlert;

    public List<Quest> quests;
    private void Start()
    {
        quests = new List<Quest>();
        questAlert.Setup(this);
    }
    
    public QuestState GetMaxState()
    {
        if (quests.Count <= 0)
            return QuestState.None;
        return quests.Select(q => q.state).Max();
    }

    public void OnLeftMouseClick()
    {
    }
    public void OnRightMouseClick()
    {
        NPCManager.Instance.UpdateUI(this);
        NPCManager.Instance.OnShowUI();
    }
    public void OnMouseOver()
    {
        MouseTip.Instance.OnShowUI(NPCName);
    }
}
