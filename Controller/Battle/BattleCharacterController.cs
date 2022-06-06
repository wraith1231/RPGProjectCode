using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCharacterController : MonoBehaviour
{
    protected Transform _transform;
    protected Transform _target = null;
    protected CapsuleCollider _characterCollider;
    protected SphereCollider _nearEnemyCollider;
    protected Rigidbody _rigidBody;
    protected int _attackAnimCount = 1;

    //애니메이션 스피드
    protected float[] _animationSpeed = new float[(int)Define.HeroState.Unknown];

    protected Animator _animator;
    public Animator Animator { get { return _animator; } }

    protected bool _isHero = false;
    public bool IsHero { get { return _isHero; } }

    protected int _heroId = -1;
    public int HeroId { get { return _heroId; } set { _heroId = value; } }

    protected int _group;
    public int Group { get { return _group; } set { _group = value; } }

    protected float _fixedTime = 0.1f;     //animation 보정용
    public float AnimFixedTime { get { return _fixedTime; } }

    protected BattleCharacterData _battleData;
    public BattleCharacterData BattleData { get { return _battleData; } set { _battleData = value; } }

    protected Define.HeroState _state = Define.HeroState.Idle;
    public virtual Define.HeroState State { get; protected set; }

    #region General Battle Value
    protected float _idleTime = 0f;
    public float IdleTime { get { return _idleTime; } }

    protected bool _attacking = false;
    protected bool _parried = false;
    protected int _prevAttack = -1;
    public virtual bool Parried { get { return _parried; } set { _parried = value; } }

    //roll
    protected bool _isRolling = false;

    //block
    protected bool _isBlock = false;
    protected bool _justGuard = false;
    protected bool _blockEnd = false;
    protected bool _blockHit = false;

    //damaged
    protected bool _isDamaged = false;

    //die
    protected bool _isDead = false;
    protected float _deadTime = 0;
    #endregion

    #region NPC Battle Value

    protected float _detectRange = 30f;
    protected float _runRange = 5f;
    public float RunRange { get { return _runRange; } set { _runRange = value; } }
    protected float _attackRange = 1.5f;
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    protected float _patrolDist = 2.0f;
    public float PatrolDist { get { return _patrolDist; } set { _patrolDist = value; } }
    protected Vector3 _rollingDirection;
    public Vector3 RollingDirection { get { return _rollingDirection; } set { _rollingDirection = value; } }

    public float TargetDistance { get; set; }
    public bool InBattleTarget { get; set; }
    public Define.HeroState NextState { get; set; }

    protected List<int> _charKeys = new List<int>();
    protected Dictionary<int, BattleCharacterController> _nearCharacter = new Dictionary<int, BattleCharacterController>();
    protected BattleCharacterController _currentNearCharacter = null;
    #endregion

    #region General Public Function Zone
    public void SetAnimatorVertical(float vertical)
    {
        _animator.SetFloat("Vertical", vertical);
    }
    public void SetAnimatorHorizontal(float horizontal)
    {
        _animator.SetFloat("Horizontal", horizontal);
    }
    public void SetAnimatorDirection(float vertical, float horizontal)
    {
        SetAnimatorVertical(vertical);
        SetAnimatorHorizontal(horizontal);
    }

    public void SetBattleCharacterData(GlobalCharacterData data)
    {
        _battleData = new BattleCharacterData(data);
    }

    public void HeadToDestination(Vector2 destination)
    {
        _transform.LookAt(new Vector3(destination.x, _transform.position.y, destination.y));
    }
    public void HeadToDestination(Vector3 destination)
    {
        _transform.LookAt(destination);
    }
    public void HeadToDestination(Transform transform)
    {
        _transform.LookAt(transform);
    }

    public Vector2 GetPositionByVector2()
    {
        return new Vector2(_transform.position.x, _transform.position.z);
    }
    public Vector3 GetPositionByVector3()
    {
        return _transform.position;
    }
    public float GetDistanceWithVector2(Vector2 dest)
    {
        Vector2 temp = new Vector2(_transform.position.x, _transform.position.z);

        return Vector2.Distance(temp, dest);
    }
    public float GetDistanceWithVector3(Vector3 dest)
    {
        return Vector3.Distance(_transform.position, dest);
    }
    public float GetDistanceVector2WithVector3(Vector3 dest)
    {
        Vector2 temp1 = new Vector2(_transform.position.x, _transform.position.z);
        Vector2 temp2 = new Vector2(dest.x, dest.z);

        return Vector2.Distance(temp1, temp2);
    }
    public float GetDistanceTarget(Transform target)
    {
        return Vector3.Distance(_transform.position, target.position);
    }

    #endregion

    #region General Protected Function Zone
    protected void AnimationSpeedChange(Define.HeroState state, float speed)
    {
        _animationSpeed[(int)state] = speed;
    }

    protected void AnimationStart(Define.HeroState state)
    {
        State = state;
        _animator.SetFloat("speed", _animationSpeed[(int)state]);
        switch (state)
        {
            case Define.HeroState.Idle:
                _animator.CrossFade("Idle", _fixedTime);
                break;
            case Define.HeroState.Strafe:
                _animator.CrossFade("Strafe", _fixedTime);
                break;
            case Define.HeroState.Running:
                _animator.CrossFade("Run", _fixedTime);
                break;
            case Define.HeroState.Rolling:
                _animator.Play("Roll");
                break;
            case Define.HeroState.Attack:
                int rand = UnityEngine.Random.Range(0, _attackAnimCount);
                if (rand == _prevAttack)
                {
                    rand = _prevAttack + 1;
                    if (rand >= _attackAnimCount)
                        rand = 0;

                    _prevAttack = rand;
                }
                _animator.SetFloat("Attack", rand);
                _animator.Play("Attack");
                break;
            case Define.HeroState.Block:
                _animator.Play("Block");
                break;
            case Define.HeroState.Damaged:
                _animator.Play("Damaged");
                break;
            case Define.HeroState.Die:
                _animator.SetFloat("Attack", UnityEngine.Random.Range(0, 2));
                _animator.Play("Death");
                break;
            default:
                break;
        }
    }

    #endregion

    #region Abstract zone
    public abstract void Init();
    protected abstract void FixedUpdate();
    protected abstract void DyingProcess();

    protected abstract void BeforeAttack();
    protected abstract bool AttackPlaying();
    protected abstract void AfterAttack();

    protected abstract void BeforeDamaged();
    protected abstract bool DamagedPlaying();
    protected abstract void AfterDamaged();

    protected abstract void BeforeBlocking();
    protected abstract bool BlockPlaying();
    protected abstract void AfterBlocking();

    #endregion

    #region Virtual But Not Initialized And Public Zone
    public virtual void GetParried() { }
    public virtual void SetHeroState(Define.HeroState state) { }
    public virtual Define.HeroState CheckNextState() { return Define.HeroState.Unknown; }
    public virtual BattleCharacterController GetNearestCharacter() { return null; }
    public virtual BattleCharacterController CalculateNearestCharacter() { return null; }
    #endregion

    #region Virtual But Not Initialized And Protected Zone
    protected virtual void BeforeRolling() { }
    protected virtual bool RollPlaying() { return true; }
    protected virtual void AfterRolling() { }

    #endregion

    #region Virtual Initialized Zone
    public virtual bool IsEnoughToBehavior(float stamina)
    {
        if (_battleData.CurrentStaminaPoint >= stamina)
            return true;

        return false;
    }

    public virtual bool PlayAnimation(Define.HeroState state)
    {
        switch (state)
        {
            case Define.HeroState.Idle:
                if (State == state)
                    return true;
                AnimationStart(state);
                break;

            case Define.HeroState.Strafe:
                if (State == state)
                    return true;
                _idleTime = 0f;
                AnimationStart(state);

                break;
            case Define.HeroState.Running:
                if (State == state)
                    return true;
                _idleTime = 0f;
                AnimationStart(state);

                break;
            case Define.HeroState.Attack:
                _idleTime = 0f;
                if (_attacking == false)
                {
                    StartCoroutine(AttackProcess());
                }

                return _attacking;
            case Define.HeroState.Block:
                _idleTime = 0f;
                if (_isBlock == false)
                {
                    StartCoroutine(BlockProcess());
                }

                return _isBlock;
            case Define.HeroState.Rolling:
                _idleTime = 0f;
                if (_isRolling == false)
                {
                    StartCoroutine(RollingProcess());
                }

                return _isRolling;
        }
        return false;
    }

    //playing 시리즈는 true 리턴하면 끝임
    protected virtual IEnumerator AttackProcess()
    {
        BeforeAttack();

        while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
            yield return null;
        while (true)
        {
            if (AttackPlaying() == true)
                break;

            yield return null;
        }

        AfterAttack();
    }
    protected IEnumerator BlockProcess()
    {
        BeforeBlocking();

        while (true)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Block") == true)
                break;
            yield return null;
        }

        while (true)
        {
            if (BlockPlaying() == true)
                break;
            yield return null;
        }

        AfterBlocking();
    }
    protected virtual IEnumerator DamagedProcess()
    {
        BeforeDamaged();

        while (true)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged") == true)
                break;

            yield return null;
        }
        while (true)
        {
            if (DamagedPlaying() == true)
                break;

            yield return null;
        }

        AfterDamaged();
    }
    protected virtual IEnumerator RollingProcess()
    {
        BeforeRolling();

        while (true)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Roll") == true)
                break;
            yield return null;
        }

        while (true)
        {
            if (RollPlaying() == true)
                break;

            yield return null;
        }

        AfterRolling();
    }

    public virtual void GetDamaged(BattleCharacterController attacker, Vector3 hitPoint)
    {
        if (State == Define.HeroState.Damaged || State == Define.HeroState.Die || State == Define.HeroState.Rolling) return;
        State = Define.HeroState.Damaged;
        Managers.VFX.Instantiate("HitEffect", hitPoint);

        float defense = 1 - _battleData.FinalDefense / (_battleData.FinalDefense + 50);
        _battleData.CurrentHealthPoint -= (attacker.BattleData.FinalPower * defense);

        if (_battleData.CurrentHealthPoint <= 0)
        {
            State = Define.HeroState.Die;
            return;
        }
        StopAllCoroutines();
        _transform.LookAt(attacker.transform);

        StartCoroutine(DamagedProcess());
    }

    public virtual void GetBlocked(BattleCharacterController attacker, Vector3 hitPoint)
    {
        float defense = 1f - _battleData.FinalDefense / (_battleData.FinalDefense + 50f);
        float attack = _battleData.FinalPower * _battleData.DefenseAdvantage - attacker.BattleData.FinalPower;
        if (attack < 0)
        {
            GetDamaged(attacker, hitPoint);
            return;
        }

        float blockDamage = (attacker.BattleData.FinalPower * defense);

        if (_justGuard == true)
        {

            _battleData.CurrentStaminaPoint -= blockDamage * 0.5f;
            if (_battleData.CurrentStaminaPoint < 0) _battleData.CurrentStaminaPoint = 0;
        }
        else
        {
            if (blockDamage > _battleData.CurrentStaminaPoint * 0.5f)
            {
                GetDamaged(attacker, hitPoint);
                return;
            }
            _battleData.CurrentStaminaPoint -= blockDamage;
            if (_battleData.CurrentStaminaPoint < 0) _battleData.CurrentStaminaPoint = 0;
        }

        _blockHit = true;
        _animator.Play("BlockHit");
    }
    #endregion
}
