using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour,
    IBeginDragHandler,IDragHandler,IEndDragHandler,
    IDropHandler,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Image skillIcon;
    [SerializeField] private Image coolTimePanel;
    [SerializeField] private TMP_Text coolTimeText;
    [SerializeField] private Image iconPanel;

    [SerializeField] private Color outOfRangeColor;
    [SerializeField] private Color enAbleColor;

    private Color defaultColor = new Color(0,0,0,0);
    private Skill skill;

    private bool IsSetup => skill != null;

    private bool isDrag;
    private bool isBookSlot;

    public void Setup(Skill skill, bool isBookSlot = false)
    {
        this.isBookSlot = isBookSlot;
        coolTimeText.enabled = false;
        coolTimePanel.enabled = false;

        if (skill != null)
        {
            this.skill = skill;

            skillIcon.sprite = skill.info.icon;
            skillIcon.color = Color.white;
        }
        else
        {
            this.skill = null;
            skillIcon.sprite = null;
            skillIcon.color = defaultColor;
        }

    }

    private void Update()
    {
        if (skill == null)
            return;

        if (SkillManager.Instance.target != null)
        {
            if (!skill.IsInRange)
                iconPanel.color = outOfRangeColor;
            else if (!skill.IsEnoughRage)
                iconPanel.color = enAbleColor;
            else if (!skill.IsMeetOterConditionForUI())
                iconPanel.color = enAbleColor;
            else
                iconPanel.color = defaultColor;
        }
        else
            iconPanel.color = defaultColor;

        Cooling();
    }

    private void Cooling()
    {
        bool isCooling = skill.isCooling;
        coolTimePanel.enabled = isCooling;
        coolTimeText.enabled = isCooling;

        if (!isCooling)
            return;

        coolTimePanel.fillAmount = skill.CoolProcess;
        float time = skill.coolLeftTime;
        if (time > 10)
            coolTimeText.text = Mathf.RoundToInt(time).ToString();
        else
            coolTimeText.text = time.ToString("0.0");

    }
    public void OnSkill()
    {
        if (skill == null)
            return;

        skill.OnSkill();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSkill();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsSetup)
            return;

        isDrag = true;
        SkillSlotInstance.Instance.OnShowUI(skill);
        if (!isBookSlot)
            Setup(null);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDrag)
            return;

        Vector2 pos = eventData.position;
        SkillSlotInstance.Instance.transform.position = pos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        SkillSlotInstance.Instance.OffShowUi();

    }
    public void OnDrop(PointerEventData eventData)
    {
        if (isBookSlot)
            return;

        Setup(SkillSlotInstance.Instance.skill);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (skill != null)
            ToolTipUI.Instance.OnShowUI(skill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (skill != null)
            ToolTipUI.Instance.OffShowUI();
    }
}
