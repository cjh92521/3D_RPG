using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDamageUI : DamageUI
{
    public override void OnAutoAttack(float amount, bool isCrit)
    {
        TMP_Text temp = SetupText(amount, false, isCrit);
        temp.color = autoColor;
        show.Enqueue(temp);
    }
    public override void OnSkill(float amount, bool isCrit)
    {
        TMP_Text temp = SetupText(amount, false, isCrit);
        temp.color = skillColor;
        show.Enqueue(temp);
    }
}
