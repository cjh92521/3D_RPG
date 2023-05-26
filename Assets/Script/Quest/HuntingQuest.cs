using QuestPart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingQuest : Quest
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private int count;


    protected override void AcceptQuest()
    {
        process = new Process(0,count);

        enemyManager.onKillEvent += ProcessCall;

        base.AcceptQuest();
    }

    protected override void FinishQuest()
    {
        enemyManager.onKillEvent -= ProcessCall;

        base.FinishQuest();
    }
    public void ProcessCall()
    {
        OnProcess();
    }
}
