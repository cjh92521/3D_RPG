using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlotInstance : MonoBehaviour
{
    static public SkillSlotInstance Instance { get; private set; }

    [SerializeField] private GameObject panel;
    [SerializeField] private Image skillIcon;

    public Skill skill;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        OffShowUi();
    }

    public void OnShowUI(Skill skill)
    {
        this.skill = skill;
        skillIcon.sprite = skill.info.icon;
        panel.SetActive(true);

    }
    public void OffShowUi()
    {
        panel.SetActive(false);
    }

}
