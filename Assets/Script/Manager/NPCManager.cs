using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    static public NPCManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private NPCUI ui;
    private void Start()
    {
        ui.SetupUI();
    }
    public void UpdateUI(NPC npc)
    {
        ui.UpdateUI(npc);
    }
    public void OnShowUI()
    {
        ui.OnShowUI();
    }
    public void OffShowUI()
    {
        ui.OffShowUI();
    }
}
