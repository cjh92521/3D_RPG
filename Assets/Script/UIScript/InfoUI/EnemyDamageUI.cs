using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDamageUI : DamageUI
{
    [SerializeField] private Camera cam;

    protected new void Update()
    {
        base.Update();

        Quaternion rotation = cam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);

    }
    public override void OnAutoAttack(float amount,bool isCrit)
    {
        TMP_Text temp = SetupText(amount,true,isCrit);
        temp.color = autoColor;
        show.Enqueue(temp);
    }
    public override void OnSkill(float amount, bool isCrit)
    {
        TMP_Text temp = SetupText(amount, true, isCrit);
        temp.color = skillColor;
        show.Enqueue(temp);
    }
}
