using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Stateable))]
[RequireComponent(typeof(Attackable))]
[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : NonPlayer, IMouseClick
{
    enum STATE
    {
        Stay,
        Patrol,
        Chase,
    }

    [SerializeField] private float detectionRange;
    [SerializeField] private float patrolRange;
    [SerializeField] private float stayTime;

    [SerializeField] private EnemyStatusBar statusBar;

    [Header("Anim")]
    [SerializeField] private Animator animator;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private Material[] materials;

    private new Transform transform;

    private Stateable stateable;
    private Attackable attackable;
    private Damageable damageable;

    private NavMeshAgent agent;

    private LayerMask playerLayerMask;
    private LayerMask groundLayerMask;

    private float attackRange;

    private float timer;
    private float attackRate;
    private float nextAttackTime;

    private Vector3 birthPoint;
    private Vector3 patrolPoint;

    private bool isReach;
    private DropInfo drops;

    public bool isInPool = true;
    private STATE state;
    private bool isInitializing;

    private EnemyManager enemyManager;

    private Damageable target;

    public string Name => stateable.Name;


    public void Setup(EnemyManager enemyManager)
    {
        transform = base.transform;
        this.enemyManager = enemyManager;

        agent = GetComponent<NavMeshAgent>();
        stateable = GetComponent<Stateable>();
        attackable = GetComponent<Attackable>();
        damageable = GetComponent<Damageable>();

        playerLayerMask = 1 << LayerMask.NameToLayer("Player");
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");

        stateable.onDead += OnDead;
        stateable.onDamage += () => { animator.SetTrigger("onDamage"); };
        attackable.onAttack += () => { animator.SetTrigger("onAttack"); };
        attackable.onAttackCrit += () => { animator.SetTrigger("onAttackCrit"); };
    }
    private void Update()
    {
        if (isInPool)
            return;

        if (stateable.isDead)
            return;

        isReach = agent.hasPath && agent.remainingDistance <= 2f;

        animator.SetBool("isMove", !isReach);
        bool isBettle = attackable.Target != null;
        animator.SetBool("isBattle", isBettle);
        statusBar.OnInBattle(isBettle);

        StateMotion();
    }

    private void StateMotion()
    {
        if (isInitializing)
            return;

        if (IsCheckTarget())
            state = STATE.Chase;

        switch (state)
        {
            case STATE.Stay:
                OnStay();
                break;
            case STATE.Patrol:
                OnPatrol();
                break;
            case STATE.Chase:
                OnChase();
                break;
            default:
                break;
        }


    }

    private void OnStay()
    {
        timer += Time.deltaTime;
        if (timer >= stayTime)
        {
            timer = 0f;

            Vector2 insideUnit = Random.insideUnitCircle * patrolRange;
            Vector3 point = birthPoint + new Vector3(insideUnit.x, 10f, insideUnit.y);

            RaycastHit hit;
            if (Physics.Raycast(point, Vector3.down, out hit, float.MaxValue, groundLayerMask))
            {
                patrolPoint = hit.point;
                state = STATE.Patrol;
            }
        }
    }
    private void OnPatrol()
    {
        agent.SetDestination(patrolPoint);
        if (isReach)
            state = STATE.Stay;

    }
    private void OnChase()
    {
        if (Vector3.Distance(birthPoint, transform.position) > patrolRange * 2f
            || target.IsDead)
        {
            InitializeEnemy();
            return;
        }

        if (target != null)
            agent.SetDestination(target.Position);

        if (isReach)
            OnFace();
    }
    private bool IsCheckTarget()
    {
        if (target != null)
            return true;

        var damageables = Physics
            .SphereCastAll(transform.position, detectionRange, Vector3.up, 0, playerLayerMask)
            .Select(hit => hit.collider.GetComponent<Damageable>());

        if (damageables.Count() <= 0)
        {
            target = null;
            return false; 
        }

        target = damageables
            .OrderBy(d => Vector3.Distance(transform.position, d.Position))
            .First();

        attackable.OnTarget(target);
        return target != null;
    }
    private void OnFace()
    {
        if (attackable.Target == null)
            return;

        Vector3 dir = attackable.Target.Position - transform.position;
        dir.y = 0;
        transform.LookAt(attackable.Target.transform);
    }
    public void InitializeEnemy()
    {
        StartCoroutine(ResetEnemy());
    }
    IEnumerator ResetEnemy()
    {
        isInitializing = true;
        target = null;
        agent.SetDestination(birthPoint);
        damageable.OnInvalid(true);
        while (isReach)
            yield return null;

        damageable.OnInvalid(false);
        stateable.ResetStatus();
        isInitializing = false;
        state = STATE.Stay;
    }

    public void OnLeftMouseClick()
    {
        InputManager.Instance.OnTarget(this);
    }

    public void OnRightMouseClick()
    {
        if (stateable.isDead)
            DropManager.Instance.OnSwitchUI(drops);
    }

    public void Respwan(Vector3 pos)
    {
        isInPool = false;
        transform.position = pos;
        agent.enabled = true;

        birthPoint = transform.position;

        drops = null;
        skinnedMeshRenderer.material = materials[0];

        statusBar.Setup(stateable);
        stateable.ResetStatus();

        agent.speed = stateable.MoveSpeed;
        attackRange = stateable.AttackRange;
        attackRate = stateable.AttackRate;
        state = STATE.Stay;
    }
    public void InPool(Vector3 pos)
    {
        agent.enabled = false;
        transform.position = pos;
        isInPool = true;
    }
    public void OnDead()
    {
        enemyManager.onKillEvent?.Invoke();

        state = STATE.Stay;
        animator.SetBool("isDead", true);
        skinnedMeshRenderer.material = materials[1];
        drops = DropManager.Instance.GetDropInfo(stateable.Name);
        StartCoroutine(DeadToDestroy());
    }

    IEnumerator DeadToDestroy()
    {
        yield return new WaitForSeconds(20f);
        enemyManager.Dead(this);
    }

    public void OnMouseOver()
    {
        MouseTip.Instance.OnShowUI(stateable.Name);
    }
}