using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Attackable))]
[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(Stateable))]
public class PlayerCon : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float groundSphere;
    [SerializeField] private LayerMask groundLayer;
    
    [SerializeField] private Transform model;
    private Animator animator;

    private Rigidbody rigid;
    private new Transform transform;

    public NonPlayer target;

    private Attackable attackable;
    private Damageable damageable;
    public Stateable stateable;

    public float TargetDistance => target !=null ?
                    Vector3.Distance(target.transform.position, transform.position) :
                    -99f;
    private bool isGround
    {
        get
        {
            return animator.GetBool("isGround");
        }
        set
        {
            animator.SetBool("isGround", value);
        }
    }
    private bool isMove
    { 
        get
        {
            return animator.GetBool("isMove");
        }
        set
        {
            animator.SetBool("isMove", value);
        }
    }
    private void Start()
    {
        transform = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();

        attackable = GetComponent<Attackable>();
        damageable = GetComponent<Damageable>();
        stateable = GetComponent<Stateable>();
        target = null;

        animator = model.GetComponent<Animator>();

        InputManager.Instance.player = this;
        InputManager.Instance.AddKeyAction(KeyCode.Space, Jump);

        stateable.onDead += OnDead; 
        stateable.onDamage += () => { animator.SetTrigger("onDamage");};
        attackable.onAttack += () => { animator.SetTrigger("onAttack"); stateable.OnGetEnergy(3f); };
        attackable.onAttackCrit += () => { animator.SetTrigger("onAttackCrit"); stateable.OnGetEnergy(6f); };
        OnSkillAnimation();
    }
    private void Update()
    {
        animator.SetFloat("velocityY", rigid.velocity.y);

        isGround = Physics.CheckSphere(transform.position, groundSphere,groundLayer);
    }

    public void MoveByKey(float x, float z, float rorateY)
    {
        Rotate(rorateY);

        Vector3 dir = (model.right * x + model.forward * z).normalized;
        Vector3 movement = dir * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
    public void MoveByMouse()
    {
        StartCoroutine(MovementMouse());
    }
    IEnumerator MovementMouse()
    {
        while (Input.GetMouseButton(1))
        {
            float rorateY = Input.GetAxisRaw("Mouse X") * 2f;
            Rotate(rorateY);

            if (Input.GetMouseButton(0))
            {
                Vector3 movement = Camera.main.transform.forward * moveSpeed * Time.deltaTime;
                transform.Translate(movement);
                animator.SetFloat("InputZ",1);
            }
            yield return null;
        }
    }
    private void Rotate(float delta)
    {
        Vector3 euler = model.localRotation.eulerAngles;
        euler.y += delta * rotateSpeed * Time.deltaTime * Mathf.Rad2Deg;
        model.localRotation = Quaternion.Euler(euler);
    }
    private void Jump()
    {
        if (!isGround)
            return;

        animator.SetTrigger("onJump");
        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    public void OnTarget(NonPlayer target)
    {
        this.target = target;
        if (target is Enemy)
            attackable.OnTarget(target.GetComponent<Damageable>());
    }
    public Damageable Target()
    {
        return attackable.Target;
    }
    public void OnDead()
    {
        Debug.Log("³ª Á×¾î!!!");
        animator.SetTrigger("onDead");
    }

    public void SetAnimator(float x, float z)
    {
        Vector2 vector2 = new Vector2(x, z);
        isMove = vector2 != Vector2.zero;
        animator.SetFloat("InputX", x);
        animator.SetFloat("InputZ", z);
    }
    public void OnSkillAnimation(int skillID = -99)
    {
        animator.SetInteger("skillID",skillID);
        if (skillID >= 0)
            animator.SetTrigger("onSkill");
    }
}
