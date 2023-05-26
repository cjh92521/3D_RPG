using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    #region Singleton
    static public SkillManager Instance {get;private set;}
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    [SerializeField] private PlayerCon player;
    [SerializeField] public SkillData skillData;

    [Header("SkillBar")]
    [SerializeField] private SkillSlot[] slots;

    [Header("Skills")]
    [SerializeField] public Skill[] skills;
    [SerializeField] private SkillBookUI skillBookUI;
    [SerializeField] private SkillBarUI skillBarUI;

    public Damageable target { get; private set; }
    public int Count => skills.Length;

    private void Start()
    {
        InputManager.Instance.AddKeyAction(KeyCode.P, OpenSkillBook);
        SetupSkills();
        skillBookUI.SetupUI();
        skillBarUI.SetupUI();
    }
    private void Update()
    {
        target = player.Target();
    }
    private void SetupSkills()
    {
        for (int i = 0; i < skillData.Count; i++)
            skills[i].SetupInfo(skillData[i],player);
    }
    public void OpenSkillBook()
    {
        skillBookUI.OnSwitchUI();
    }


}
