using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using QuestPart;

public class NPCQuestAlert : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Camera cam;
    [SerializeField] private float distance;

    [SerializeField] private Image image;
    [SerializeField] private Outline outline;

    private bool isSetup = false;
    private NPC npc;
    public void Setup(NPC npc)
    {
        isSetup = true;
        this.npc = npc;
    }

    private void Update()
    {
        if (!isSetup)
            return;
        if (npc.quests.Count <= 0)
        {
            image.enabled = false;
            return;
        }

        float dis = Vector3.Distance(transform.position, cam.transform.position);
        if (dis > distance)
            image.enabled = false;
        else
        {
            QuestState state = npc.GetMaxState();
            if (state == QuestState.None)
                return;

            image.enabled = true;

            StateUI stateUI = QuestManager.Instance.GetIconStateUI(state);
            image.sprite = stateUI.sprite;
            image.color = stateUI.color;

            outline.effectColor = image.color;
        }

        Quaternion rotation = cam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

}
