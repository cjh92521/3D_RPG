using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Hamstring : Skill
{
    [SerializeField] private float duration;
    [SerializeField] private float decreaseRate;

    public override bool OnSkill()
    {
        if (!base.OnSkill())
            return false;

        SkillManager.Instance.target.OnSlow(decreaseRate, duration);
        return true;
    }
}
