using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillBookSlot : MonoBehaviour
{
    [SerializeField] private SkillSlot skillSlot;
    [SerializeField] private TMP_Text nameText;

    public void Setup(Skill skill)
    {
        skillSlot.Setup(skill,true);
        nameText.text = skill.info.name;
    }

}
