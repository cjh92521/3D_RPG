using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected float litimDistance;
    [SerializeField] protected bool isUnTarget;

    protected PlayerCon player;
    public SkillInfo info { get; private set; }

    public bool IsEnoughRage => stateable.Energy >= info.energyCost;
    public bool IsInRange => isUnTarget? true : player.TargetDistance <= litimDistance;
    public bool isCooling { get; private set; }
    public float coolLeftTime { get; private set; }
    public float CoolProcess => coolLeftTime / info.coolTime;

    protected Stateable stateable;
    protected Damageable Target => SkillManager.Instance.target;
    public void SetupInfo(SkillInfo info, PlayerCon player)
    {
        this.info = info;
        this.player = player;
        stateable = player.transform.GetComponent<Stateable>();
    }
    public virtual bool OnSkill()
    {
        if (!IsSkillable())
            return false;

        player.stateable.OnGetEnergy(-info.energyCost);
        StartCoroutine(CoolDown());
        return true;
    }
    public virtual bool IsSkillable()
    {
        if (!isUnTarget && Target == null)
        {
            AlertMessage.Instance.OnMessege("Need a target");
            return false;
        }
        if (isCooling)
        {
            AlertMessage.Instance.OnMessege("SKill is not Ready");
            return false;
        }
        if (!IsInRange)
        {
            AlertMessage.Instance.OnMessege("Target is so far");
            return false;
        }
        if (!IsEnoughRage)
        {
            AlertMessage.Instance.OnMessege("Rage is not enough");
            return false;
        }
        if (!isUnTarget && Target.IsDead)
        {
            AlertMessage.Instance.OnMessege("Target is already dead");
            return false;
        }
        string msg = null;
        if (!IsMeetOterCondition(out msg))
        {
            AlertMessage.Instance.OnMessege(msg);
            return false;
        }

        return true;
    }
    public virtual bool IsMeetOterConditionForUI()
    {
        return true;
    }
    public virtual bool IsMeetOterCondition(out string msg)
    {
        msg = string.Empty;
        return true;
    }

    IEnumerator CoolDown()
    {
        isCooling = true;
        coolLeftTime = info.coolTime;
        while (coolLeftTime > 0)
        {
            coolLeftTime -= Time.deltaTime;
            yield return null;
        }
        isCooling = false;
    }
}
