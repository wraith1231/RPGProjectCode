using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public abstract class EnemyHeroController : BattleHeroController
{
    protected HeroNode _root = null;
    protected SphereCollider _nearEnemyCollider;
    public Animator HeroAnimator { get { return _animator; } }

    protected float _detectRange = 30f;

    protected float _runRange = 10f;
    public float RunRange { get { return _runRange; } set { _runRange = value; } }

    protected float _attackRange = 1.5f;
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }

    protected float _patrolDist = 2.0f;
    public float PatrolDist { get { return _patrolDist; } set { _patrolDist = value; } }

    public Vector3 RollingDirection { get { return _rollingDirection; } set { _rollingDirection = value; } }

    public float AnimFixedTime { get { return _fixedTime; } }

    public override Define.HeroState State { get { return _state; } protected set { _state = value; } }

    protected List<int> _charKeys = new List<int>();
    protected Dictionary<int, BattleHeroController> _nearCharacter = new Dictionary<int, BattleHeroController>();
    protected BattleHeroController _currentNearCharacter = null;
    public float TargetDistance { get; set; }
    public bool InBattleTarget { get; set; }
    public Define.HeroState NextState { get; set; }

    public override void Init()
    {
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();

        _nearEnemyCollider = GetComponent<SphereCollider>();
        _nearEnemyCollider.isTrigger = true;
        _nearEnemyCollider.radius = _detectRange;
        _nearEnemyCollider.enabled = false;

        for (int i = 0; i < (int)Define.HeroState.Unknown; i++)
            AnimationSpeedChange((Define.HeroState)i, 1.0f * ( 1 + _battleData.FinalDexterity));
    }
    public void SetHeroState(Define.HeroState state)
    {
        _state = state;
    }

    #region TriggerEvent
    private void OnTriggerEnter(Collider other)
    {
        BattleHeroController controller = other.GetComponent<BattleHeroController>();

        if (controller == null || controller.State == Define.HeroState.Die)
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
    public BattleHeroController CalculateNearestCharacter()
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

    public BattleHeroController GetNearestCharacter()
    {
        return _currentNearCharacter;
    }

#endregion

#region Attack
    public bool PlayAttackAnimation()
    {
        if(_attacking == false)
        {
            StartCoroutine(AttackProcess());
        }

        return _attacking;
    }
    protected override void BeforeAttack()
    {
        _nearEnemyCollider.enabled = false;
        _battleData.CurrentStaminaPoint -= AttackStamina;

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
    public bool PlayBlockAnimation()
    {
        if (_isBlock == false)
        {
            StartCoroutine(BlockProcess());
        }

        return _isBlock;
    }

    protected override void BeforeBlocking()
    {
        base.BeforeBlocking();
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
    public bool PlayRollAnimation()
    {
        if (_isRolling == false)
        {
            StartCoroutine(RollingProcess());
        }

        return _isRolling;
    }
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
    public override void GetDamaged(BattleHeroController attacker)
    {
        base.GetDamaged(attacker);

        NextState = Define.HeroState.Damaged;
        if(_nearCharacter.ContainsKey(attacker.HeroId) == false)
            _nearCharacter.Add(attacker.HeroId, attacker);
    }

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
    protected override void DyingProcess()
    {
        _deadTime += Time.deltaTime;
        if (_isDead == false)
        {
            _isDead = true;
            _characterCollider.enabled = false;
            StopAllCoroutines();

            AnimationStart(Define.HeroState.Die);
        }

        if (_deadTime >= 5.0f)
        {
            Managers.Battle.AnotherOneBiteDust();
            gameObject.SetActive(false);
        }
    }

    //protected override void ResetBooleanValues()
    //{
    //    base.ResetBooleanValues();
    //}

    protected override void FixedUpdate()
    {
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

    public abstract Define.HeroState CheckNextState();
}
