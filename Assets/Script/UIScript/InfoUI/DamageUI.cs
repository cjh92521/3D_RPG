using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageUI : MonoBehaviour
{
    [SerializeField] protected Color skillColor;
    [SerializeField] protected Color autoColor;
    [SerializeField] private float duration;
    [SerializeField] protected float critFontSize;
    [SerializeField] protected float originFontSize;
    [SerializeField] protected float animatingHeight;

    protected RectTransform rect;
    protected Queue<TMP_Text> pool;
    protected Queue<TMP_Text> show;

    private float deltaHeight;
    private float deltaAlpha;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        pool = new Queue<TMP_Text>(transform.GetComponentsInChildren<TMP_Text>());
        show = new Queue<TMP_Text>();

        deltaHeight = (animatingHeight / duration) * Time.deltaTime;
        deltaAlpha = (1f / duration) * Time.deltaTime;

        foreach (TMP_Text text in pool)
            text.enabled = false;
    }
    protected void Update()
    {
        if (show.Count <= 0)
            return;

        int count = 0;
        foreach (TMP_Text text in show)
        {
            Vector2 pos = text.transform.localPosition;
            pos.y += deltaHeight;
            text.transform.localPosition = pos;
            Color color = text.color;
            color.a -= deltaAlpha;
            text.color = color;

            if (text.color.a <= 0f)
            {
                text.enabled = false;
                count++;
            }
        }
        for (int i = 0; i < count; i++)
            pool.Enqueue(show.Dequeue());
    }
    public virtual void OnAutoAttack(float amount, bool isCrit) { }
    public virtual void OnSkill(float amount, bool isCrit) { }

    protected TMP_Text SetupText(float amount ,bool isEnemy,bool isCrit)
    {
        TMP_Text temp = pool.Dequeue();
        
        temp.enabled = true;
        
        amount = isEnemy ? amount : -amount;
        temp.text = Mathf.RoundToInt(amount).ToString("#,###");
        
        Vector2 pos = temp.transform.localPosition;
        pos.x = Random.Range(0, rect.rect.width);
        pos.y = 0;
        temp.transform.localPosition = pos;
        
        temp.fontSize = isCrit ? critFontSize : originFontSize;

        return temp;
    }
}
