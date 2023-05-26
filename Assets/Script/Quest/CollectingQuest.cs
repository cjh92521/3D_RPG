using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QuestPart;

public class CollectingQuest : Quest
{
    [SerializeField] private int itemCount;

    private int recentCount;
    protected override void AcceptQuest()
    {
        int recent = InventoryManager.Instance.GetItemCount(targetName);
        process = new Process(recent, itemCount);

        InventoryManager.Instance.onGetItemEvent += ProcessCall;

        base.AcceptQuest();
    }

    protected override void FinishQuest()
    {
        InventoryManager.Instance.onGetItemEvent -= ProcessCall;
        base.FinishQuest();
    }
    public void ProcessCall(Item item)
    {
        if (item.data.itemName.Equals(targetName))
            OnProcess(item.count);
    }
}
