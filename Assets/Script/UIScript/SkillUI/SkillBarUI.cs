using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarUI : MonoBehaviour
{
    [SerializeField] private Transform slotParent;
    private SkillSlot[] skillSlots;

    public void SetupUI()
    {
        skillSlots = slotParent.GetComponentsInChildren<SkillSlot>();
        foreach (SkillSlot slot in skillSlots)
            slot.Setup(null);

        InputManager.Instance.AddKeyAction(KeyCode.Alpha1, skillSlots[0].OnSkill);
        InputManager.Instance.AddKeyAction(KeyCode.Alpha2, skillSlots[1].OnSkill);
        InputManager.Instance.AddKeyAction(KeyCode.Alpha3, skillSlots[2].OnSkill);
        InputManager.Instance.AddKeyAction(KeyCode.Alpha4, skillSlots[3].OnSkill);
        InputManager.Instance.AddKeyAction(KeyCode.Alpha5, skillSlots[4].OnSkill);
        InputManager.Instance.AddKeyAction(KeyCode.Alpha6, skillSlots[5].OnSkill);
        InputManager.Instance.AddKeyAction(KeyCode.Alpha7, skillSlots[6].OnSkill);
        InputManager.Instance.AddKeyAction(KeyCode.Alpha8, skillSlots[7].OnSkill);
    }
}
