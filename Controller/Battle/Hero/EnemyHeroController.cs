using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public abstract class EnemyHeroController : BattleHeroController
{
    protected NodeBase _root = null;

    public override Define.HeroState State { get { return _state; } protected set { _state = value; } }

    public override void Init()
    {
        _nearEnemyCollider = GetComponent<SphereCollider>();
        _nearEnemyCollider.isTrigger = true;
        _nearEnemyCollider.radius = _detectRange;
        _nearEnemyCollider.enabled = false;
        AnimationStart(State);

    }
    public override void SetHeroState(Define.HeroState state)
    {
        _state = state;
    }

    #region TriggerEvent
    private void OnTriggerEnter(Collider other)
    {
        BattleCharacterController controller = other.GetComponent<BattleCharacterController>();

        if (controller == null || controller.State == Define.HeroState.Die)
            return;
        if (controller.IsHero == false)
            return;

        int id = controller.HeroId;
        if (_nearCharacter.ContainsKey(id) == false)
        {
            int group = controller.Group;
            if (Group != group)
            {
                _charKeys.Add(id);
                _nearCharacter[id] = controller;
            }
        }
        CalculateNearestCharacter();
    }

    private void OnTriggerExit(Collider other)
    {
        BattleHeroController controller = other.GetComponent<BattleHeroController>();

        if (controller == null)
            return;

        int id = controller.HeroId;
        if (_nearCharacter.ContainsKey(id) == true)
        {
            if (State != Define.HeroState.Attack)
            {
                _charKeys.Remove(id);
                _nearCharacter.Remove(id);
            }
        }
        CalculateNearestCharacter();
    }
    #endregion

    #region NearCharacter
    public override BattleCharacterController CalculateNearestCharacter()
    {
        _nearEnemyCollider.enabled = true;
        if (_charKeys.Count == 0)
        {
            return null;
        }

        int size = _charKeys.Count;
        float dist = 0;
        float min = 99999;
        _currentNearCharacter = null;

        for (int i = 0; i < size;)
        {
            if (_nearCharacter[_charKeys[i]].State == Define.HeroState.Die)
            {
                _nearCharacter.Remove(_charKeys[i]);
                _charKeys.RemoveAt(i);
                size--;

                continue;
            }

            dist = Vector3.Distance(_transform.position, _nearCharacter[_charKeys[i]].transform.position);
            if (dist < min)
            {
                min = dist;
                _currentNearCharacter = _nearCharacter[_charKeys[i]];
            }
            i++;
        }

        TargetDistance = min;
        _nearEnemyCollider.enabled = false;
        return _currentNearCharacter;
    }

    public override BattleCharacterController GetNearestCharacter()
    {
        return _currentNearCharacter;
    }

#endregion

#region Attack
    protected override void BeforeAttack()
    {
        _nearEnemyCollider.enabled = false;

        base.BeforeAttack();
    }
    protected override void AfterAttack()
    {
        base.AfterAttack();

        _nearEnemyCollider.enabled = true;
        InBattleTarget = false;
        NextState = Define.HeroState.Idle;

        AnimationStart(Define.HeroState.Idle);
    }

#endregion

#region Block
    protected override void BeforeBlocking()
    {
        base.BeforeBlocking();
        _blockEnd = true;
    }

    protected override void AfterBlocking()
    {
        base.AfterBlocking();

        InBattleTarget = false;
        NextState = Define.HeroState.Idle;

        AnimationStart(Define.HeroState.Idle);
    }
#endregion

#region Rolling
    protected override void BeforeRolling()
    {
        _transform.LookAt(RollingDirection);
        base.BeforeRolling();
    }
    protected override void AfterRolling()
    {
        base.AfterRolling();

        NextState = Define.HeroState.Idle;
        InBattleTarget = false;

        AnimationStart(Define.HeroState.Idle);
    }
#endregion

#region Damaged
    protected override void BeforeDamaged()
    {
        InBattleTarget = false;

        base.BeforeDamaged();
    }
    protected override void AfterDamaged()
    {
        base.AfterDamaged();

        NextState = Define.HeroState.Idle;
        InBattleTarget = false;

        AnimationStart(Define.HeroState.Idle);
    }
#endregion


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
            _battleData.CurrentStaminaPoint -= _battleData.StaminaRecovery * Time.deltaTime;

            if (_battleData.CurrentStaminaPoint < 0) _battleData.CurrentStaminaPoint = 0;
        }
        else if (_state != Define.HeroState.Block)
        {
            _battleData.CurrentStaminaPoint += _battleData.StaminaRecovery * Time.deltaTime;

            if (_battleData.CurrentStaminaPoint > _battleData.MaxStaminaPoint) _battleData.CurrentStaminaPoint = _battleData.MaxStaminaPoint;
        }
    }
}
