using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMonsterController : BattleCharacterController
{
    protected NodeBase _root = null;

    protected LeftWeaponHolder _leftAttack;
    protected RightWeaponHolder _rightAttack;

    public override Define.HeroState State { get { return _state; } protected set { _state = value; } }


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        _rigidBody = GetComponent<Rigidbody>();
        _isHero = true;

        _nearEnemyCollider = GetComponent<SphereCollider>();
        _nearEnemyCollider.isTrigger = true;
        _nearEnemyCollider.radius = _detectRange;
        _nearEnemyCollider.enabled = false;

        for (int i = 0; i < (int)Define.HeroState.Unknown; i++)
            AnimationSpeedChange((Define.HeroState)i, 1.0f * (1 + _battleData.FinalAgility));

        Init();
    }

    public override void Init()
    {
        _leftAttack = gameObject.GetComponentInChildren<LeftWeaponHolder>();
        _rightAttack = gameObject.GetComponentInChildren<RightWeaponHolder>();
        _characterCollider = GetComponent<CapsuleCollider>();
        _attackAnimCount = 1;

        _leftAttack.CheckColliders(true, this);
        _rightAttack.CheckColliders(true, this);
    }
    
    protected override void FixedUpdate()
    {
        if (State == Define.HeroState.Idle)
            _idleTime += Time.deltaTime;

        if (_battleData.CurrentHealthPoint <= 0 && State != Define.HeroState.Die)
        {
            State = Define.HeroState.Die;
        }
        if (State == Define.HeroState.Die)
        {
            DyingProcess();
            return;
        }
        else if (_root != null && State != Define.HeroState.Damaged)
            _root.Evaluate();

        if (_state == Define.HeroState.Running)
        {
            _transform.position += _transform.forward * _animationSpeed[(int)Define.HeroState.Running] * 3 * Time.deltaTime;
            _battleData.CurrentStaminaPoint -= _battleData.StaminaRecovery * Time.deltaTime;

            if (_battleData.CurrentStaminaPoint < 0) _battleData.CurrentStaminaPoint = 0;
        }
        else if (_state != Define.HeroState.Block)
        {
            _battleData.CurrentStaminaPoint += _battleData.StaminaRecovery * Time.deltaTime;

            if (_battleData.CurrentStaminaPoint > _battleData.MaxStaminaPoint) _battleData.CurrentStaminaPoint = _battleData.MaxStaminaPoint;
        }
        else if (_state == Define.HeroState.Strafe)
        {
            _transform.position += _transform.forward * _animationSpeed[(int)Define.HeroState.Running] * 2 * Time.deltaTime;
        }
    }


    #region Attack
    private void AttackActivate(bool activate)
    {
        _leftAttack.SetActive(activate);
        _rightAttack.SetActive(activate);
    }
    protected override void BeforeAttack()
    {
        _attacking = true;
        _parried = false;
        _nearEnemyCollider.enabled = false;
        _battleData.CurrentStaminaPoint -= AttackStamina;

        AnimationStart(Define.HeroState.Attack);
    }

    protected override bool AttackPlaying()
    {
        float normalizedTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //패링 안당한 경우
        if (_parried == false)
        {
            if (normalizedTime >= 0.95f)
            {
                return true;
            }
            else if (normalizedTime >= 0.7f)
            {
                AttackActivate(false);
            }
            else if (normalizedTime >= 0.4f)
            {
                AttackActivate(true);
            }
        }
        //패링 당한 경우
        else
        {
            _animator.SetFloat("speed", -1f);
            if (_attackColliderEnabled == true)
            {
                AttackActivate(false);
            }
            if (normalizedTime <= 0f || normalizedTime >= 1f)
            {
                return true;
            }
        }

        return false;
    }

    protected override void AfterAttack()
    {
        _attacking = false;
        _parried = false;
        _nearEnemyCollider.enabled = true;

        AnimationStart(Define.HeroState.Idle);
    }


    #endregion

    #region Damage
    protected override void BeforeDamaged()
    {

        AttackActivate(false);
        ResetGeneralBooleanValues();
        _isDamaged = true;

        AnimationStart(Define.HeroState.Damaged);
    }

    protected override bool DamagedPlaying()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            return true;

        return false;
    }

    protected override void AfterDamaged()
    {
        _isDamaged = false;
        AnimationStart(Define.HeroState.Idle);
    }

    #endregion

    #region Block
    protected override void BeforeBlocking()
    {

    }

    protected override bool BlockPlaying()
    {
        return true;
    }

    protected override void AfterBlocking()
    {

    }
    #endregion
    /*

        protected override void DyingProcess()
        {

        }

        #region Attack
        protected override void BeforeAttack()
        {

        }

        protected override bool AttackPlaying()
        {
            return true;
        }

        protected override void AfterAttack()
        {

        }


        #endregion

        #region Damage
        protected override void BeforeDamaged()
        {

        }

        protected override bool DamagedPlaying()
        {
            return true;
        }

        protected override void AfterDamaged()
        {

        }

        #endregion

        #region Block
        protected override void BeforeBlocking()
        {

        }

        protected override bool BlockPlaying()
        {
            return true;
        }

        protected override void AfterBlocking()
        {

        }
        #endregion*/
}
