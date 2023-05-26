using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QuestPart;

public class QuestManager : MonoBehaviour
{
    static public QuestManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private StateUI[] stateUIs;
    public StateUI GetIconStateUI(QuestState state)
    {
        return stateUIs[(int)state];
    }

    [SerializeField] private Quest[] quests;
    [SerializeField] private QuestUI ui;
    [SerializeField] private QuestProcessUI processUI;

    private void Start()
    {
        foreach (Quest quest in quests)
            quest.Setup();

        ui.SetupUI();
    }

    public void UpdateUI(Quest quest)
    {
        ui.UpdateUI(quest);
    }
    public void OnShowUI()
    {
        ui.OnShowUI();
    }
    public void OffShowUI()
    {
        ui.OffShowUI();
    }
    public void AddQuest(Quest quest)
    {
        processUI.AddProcess(quest);
    }
    public void RemoveQuest(Quest quest)
    {
        processUI.RemoveProcess(quest);
    }
}
