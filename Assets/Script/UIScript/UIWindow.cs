using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIWindow : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] protected Button closeButton;

    public bool IsOpen => panel.activeSelf;
    public virtual void SetupUI()
    {
        closeButton.onClick.AddListener(OffShowUI);
        InputManager.Instance.AddKeyAction(KeyCode.Escape, OffShowUI);

        OffShowUI();
    }
    
    public void OnSwitchUI()
    {
        panel.SetActive(!panel.activeSelf);
    }
    public void OnShowUI()
    {
        panel.SetActive(true);
    }
    public void OffShowUI()
    {
        panel.SetActive(false);
    }

}
