using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleHeroController : MonoBehaviour
{
    //general
    [SerializeField] public static float AttackStamina = 30f;
    [SerializeField] public static float RollingStamina = 30f;
    [SerializeField] public static float BlockStamina = 20f;
    [SerializeField] protected float _fixedTime = 0.1f;     //animation 보정용
    protected Transform _transform;
    protected Transform _target = null;
    protected Animator _animator;
    protected CharacterData _data;
    protected Define.HeroState _state = Define.HeroState.Idle;
    public virtual Define.HeroState State { get; protected set; }
    protected BattleCharacterData _battleData;
    public CharacterData Data { get { return _data; } }
    public BattleCharacterData BattleData { get { return _battleData; } set { _battleData = value; } }

    //애니메이션 스피드
    protected float[] _animationSpeed = new float[(int)Define.HeroState.Unknown];

    //attack
    protected bool _attacking = false;
    protected bool _parried = false;
    protected bool _isOneHand = false;
    protected bool _attackColliderEnabled = false;
    protected int _prevAttack = -1;
    public virtual bool Parried { get { return _parried; } set { _parried = value; } }

    //roll
    protected bool _isRolling = false;
    protected Vector3 _rollingDirection;

    //block
    protected bool _isBlock = false;
    protected bool _justGuard = false;
    protected bool _blockEnd = false;
    protected bool _blockHit = false;

    //damaged
    protected bool _isDamaged = false;

    //die
    protected bool _isDead = false;

    //weapon
    protected WeaponHolder _leftWeaponHolder;
    protected WeaponHolder _rightWeaponHolder;
    protected EquipWeapon _leftEquipWeapon;
    protected EquipWeapon _rightEquipWeapon;
    protected bool _leftWeapon = false;
    protected bool _rightWeapon = false;

    #region General
    //start는 각 멤버는 건들지 않기
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        _leftWeaponHolder = gameObject.GetComponentInChildren<LeftWeaponHolder>();
        _rightWeaponHolder = gameObject.GetComponentInChildren<RightWeaponHolder>();
        CreateWeapon();

        Init();
    }

    public void SetBattleCharacterData(GlobalCharacterData data)
    {
        _battleData = new BattleCharacterData(data);
    }
    public bool IsEnoughToBehavior(float stamina)
    {
        if (_battleData.CurrentStaminaPoint >= stamina)
            return true;

        return false;
    }

    protected abstract void Update();

    public abstract void Init();
    protected abstract void DyingProcess();

    private void StopCoroutines()
    {
        StopAllCoroutines();
    }

    public void SetCharacterData(CharacterData data)
    {
        _data = data;
    }

    public abstract void GetDamaged(BattleHeroController attacker);
    public abstract void GetBlocked(BattleHeroController attacker);
    #endregion

    #region WeaponAndAnimator
    public void SetEquipWeapon(EquipWeapon left = null, EquipWeapon right = null)
    {
        _leftEquipWeapon = left;
        _rightEquipWeapon = right;
    }

    public void CreateWeapon()
    {
        SetAnimatorAndWeapon(_leftEquipWeapon, _rightEquipWeapon);
    }

    protected void SetAnimatorAndWeapon(EquipWeapon left, EquipWeapon right)
    {
        string path = "Animation/Controller/Battle/";
        bool isDual = false;
        if ((right.GetCategory() == Define.WeaponCategory.Unknown && left.GetCategory() == Define.WeaponCategory.Unknown)
            || (right.GetWeaponType() == Define.WeaponType.Gauntlet)
            || (right.GetCategory() == Define.WeaponCategory.Shield && left.GetCategory() == Define.WeaponCategory.Shield))
        {
            path += "Unarmed";
            _leftWeapon = true;
            _rightWeapon = true;
        }
        if (right.GetCategory() == Define.WeaponCategory.TwoHand)
        {
            path += "TwoHand/";
            _rightWeapon = true;

            Define.WeaponType type = right.GetWeaponType();
            switch (type)
            {
                case Define.WeaponType.Sword:
                    path += "Sword";
                    break;
                case Define.WeaponType.Axe:
                    path += "Axe";
                    break;
                case Define.WeaponType.Dagger:
                    path += "Dagger";
                    break;
                case Define.WeaponType.Mace:
                    path += "Mace";
                    break;
                case Define.WeaponType.Spear:
                    path += "Spear";
                    break;
                case Define.WeaponType.Bow:
                    path += "Bow";
                    break;
            }
        }
        else if (right.GetCategory() == Define.WeaponCategory.OneHand)
        {
            if (left.GetCategory() == Define.WeaponCategory.OneHand)
            {
                path += "Dual/";
                isDual = true;
                _leftWeapon = true;
            }
            else if (left.GetCategory() == Define.WeaponCategory.Shield)
            {
                path += "OneHand/LeftShield/";
            }
            else
            {
                path += "OneHand/Right/";
            }
            _rightWeapon = true;

            Define.WeaponType type = right.GetWeaponType();
            switch (type)
            {
                case Define.WeaponType.Sword:
                    path += "Sword";
                    break;
                case Define.WeaponType.Axe:
                    path += "Axe";
                    break;
                case Define.WeaponType.Dagger:
                    path += "Dagger";
                    break;
                case Define.WeaponType.Mace:
                    path += "Mace";
                    break;
                case Define.WeaponType.Spear:
                    path += "Spear";
                    break;
                case Define.WeaponType.Bow:
                    path += "Bow";
                    break;
            }

            if (isDual == true)
            {
                type = left.GetWeaponType();
                switch (type)
                {
                    case Define.WeaponType.Sword:
                        path += "Sword";
                        break;
                    case Define.WeaponType.Axe:
                        path += "Axe";
                        break;
                    case Define.WeaponType.Dagger:
                        path += "Dagger";
                        break;
                    case Define.WeaponType.Mace:
                        path += "Mace";
                        break;
                    case Define.WeaponType.Spear:
                        path += "Spear";
                        break;
                    case Define.WeaponType.Bow:
                        path += "Bow";
                        break;
                }
            }
        } //right.category == onehand
        else if (left.GetCategory() == Define.WeaponCategory.OneHand)
        {
            if (right.GetCategory() == Define.WeaponCategory.Shield)
            {
                path += "OneHand/RightShield/";
            }
            else
            {
                path += "OneHand/Left/";
            }
            _leftWeapon = true;
            Define.WeaponType type = left.GetWeaponType();
            switch (type)
            {
                case Define.WeaponType.Sword:
                    path += "Sword";
                    break;
                case Define.WeaponType.Axe:
                    path += "Axe";
                    break;
                case Define.WeaponType.Dagger:
                    path += "Dagger";
                    break;
                case Define.WeaponType.Mace:
                    path += "Mace";
                    break;
                case Define.WeaponType.Spear:
                    path += "Spear";
                    break;
                case Define.WeaponType.Bow:
                    path += "Bow";
                    break;
            }
        }

        RuntimeAnimatorController controller = Managers.Resource.Load<RuntimeAnimatorController>(path);
        if (controller != null)
        {
            _animator.runtimeAnimatorController = controller;
        }
        else
        {
            _animator.runtimeAnimatorController = Managers.Resource.Load<RuntimeAnimatorController>("Animation/Controller/Battle/Unarmed");
            Debug.Log($"Missing animator controller {path}");
        }

        if (right.GetCategory() != Define.WeaponCategory.Unknown)
        {
            FindWeaponFile(true, right, _rightWeaponHolder);
            _rightWeaponHolder.CheckColliders(left.GetCategory() != Define.WeaponCategory.Unknown, this);
        }
        if (left.GetCategory() != Define.WeaponCategory.Unknown)
        {
            FindWeaponFile(false, left, _leftWeaponHolder);
            _leftWeaponHolder.CheckColliders(right.GetCategory() != Define.WeaponCategory.Unknown, this);
        }

        if(right.GetCategory() == Define.WeaponCategory.Unknown && left.GetCategory() == Define.WeaponCategory.Unknown)
        {
            _rightWeaponHolder.CheckColliders(false, this);
            _leftWeaponHolder.CheckColliders(false, this);
        }
    }

    protected void FindWeaponFile(bool isRight, EquipWeapon weapon, WeaponHolder holder)
    {
        string path = "Weapons/";
        switch (weapon.GetCategory())
        {
            case Define.WeaponCategory.Unknown:
                return;
            case Define.WeaponCategory.OneHand:
                path += "OneHand/";
                break;
            case Define.WeaponCategory.TwoHand:
                path += "TwoHand/";
                break;
            case Define.WeaponCategory.Shield:
                path += "Shield/";
                break;
        }

        path += weapon.GetFileName();

        if (isRight == true)
        {
            GameObject go = Managers.Resource.Instantiate(path, holder.transform);
            go.transform.localPosition = weapon.GetRightPosition();
            go.transform.localEulerAngles = weapon.GetRightRotation();
            go.transform.localScale = weapon.GetSize();
        }
        else
        {
            GameObject go = Managers.Resource.Instantiate(path, holder.transform);
            go.transform.localPosition = weapon.GetLeftPosition();
            go.transform.localEulerAngles = weapon.GetLeftRotation();
            go.transform.localScale = weapon.GetSize();
        }
    }

    protected void WeaponSetActive(bool active)
    {
        if (_attackColliderEnabled == active)
            return;

        _attackColliderEnabled = active;

        if (_leftWeaponHolder.HasWeaponCollider() == true)
            _leftWeaponHolder.SetActive(active);
        if (_rightWeaponHolder.HasWeaponCollider() == true)
            _rightWeaponHolder.SetActive(active);
    }
    #endregion

    //이하 상태 animation 관련 coroutine은 before, after 함수를 추상으로 둬서 설정하도록 할것

    #region AttackProcess
    protected abstract void BeforeAttack();

    protected IEnumerator AttackProcess()
    {
        BeforeAttack();

        while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") == false)
            yield return null;
        while (true)
        {
            float normalizedTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            //패링 안당한 경우
            if (_parried == false)
            {
                if (normalizedTime >= 0.93f)
                {
                    WeaponSetActive(false);
                    break;
                }
                else if (normalizedTime >= 0.2f)
                {
                    WeaponSetActive(true);
                }
            }
            //패링 당한 경우
            else
            {
                _animator.SetFloat("speed", -1f);
                if(_attackColliderEnabled == true)
                {
                    WeaponSetActive(false);
                }
                if (normalizedTime <= 0f || normalizedTime >= 1f)
                {
                    break;
                }
            }
            yield return null;
        }

        AfterAttack();
    }

    protected abstract void AfterAttack();
    #endregion

    #region RollProcess
    protected abstract void BeforeRolling();
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
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                break;
            yield return null;
        }

        AfterRolling();
    }
    protected abstract void AfterRolling();
    #endregion

    #region BlockProcess
    protected abstract void BeforeBlocking();

    protected IEnumerator BlockProcess()
    {
        BeforeBlocking();
        WeaponSetActive(false);

        while (true)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Block") == true)
                break;
            yield return null;
        }

        while (true)
        {
            float normalizedTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (_blockHit == false)
            {
                if (_justGuard == true)
                {
                    if (normalizedTime >= 0.1f)
                        _justGuard = false;
                }

                if (normalizedTime >= 0.3f)
                {
                    if (_blockEnd == true)
                        break;
                }
            }
            else if (_blockHit == true)
            {
                if (normalizedTime >= 0.9f)
                {
                    if (_blockEnd == true)
                    {
                        break;
                    }
                    else
                    {
                        _blockHit = false;
                        _animator.Play("Block");
                    }
                }
            }
            yield return null;
        }

        AfterBlocking();
    }

    protected abstract void AfterBlocking();
    #endregion

    #region DamagedProcess
    protected virtual void ResetBooleanValues()
    {
        _attacking = false;
        _parried = false;
        _isOneHand = false;
        _prevAttack = -1;

        _isRolling = false;

        //block
        _isBlock = false;
        _justGuard = false;
        _blockEnd = false;
        _blockHit = false;

        _isDamaged = false;
    } 

    protected abstract void BeforeDamaged();

    protected IEnumerator DamagedProcess()
    {
        BeforeDamaged();
        WeaponSetActive(false);

        while (true)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
                break;

            yield return null;
        }

        AfterDamaged();
    }

    protected abstract void AfterDamaged();

    #endregion
}
