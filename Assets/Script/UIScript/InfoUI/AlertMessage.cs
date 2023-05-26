using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlertMessage : MonoBehaviour
{
    public static AlertMessage Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private TMP_Text text;
    [SerializeField] private float duration;

    private Coroutine coroutine;
    void Start()
    {
        text.enabled = false;
    }

    public void OnMessege(string msg)
    {
        text.text = msg;
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(Animation());
    }
    IEnumerator Animation()
    {
        text.enabled = true;
        yield return new WaitForSeconds(0.5f);
        float time = duration;
        while (time > 0)
        {
            time -= Time.deltaTime;
            Color temp = text.color;
            temp.a = time / duration;
            text.color = temp;
            yield return null;
        }
        text.enabled = false;
    }
}
