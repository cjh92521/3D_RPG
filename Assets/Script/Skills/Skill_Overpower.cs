using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Overpower : Skill
{
    [SerializeField] private float coefficient;
    public override bool OnSkill()
    {
        if (!base.OnSkill())
            return false;

        SkillManager.Instance.target.GetDamagedBySkill(stateable.Power * coefficient, true);
        return true;
    }
}
