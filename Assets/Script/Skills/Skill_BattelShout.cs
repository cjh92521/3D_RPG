using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_BattelShout : Skill
{
    [ContextMenu("SKILL")]
    public override bool OnSkill()
    {
        if (!base.OnSkill())
            return false;

        stateable.OnGetEnergy(30);
        //FXManager.Instance.Action(player.transform.position);
        return true;
    }
}
