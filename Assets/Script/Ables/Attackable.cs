using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stateable))]
public class Attackable : MonoBehaviour
{
    private Stateable status;
    [SerializeField]private Damageable target;

    public System.Action onAttack;
    public System.Action onAttackCrit;

    public Damageable Target => target;

    private new Transform transform;

    private float nextAttackTime;

    void Start()
    {
        transform = base.transform;
        status = GetComponent<Stateable>();
    }
    private void Update()
    {
        if (status.isDead)
            return;

        if (target != null && !target.IsDead)
            AutoAttack(target);
    }
    public void OnTarget(Damageable target)
    {
        this.target = target;
    }
    public void AutoAttack(Damageable target)
    {
        float distance = Vector3.Distance(transform.position, target.Position);
        bool isInAttackRange =  distance <= status.AttackRange;
        if (isInAttackRange)
        {
            if (nextAttackTime < Time.time)
            {
                float rand = Random.Range(0f, 100.00f);
                float damage = status.Power;
                bool isCrit = rand <= status.Crit;
                if (isCrit)
                {
                    damage *= 2f;
                    onAttackCrit?.Invoke();
                }
                else
                    onAttack?.Invoke();

                target.GetDamaged(damage,isCrit);
                nextAttackTime = Time.time + status.AttackRate;
            }
        }
        else
            target = null;
    }

}
