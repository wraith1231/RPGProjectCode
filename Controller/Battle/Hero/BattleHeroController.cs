using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleHeroController : BattleCharacterController
{

    private List<UnityEngine.Object> _attached = new List<UnityEngine.Object>();

    //attack
    protected bool _isOneHand = false;

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
        _rigidBody = GetComponent<Rigidbody>();
        _leftWeaponHolder = gameObject.GetComponentInChildren<LeftWeaponHolder>();
        _rightWeaponHolder = gameObject.GetComponentInChildren<RightWeaponHolder>();
        _characterCollider = GetComponent<CapsuleCollider>();
        _isHero = true;
        _animationRootMotion = true;

        for (int i = 0; i < (int)Define.HeroState.Unknown; i++)
            AnimationSpeedChange((Define.HeroState)i, 1.0f * (1 + _battleData.FinalAgility));

        CreateWeapon();

        Init();
    }

    private void OnDestroy()
    {
        _battleData.CurrentStaminaPoint = _battleData.MaxStaminaPoint;
        int size = _attached.Count;
        for (int i = 0; i < size; i++)
            Managers.Resource.Release(_attached[i]);

        _attached.Clear();
    }

    public override void GetParried()
    {
        _parried = true;
    }
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

        if ((left.GetWeaponType() == Define.WeaponType.Unknown
            || left.GetWeaponType() == Define.WeaponType.Shield)
            && (right.GetWeaponType() == Define.WeaponType.Unknown
            || right.GetWeaponType() == Define.WeaponType.Shield))
        {
            key = "Unarmed";
            _attackAnimCount = 7;
        }
        else if (right.GetCategory() == Define.WeaponCategory.TwoHand)
        {
            key = "TwoHand/";

            if (right.GetWeaponType() == Define.WeaponType.Axe
                || right.GetWeaponType() == Define.WeaponType.Mace)
            {
                key += "Blunt";
                _attackAnimCount = 6;
            }

            if (right.GetWeaponType() == Define.WeaponType.Sword)
            {
                key += "Sword";
                _attackAnimCount = 7;
            }

            if (right.GetWeaponType() == Define.WeaponType.Spear)
            {
                key += "Spear";
                _attackAnimCount = 7;
            }
        }
        else
        {
            if (left.GetWeaponType() == Define.WeaponType.Shield)
            {
                key = "LeftShield/";

                if (right.GetWeaponType() == Define.WeaponType.Sword
                    || right.GetWeaponType() == Define.WeaponType.Dagger)
                    key += "Sword";

                else if (right.GetWeaponType() == Define.WeaponType.Mace
                    || right.GetWeaponType() == Define.WeaponType.Axe)
                    key += "Blunt";

                _attackAnimCount = 7;
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
                _attackAnimCount = 7;

            }
            else if (left.GetWeaponType() == Define.WeaponType.Unknown)
            {
                key = "Right/";

                if (right.GetWeaponType() == Define.WeaponType.Sword
                    || right.GetWeaponType() == Define.WeaponType.Dagger)
                    key += "Sword";

                else if (right.GetWeaponType() == Define.WeaponType.Mace
                    || right.GetWeaponType() == Define.WeaponType.Axe)
                    key += "Blunt";

                _attackAnimCount = 7;
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

                _attackAnimCount = 7;
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

                _attackAnimCount = 7;
            }
        }

        Managers.Resource.Load<RuntimeAnimatorController>(key, AnimatorSetting);

        if (right.GetCategory() != Define.WeaponCategory.Unknown)
        {
            FindWeaponFile(true, right, _rightWeaponHolder);
        }
        if (left.GetCategory() != Define.WeaponCategory.Unknown)
        {
            FindWeaponFile(false, left, _leftWeaponHolder);
        }

        if (right.GetCategory() == Define.WeaponCategory.Unknown && left.GetCategory() == Define.WeaponCategory.Unknown)
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
            _attached.Add(controller);
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
                key += "Onehand/";
                break;
            case Define.WeaponCategory.TwoHand:
                key += "Twohand/";
                break;
            case Define.WeaponCategory.Shield:
                key += "Shield/";
                break;
        }

        key += weapon.GetFileName();

        if (isRight == true)
        {
            Managers.Resource.Instantiate(key, SetRightWeapon);
        }
        else
        {
            Managers.Resource.Instantiate(key, SetLeftWeapon);
        }
    }

    private void SetRightWeapon(GameObject go)
    {
        _attached.Add(go);
        go.transform.parent = _rightWeaponHolder.transform;
        go.transform.localPosition = _rightEquipWeapon.GetRightPosition();
        go.transform.localEulerAngles = _rightEquipWeapon.GetRightRotation();
        go.transform.localScale = _rightEquipWeapon.GetSize();
        _rightWeaponHolder.CheckColliders(_rightEquipWeapon.GetCategory() != Define.WeaponCategory.Unknown, this);
    }

    private void SetLeftWeapon(GameObject go)
    {
        _attached.Add(go);
        go.transform.parent = _leftWeaponHolder.transform;
        go.transform.localPosition = _leftEquipWeapon.GetLeftPosition();
        go.transform.localEulerAngles = _leftEquipWeapon.GetLeftRotation();
        go.transform.localScale = _leftEquipWeapon.GetSize();
        _leftWeaponHolder.CheckColliders(_leftEquipWeapon.GetCategory() != Define.WeaponCategory.Unknown, this);
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

    #region AttackProcess
    protected override void BeforeAttack()
    {
        _attacking = true;
        _parried = false;
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
                WeaponSetActive(false);
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
            if (_attackColliderEnabled == true)
            {
                WeaponSetActive(false);
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
    }
    #endregion

    #region RollProcess
    protected override void BeforeRolling()
    {
        _isRolling = true;
        _battleData.CurrentStaminaPoint -= RollingStamina;

        AnimationStart(Define.HeroState.Rolling);
    }

    protected override bool RollPlaying()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            return true;

        return false;
    }

    protected override void AfterRolling()
    {
        _isRolling = false;
    }
    #endregion

    #region BlockProcess
    protected override void BeforeBlocking()
    {
        _isBlock = true;
        _justGuard = true;
        _blockEnd = false;
        _blockHit = false;
        _battleData.CurrentStaminaPoint -= BlockStamina;
        AnimationStart(Define.HeroState.Block);
        WeaponSetActive(false);
    }

    protected override bool BlockPlaying()
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
                    return true;
            }
        }
        else
        {
            if (normalizedTime >= 0.9f)
            {
                if (_blockEnd == true)
                {
                    return true;
                }
                else
                {
                    _blockHit = false;
                    _animator.StopPlayback();
                    _animator.Play("Block");
                }
            }
        }

        return false;
    }

    protected override void AfterBlocking()
    {
        _justGuard = false;
        _isBlock = false;
        _blockEnd = false;
        _blockHit = false;
    }
    #endregion

    #region DamagedProcess
    public override void HitByOther(BattleCharacterController attack, Vector3 hitPoint)
    {
        if (State == Define.HeroState.Block)
        {
            float dot = Vector3.Dot(attack.transform.forward, _transform.forward);
            bool isOpposite = dot < -0.7 ? true : false;
            if (State == Define.HeroState.Block && isOpposite == true)
            {
                GetBlocked(attack, hitPoint);
                attack.GetParried();
            }
            else
            {
                GetDamaged(attack, transform.position);
            }
        }
        else //hero.state != block
        {
            GetDamaged(attack, transform.position);
        }

    }

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

    protected override void BeforeDamaged()
    {
        WeaponSetActive(false);
        ResetBooleanValues();
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
    }

    #endregion
}
