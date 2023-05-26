using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Skill_Whirlwind : Skill
{
    [SerializeField] private float effectRange;
    [SerializeField] private float rate;
    [SerializeField] private float duration;
    [SerializeField] private float coefficient;
    [SerializeField] private LayerMask enemyLayer;
    

    private Vector3 pivotPos;
    public override bool OnSkill()
    {
        if (!base.OnSkill())
            return false;

        

        Stateable status = player.gameObject.GetComponent<Stateable>();

        pivotPos = player.transform.position;
        float damage = status.Power * coefficient;

        StartCoroutine(Skill(damage, enemyLayer));
        return true;
    }

    IEnumerator Skill(float damage,int enemyLayer)
    {
        player.OnSkillAnimation(info.id);
        WaitForSeconds wait = new WaitForSeconds(rate);
        float time = 0f;
        while (time < duration)
        {
            time += rate;

            RaycastHit[] hits = Physics.SphereCastAll(pivotPos, effectRange, Vector3.up, 0f, enemyLayer);
            if (hits != null)
            {
                var damageables = hits.Select(h => h.collider.gameObject.GetComponent<Damageable>());
                foreach (Damageable damageable in damageables)
                    damageable.GetDamagedBySkill(damage);
            }

            yield return wait;
        }
        player.OnSkillAnimation();

    }
}
