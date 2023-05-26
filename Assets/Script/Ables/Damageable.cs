using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stateable))]
public class Damageable : MonoBehaviour
{
    private Stateable status;
    public Vector3 Position => transform.position;
    public bool IsInvalid { get; private set; }
    public bool IsDead => status.isDead;
    public bool IsDying => status.hpFill <= 0.2f;
    private void Start()
    {
        status = GetComponent<Stateable>();
        IsInvalid = false;
    }
    public void GetDamaged(float amount, bool isCrit = false)
    {
        if (!IsInvalid)
            status.OnDamage(amount,isCrit);
    }
    public void GetDamagedBySkill(float amount, bool isCrit = false)
    {
        if (!IsInvalid)
            status.OnDamage(amount, isCrit,false);
    }
    public void OnInvalid(bool isInvalid)
    {
        IsInvalid = isInvalid;
    }
    public void OnSlow(float decreaseRate,float duration)
    {
        status.OnSlow(decreaseRate, duration);
    }
}
