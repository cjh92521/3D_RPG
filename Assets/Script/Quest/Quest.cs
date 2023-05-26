using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QuestPart;

namespace QuestPart
{
    public enum QuestState
    {
        Acceptable,
        Incomplete,
        Complete,

        None,
    }

    [System.Serializable]public struct RewardInfo
    {
        public string itemName;
        public int count;
    }

    public struct Reward
    {
        public int gold;
        public Item[] items;
        public Reward(int gold, RewardInfo[] rewardInfos)
        {
            this.gold = gold;
            List<Item> list = new List<Item>();
            foreach (RewardInfo info in rewardInfos)
                list.Add(ItemManager.Instance.GetItem(info.itemName, info.count));
            items = list.ToArray();
        }
    }

    public struct Process
    {
        public int current;
        public int total;
        public bool isFinish => current >= total;
        public Process(int current,int total)
        {
            this.current = current;
            this.total = total;
        }
    }
    [System.Serializable]public struct StateUI
    {
        public Color color;
        public Sprite sprite;
    }
}

public class Quest : MonoBehaviour
{
    [SerializeField] public string questName;
    [SerializeField] public string questDescription;


    [SerializeField] public NPC giver;
    [SerializeField] public NPC ender;

    [Header("Reward")]
    [SerializeField] public int gold;
    [SerializeField] public RewardInfo[] rewardInfos;
    
    [SerializeField] public string targetName;
    public Reward reward;
    public QuestState state;
    public Process process;

    public System.Action onProcess;

    public bool isComplete => state == QuestState.Complete;

    public virtual void Setup()
    {
        state = QuestState.Acceptable;
        giver.quests.Add(this);

        //reward = new Reward(gold, rewardInfos);
    }
    public void QuestStateMotion()
    {
        switch (state)
        {
            case QuestState.Acceptable:
                AcceptQuest();
                break;
            case QuestState.Incomplete:
                break;
            case QuestState.Complete:
                FinishQuest();
                break;
            case QuestState.None:
                break;
            default:
                break;
        }
    }
    protected virtual void AcceptQuest()
    {
        state = QuestState.Incomplete;
        ender.quests.Add(this);
        giver.quests.Remove(this);

        QuestManager.Instance.AddQuest(this);
    }
    protected virtual void FinishQuest()
    {
        state = QuestState.None;
        ender.quests.Remove(this);

        QuestManager.Instance.RemoveQuest(this);
    }
    protected void CheckQuestProgress()
    {
        if (process.isFinish)
            state = QuestPart.QuestState.Complete;

        onProcess?.Invoke();
    }
    public void OnProcess(int amount = 1)
    {
        process.current += amount;
        CheckQuestProgress();
    }
    
}
