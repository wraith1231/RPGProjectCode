using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGhoulController : BattleMonsterController
{

    protected LeftWeaponHolder _leftAttack;
    protected RightWeaponHolder _rightAttack;

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
        base.FixedUpdate();

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
        else if(_state == Define.HeroState.Strafe)
        {
            _transform.position += _transform.forward * _animationSpeed[(int)Define.HeroState.Running] * 2 * Time.deltaTime;
        }
    }

    protected override void DyingProcess()
    {
        _deadTime += Time.deltaTime;
        if (_isDead == false)
        {
            _isDead = true;
            _characterCollider.enabled = false;
            _rigidBody.useGravity = false;
            StopAllCoroutines();

            AnimationStart(Define.HeroState.Die);
        }

        if (_deadTime >= 5.0f)
        {
            Managers.Battle.AnotherOneBiteDust();
            gameObject.SetActive(false);
        }
    }

    #region Attack
    private void AttackActivate(bool activate)
    {
        _leftAttack.SetActive(true);
        _rightAttack.SetActive(true);
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
}
