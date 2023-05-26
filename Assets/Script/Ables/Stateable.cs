using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stateable : MonoBehaviour
{
    [System.Serializable] public struct Status
    {
        public string name;
        public float maxHp;
        public float maxEnergy;

        public float moveSpeed;

        public float attackRange;
        public float attackRate;
        public float power;
        public float crit;

    }
    public string Name => status.name;
    public float MaxHp => status.maxHp;
    public float MaxEnergy => status.maxEnergy;
    public float Power => status.power;
    public float MoveSpeed => status.moveSpeed;
    public float AttackRange => status.attackRange;
    public float AttackRate=> status.attackRate;
    public float Crit => status.crit;
    public float hpFill => hp / status.maxHp;
    public float energyFill => energy / status.maxEnergy;

    [SerializeField] private Status status;
    [SerializeField] private DamageUI damageUI;
    private float hp;
    public float Hp => hp;
    private float energy;
    public float Energy => energy;

    public bool isDead;
    public System.Action onDead;
    public System.Action onDamage;

    private void Start()
    {
        hp = MaxHp;
        energy = 0f;
        isDead = false;
    }

    public void OnDamage(float amount,bool isCrit,bool isAutoAttack = true)
    {
        if (isDead)
            return;

        onDamage?.Invoke();
        hp = Mathf.Clamp(hp - amount, 0, MaxHp);

        if (isAutoAttack)
            damageUI.OnAutoAttack(amount,isCrit);
        else
            damageUI.OnSkill(amount, isCrit);

        if (hp == 0)
            OnDead();
    }
    public void OnGetEnergy(float amount)
    {
        if (energy + amount < 0)
            return;
        else
            energy = Mathf.Clamp(energy + amount, 0, MaxEnergy);
    }
    public void OnSlow(float amount,float duration)
    {
        StartCoroutine(Slow(amount, duration));
    }
    IEnumerator Slow(float decreaseRate,float duration)
    {
        float origin = MoveSpeed;
        status.moveSpeed *= decreaseRate;
        yield return new WaitForSeconds(duration);
        status.moveSpeed = origin;
    }
    public void OnChangeSpeed(float amount)
    {
        status.moveSpeed = amount;
    }

    private void OnDead()
    {
        if (isDead)
            return;

        isDead = true;
        onDead?.Invoke();
    }
    public void ResetStatus()
    {
        hp = MaxHp;
        energy = 0f;
    }



}
