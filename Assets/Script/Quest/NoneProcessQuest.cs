using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QuestPart;
public class NoneProcessQuest : Quest
{
    protected override void AcceptQuest()
    {
        process = new Process(0, 0);
        base.AcceptQuest();
    }
}
