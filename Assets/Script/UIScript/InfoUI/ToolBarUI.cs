using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarUI : MonoBehaviour
{
    [SerializeField] private ToolBarButton[] buttons;

    private void Start()
    {
        buttons[0].Setup(KeyCode.C, () => { AlertMessage.Instance.OnMessege("아직 시공중..."); });
        buttons[1].Setup(KeyCode.B, () => { InventoryManager.Instance.SwitchInventory(); });
        buttons[2].Setup(KeyCode.P, () => { SkillManager.Instance.OpenSkillBook(); });
        buttons[3].Setup(KeyCode.M, () => { AlertMessage.Instance.OnMessege("아직 시공중..."); });
    }
}
