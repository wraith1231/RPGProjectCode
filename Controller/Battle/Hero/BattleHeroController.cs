using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleHeroController : MonoBehaviour
{
    //general
    [SerializeField] public static float AttackStamina = 30f;
    [SerializeField] public static float RollingStamina = 30f;
    [SerializeField] public static float BlockStamina = 20f;
    [SerializeField] protected float _fixedTime = 0.1f;     //animation ������
    protected Transform _transform;
    protected Transform _target = null;
    protected Animator _animator;
    protected CharacterData _data;
    protected Define.HeroState _state = Define.HeroState.Idle;
    public virtual Define.HeroState State { get; protected set; }
    protected BattleCharacterData _battleData;
    public CharacterData Data { get { return _data; } }
    public BattleCharacterData BattleData { get { return _battleData; } set { _battleData = value; } }

    //�ִϸ��̼� ���ǵ�
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
    //start�� �� ����� �ǵ��� �ʱ�
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
        string key;

        if((left.GetWeaponType() == Define.WeaponType.Unknown
            || left.GetWeaponType() == Define.WeaponType.Shield)
            && (left.GetWeaponType() == Define.WeaponType.Unknown
            || right.GetWeaponType() == Define.WeaponType.Shield))
        {
            key = "Unarmed";
        }
        else if(right.GetCategory() == Define.WeaponCategory.TwoHand)
        {
            key = "Twohand/";

            if (right.GetWeaponType() == Define.WeaponType.Axe
                || right.GetWeaponType() == Define.WeaponType.Mace)
                key += "Blunt";

            if (right.GetWeaponType() == Define.WeaponType.Sword)
                key += "Sword";

            if (right.GetWeaponType() == Define.WeaponType.Spear)
                key += "Spear";
        }
        else
        {
            if(left.GetWeaponType() == Define.WeaponType.Shield)
            {
                key = "LeftShield/";

                if (right.GetWeaponType() == Define.WeaponType.Sword
                    || right.GetWeaponType() == Define.WeaponType.Dagger)
                    key += "Sword";

                else if (right.GetWeaponType() == Define.WeaponType.Mace
                    || right.GetWeaponType() == Define.WeaponType.Axe)
                    key += "Blunt";
            }
            else if (right.GetWeaponType() == Define.WeaponType.Shield)
            {
                key = "RightShield/";

                if (left.GetWeaponType() == Define.WeaponType.Sword
                    || left.GetWeaponType() == Define.WeaponType.Dagger)
                    key += "Sword";

                else if (left.GetWeaponType() == Define.WeaponType.Mace
                    || left.GetWeaponType() == Define.WeaponType.Axe)
                    key += "Blunt";
            }
            else if(left.GetWeaponType() == Define.WeaponType.Unknown)
            {
                key = "Right/";

                if (right.GetWeaponType() == Define.WeaponType.Sword
                    || right.GetWeaponType() == Define.WeaponType.Dagger)
                    key += "Sword";

                else if (right.GetWeaponType() == Define.WeaponType.Mace
                    || right.GetWeaponType() == Define.WeaponType.Axe)
                    key += "Blunt";
            }
            else if (right.GetWeaponType() == Define.WeaponType.Unknown)
            {
                key = "Left/";

                if (left.GetWeaponType() == Define.WeaponType.Sword
                    || left.GetWeaponType() == Define.WeaponType.Dagger)
                    key += "Sword";

                else if (left.GetWeaponType() == Define.WeaponType.Mace
                    || left.GetWeaponType() == Define.WeaponType.Axe)
                    key += "Blunt";
            }
            else
            {
                key = "Dual/";

                if (left.GetWeaponType() == Define.WeaponType.Sword
                    || left.GetWeaponType() == Define.WeaponType.Dagger)
                    key += "Sword";
                else if (left.GetWeaponType() == Define.WeaponType.Mace
                    || left.GetWeaponType() == Define.WeaponType.Axe)
                    key += "Blunt";

                if (right.GetWeaponType() == Define.WeaponType.Sword
                    || right.GetWeaponType() == Define.WeaponType.Dagger)
                    key += "Sword";

                else if (right.GetWeaponType() == Define.WeaponType.Mace
                    || right.GetWeaponType() == Define.WeaponType.Axe)
                    key += "Blunt";
            }
        }

        Managers.Resource.Load<RuntimeAnimatorController>(key, AnimatorSetting);

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

    private void AnimatorSetting(RuntimeAnimatorController controller)
    {
        if (controller != null)
        {
            _animator.runtimeAnimatorController = controller;
        }
        else
        {
            Debug.Log($"Missing animator controller");
        }
    }

    protected void FindWeaponFile(bool isRight, EquipWeapon weapon, WeaponHolder holder)
    {
        string key = "";

        switch (weapon.GetCategory())
        {
            case Define.WeaponCategory.Unknown:
                return;
            case Define.WeaponCategory.OneHand:
                key += "OneHand/";
                break;
            case Define.WeaponCategory.TwoHand:
                key += "TwoHand/";
                break;
            case Define.WeaponCategory.Shield:
                key += "Shield/";
                break;
        }

        key += weapon.GetFileName();

        if (isRight == true)
        {
            Managers.Resource.Instantiate(key, SetRightWeapon);
            //GameObject go = Managers.Resource.Instantiate(path, holder.transform);
            //go.transform.localPosition = weapon.GetRightPosition();
            //go.transform.localEulerAngles = weapon.GetRightRotation();
            //go.transform.localScale = weapon.GetSize();
        }   
        else
        {
            Managers.Resource.Instantiate(key, SetLeftWeapon);
            //GameObject go = Managers.Resource.Instantiate(path, holder.transform);
            //go.transform.localPosition = weapon.GetLeftPosition();
            //go.transform.localEulerAngles = weapon.GetLeftRotation();
            //go.transform.localScale = weapon.GetSize();
        }
    }

    private void SetRightWeapon(GameObject go)
    {
        go.transform.parent = _rightWeaponHolder.transform;
        go.transform.localPosition = _rightEquipWeapon.GetRightPosition();
        go.transform.localEulerAngles = _rightEquipWeapon.GetRightRotation();
        go.transform.localScale = _rightEquipWeapon.GetSize();
    }

    private void SetLeftWeapon(GameObject go)
    {
        go.transform.parent = _leftWeaponHolder.transform;
        go.transform.localPosition = _leftEquipWeapon.GetRightPosition();
        go.transform.localEulerAngles = _leftEquipWeapon.GetRightRotation();
        go.transform.localScale = _leftEquipWeapon.GetSize();
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

    //���� ���� animation ���� coroutine�� before, after �Լ��� �߻����� �ּ� �����ϵ��� �Ұ�

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
            //�и� �ȴ��� ���
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
            //�и� ���� ���
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
