using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleMonsterController : BattleCharacterController
{
    protected NodeBase _root = null;

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

    protected override void FixedUpdate()
    {
        if (State == Define.HeroState.Idle)
            _idleTime += Time.deltaTime;
    }
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
