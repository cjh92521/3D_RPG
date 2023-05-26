using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skill_Execute : Skill
{
    [SerializeField] private float coefficient;
    [SerializeField] private float effectRange;

    public override bool OnSkill()
    {
        if (!base.OnSkill())
            return false;

        SkillManager.Instance.target.GetDamagedBySkill(stateable.Power * coefficient,true);
        return true;
    }
    public override bool IsMeetOterConditionForUI()
    {
        if (Target.IsDying)
            return true;
        else
            return false;
    }
    public override bool IsMeetOterCondition(out string msg)
    {
        msg = string.Empty;
        if (Target.IsDying)
            return true;
        else
        {
            msg = "Target's hp is more than 20%";
            return false;
        }

    }

}
