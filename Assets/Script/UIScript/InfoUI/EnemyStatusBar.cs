using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyStatusBar : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Camera cam;
    [SerializeField] private float distance;
    [SerializeField] private Image hpImg;
    [SerializeField] private float offsetY;

    [SerializeField] private Color[] colors;

    [SerializeField]private Stateable stateable;
    public void Setup(Stateable stateable)
    {
        hpImg.color = colors[0];

        this.stateable = stateable;
    }

    private void Update()
    {
        if (stateable == null)
            return;

        float dis = Vector3.Distance(stateable.transform.position, cam.transform.position);
        if (dis > distance || stateable.isDead)
            panel.SetActive(false);
        else
            panel.SetActive(true);

        Quaternion rotation = cam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);

        hpImg.fillAmount = stateable.hpFill;
    }

    public void OnInBattle(bool isOn)
    {
        hpImg.color = colors[isOn? 1 : 0];
    }

    [ContextMenu("test")]
    public void Foo()
    {
        hpImg.color = colors[1];
    }
}
