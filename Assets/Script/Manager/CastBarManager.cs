using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastBarManager : MonoBehaviour
{
    public static CastBarManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject panel;
    [SerializeField] private Image fillBar;
    [SerializeField] private Color breakColor;

    void Start()
    {
        panel.SetActive(false);
    }

    public void Casting(float castTime, System.Action callback)
    {
        StartCoroutine(Cast(castTime, callback));
    }
    IEnumerator Cast(float castTime, System.Action callback)
    {
        panel.SetActive(true);
        fillBar.color = Color.white;

        float time = 0f;
        while (time < castTime)
        {
            time += Time.deltaTime;
            fillBar.fillAmount = time / castTime;
            yield return null;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                fillBar.color = breakColor;
                goto end;
            }

        }

        callback?.Invoke();

    end:
        yield return new WaitForSeconds(0.5f);
        panel.SetActive(false);
    }
}
