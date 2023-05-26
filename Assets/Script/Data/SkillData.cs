using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]public struct SkillInfo
{
    public int id;
    public string name;
    public string description;
    public Sprite icon;
    public float coolTime;
    public float energyCost;
}

[CreateAssetMenu(menuName = "Data/SkillData", fileName = ("SkillData"))]
public class SkillData : ScriptableObject
{
    [SerializeField] private SkillInfo[] skillInfos;

    public int Count => skillInfos.Length;
    public SkillInfo this[int index]
    {
        get { return skillInfos[index]; }
    }
}


