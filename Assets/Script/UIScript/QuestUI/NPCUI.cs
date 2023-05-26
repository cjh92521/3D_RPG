using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using QuestPart;

public class NPCUI : UIWindow
{
    [SerializeField] private TMP_Text conversationText;

    [SerializeField] private NPCChoice prefab;
    [SerializeField] private Transform choiceTrans;

    List<NPCChoice> choices;
    public override void SetupUI()
    {
        base.SetupUI();
        if (choices == null)
            choices = new List<NPCChoice>();
    }
    public void UpdateUI(NPC npc)
    {
        foreach (NPCChoice choice in choices)
            Destroy(choice.gameObject);
        choices.Clear();

        conversationText.text = npc.conversation;

        foreach (Quest quest in npc.quests)
        {
            NPCChoice choice = Instantiate(prefab, choiceTrans);
            choice.Setup(quest);
            choices.Add(choice);
        }

        if (npc.isVendor)
        {
            NPCChoice shop = Instantiate(prefab, choiceTrans);
            shop.Setup(npc.NPCName);
            choices.Add(shop);
        }
    }
}
