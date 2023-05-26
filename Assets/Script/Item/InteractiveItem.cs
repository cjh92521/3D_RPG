using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class InteractiveItem : MonoBehaviour, IMouseClick
{
    [SerializeField] private string itemName;
    [SerializeField] private float castTime;

    private DropInfo drop;
    private bool isAct;

    private void Start()
    {
        isAct = false;
    }

    public void OnLeftMouseClick()
    {
    }

    public void OnRightMouseClick()
    {
        if (isAct)
            return;

        isAct = true;
        CastBarManager.Instance.Casting(castTime, Callback);
 
    }

    private void Callback()
    {
        drop = DropManager.Instance.GetDropInfo(itemName);
        DropManager.Instance.OnSwitchUI(drop);
        Destroy(gameObject);
    }

    public void OnMouseOver()
    {
        MouseTip.Instance.OnShowUI(itemName);
    }
}