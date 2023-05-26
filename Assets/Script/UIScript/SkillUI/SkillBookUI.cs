using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBookUI : UIWindow
{
    [SerializeField] private SkillBookSlot prefab;
    [SerializeField] private Transform slotTrans;

    public override void SetupUI()
    {
        base.SetupUI(); 

        
        for (int i = 0; i < SkillManager.Instance.Count; i++)
        {
            SkillBookSlot slot = Instantiate(prefab, slotTrans);
            slot.Setup(SkillManager.Instance.skills[i]);
        }


    }
}
