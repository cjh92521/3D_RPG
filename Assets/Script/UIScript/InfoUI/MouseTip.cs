using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseTip : MonoBehaviour
{
    public static MouseTip Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image panel;
    [SerializeField] private TMP_Text msgText;


    private bool isSet;
    private void Start()
    {
        OffShowUI();
        isSet = false;
    }
    private void Update()
    {
        if (!isSet)
            return;

        transform.position = Input.mousePosition;
    }
    public void OnShowUI(string msg)
    {
        isSet = true;
        panel.enabled = true;
        msgText.text = msg;
        panel.rectTransform.sizeDelta = msgText.rectTransform.sizeDelta;
    }
    public void OffShowUI()
    {
        isSet = false;
        panel.enabled = false;
        msgText.text = string.Empty;
        panel.rectTransform.sizeDelta = msgText.rectTransform.sizeDelta;
    }
}
