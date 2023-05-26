using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProcessUI : MonoBehaviour
{
    [SerializeField] private QuestProcess prefab;

    Dictionary<Quest, QuestProcess> processes;
    private void Start()
    {
        processes = new Dictionary<Quest, QuestProcess>();
    }
    public void AddProcess(Quest quest)
    {
        QuestProcess process = Instantiate(prefab, transform);
        process.Setup(quest);
        
        processes.Add(quest, process);
    }
    public void RemoveProcess(Quest quest)
    {
        Destroy(processes[quest].gameObject);
        processes.Remove(quest);
    }
}
