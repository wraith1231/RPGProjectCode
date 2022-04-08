using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeroController : BattleHeroController
{
    //�ܺ�
    private CameraController _camera = null;

    //����
    //���¿� ���� ������ �Լ�
    private delegate void CurrentState();
    private CurrentState currentState;

    //ĳ���� ����

    //�Է°�
    private float _vertical = 0f;
    private float _horizontal = 0f;
    private float _prevMouse = 0f;


    //rolling��
    private Vector3 _prevDir;

    #region PublicZone
    public override Define.HeroState State         //���ο��� _state �ٲ㵵 �̰� ���ؼ� �� ��
    { 
        get 
        { 
            return _state;
        }
        protected set
        {
            _state = value;
            switch(_state)
            {
                case Define.HeroState.Idle:
                    currentState = UpdateIdle;
                    break;
                case Define.HeroState.Strafe:
                    currentState = UpdateMove;
                    break;
                case Define.HeroState.Running:
                    currentState = UpdateMove;
                    break;
                case Define.HeroState.Rolling:
                    currentState = UpdateRolling;
                    break;
                case Define.HeroState.Attack:
                    currentState = UpdateAttack;
                    break;
                case Define.HeroState.Block:
                    currentState = UpdateBlock;
                    break;
                case Define.HeroState.Damaged:
                    currentState = UpdateDamaged;
                    break;
                case Define.HeroState.Die:
                    currentState = DyingProcess;
                    break;
                default:
                    break;
            }

        } 
    }

    public void SetCamera(CameraController cam)
    {
        _camera = cam;
    }
    #endregion

    #region General
    public override void Init()
    {
        //manager�� ����
        {
            Managers.Input.KeyAction -= OnKeyboardEvent;
            Managers.Input.KeyAction += OnKeyboardEvent;

            Managers.Input.LeftMouseAction -= OnLeftMouseEvent;
            Managers.Input.LeftMouseAction += OnLeftMouseEvent;

            Managers.Input.RightMouseAction -= OnRightMouseEvent;
            Managers.Input.RightMouseAction += OnRightMouseEvent;
        }

        if (Managers.Battle.BothInit == true)
        {
            SetCamera(Managers.Battle.Camera);
            _camera.SetPlayer(this);
        }
        _prevMouse = Input.GetAxis("Mouse X");

        currentState = UpdateIdle;

        for (int i = 0; i < (int)Define.HeroState.Unknown; i++)
            AnimationSpeedChange((Define.HeroState)i, 1.0f * (1 + _battleData.FinalDexterity));

        AnimationSpeedChange(Define.HeroState.Rolling, 1.0f * (1 +_battleData.FinalDexterity));

        AnimationStart(State);
    }
    protected override void FixedUpdate()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        currentState();

        if (State == Define.HeroState.Running)
        {
            _battleData.CurrentStaminaPoint -= _battleData.StaminaRecovery * Time.deltaTime;

            if (_battleData.CurrentStaminaPoint < 0) _battleData.CurrentStaminaPoint = 0;
        }
        else if (State != Define.HeroState.Block)
        {
            _battleData.CurrentStaminaPoint += _battleData.HealthRecovery * Time.deltaTime;

            if (_battleData.CurrentStaminaPoint > _battleData.MaxStaminaPoint) _battleData.CurrentStaminaPoint = _battleData.MaxStaminaPoint;
        }
    }
    private void AnimationSpeedChange(Define.HeroState state, float speed)
    {
        _animationSpeed[(int)state] = speed;
    }
    private void AnimationStart(Define.HeroState state)
    {
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
                int rand = Random.Range(0, 7);
                if (rand == _prevAttack)
                {
                    rand = _prevAttack + 1;
                    if (rand >= 7)
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
                _animator.SetFloat("Attack", Random.Range(0, 2));
                _animator.Play("Death");
                break;
            default:
                break;
        }
    }
    private void AfterBehavior()
    {
        if (Input.anyKey == false)
        {
            AnimationStart(Define.HeroState.Idle);
            State = Define.HeroState.Idle;
        }
        else
        {
            AnimationStart(Define.HeroState.Strafe);
            State = Define.HeroState.Strafe;
        }
    }

    private void RotateCharacter()
    {
        if (_camera == null) return;

        float curMouse = Input.GetAxis("Mouse X");

        float power = (curMouse) * _camera.MouseSpeed * Time.deltaTime;
        _transform.RotateAround(_transform.position, Vector3.up, power);
    }

    private bool CanCurruptState()
    {
        if (State == Define.HeroState.Idle || State == Define.HeroState.Strafe || State == Define.HeroState.Running)
            return true;

        return false;
    }
    #endregion

    #region Non-fight
    private void UpdateIdle()
    {
        RotateCharacter();
    }

    private void UpdateMove()
    {
        RotateCharacter();
        if(Input.anyKey == false)
        {
            AnimationStart(Define.HeroState.Idle);
            State = Define.HeroState.Idle;
            return;
        }
    }

    protected override void DyingProcess()
    {
        if(_isDead == false)
        {
            _isDead = true;
            _characterCollider.enabled = false;
            StopAllCoroutines();

            AnimationStart(Define.HeroState.Die);
        }
    }

    #endregion

    #region Attack
    private void UpdateAttack()
    {
        if (_attacking == false)
            StartCoroutine(AttackProcess());
    }
    protected override void BeforeAttack()
    {
        _attacking = true;
        _parried = false;
        _battleData.CurrentStaminaPoint -= AttackStamina;

        AnimationStart(Define.HeroState.Attack);
    }
    protected override void AfterAttack()
    {
        _attacking = false;
        _parried = false;
        if (State != Define.HeroState.Block)
            AfterBehavior();
    }
    #endregion

    #region Block
    private void UpdateBlock()
    {
        RotateCharacter();
        if(_isBlock == false)
        {
            if(_attacking == true)
            {
                StopCoroutine(AttackProcess());
                AfterAttack();
            }
            _battleData.CurrentStaminaPoint -= BlockStamina;
            StartCoroutine(BlockProcess());
        }
    }
    protected override void BeforeBlocking()
    {
        _isBlock = true;
        _justGuard = true;
        _blockEnd = false;
        _blockHit = false;
        AnimationStart(Define.HeroState.Block);
    }
    protected override void AfterBlocking()
    {
        _justGuard = false;
        _isBlock = false;
        _blockEnd = false;
        _blockHit = false;
        AfterBehavior();
    }
    #endregion

    #region Roll
    private void UpdateRolling()
    {
        if (_isRolling == true)
            return;

        StartCoroutine(RollingProcess());
    }
    protected override void BeforeRolling()
    {
        _prevDir = _transform.forward;
        _transform.LookAt(_transform.position + _rollingDirection);
        _isRolling = true;
        _battleData.CurrentStaminaPoint -= RollingStamina;

        AnimationStart(Define.HeroState.Rolling);
    }
    protected override void AfterRolling()
    {
        _isRolling = false;
        _transform.LookAt(_transform.position + _prevDir);

        AfterBehavior();
    }
    #endregion

    #region Damaged
    protected override void BeforeDamaged()
    {
        _isDamaged = true;

        ResetBooleanValues();
        AnimationStart(Define.HeroState.Damaged);
    }
    protected override void AfterDamaged()
    {
        _isDamaged = false;
        AfterBehavior();
    }

    protected void UpdateDamaged()
    {
        if (_isDamaged == false)
        {
            StopAllCoroutines();
            StartCoroutine(DamagedProcess());
        }
    }
    #endregion

    #region InputFunction

    public void OnKeyboardEvent()
    {
        if (!(CanCurruptState()))
            return;

        State = Define.HeroState.Strafe;

        _animator.SetFloat("Horizontal", _horizontal);
        _animator.SetFloat("Vertical", _vertical);

        if (Input.GetAxisRaw("Run") > 0.5f)
        {
            State = Define.HeroState.Running;
            if (_animator.GetAnimatorTransitionInfo(0).anyState == false)
                _animator.CrossFade("Run", _fixedTime);
        }
        else
        {
            if (_animator.GetAnimatorTransitionInfo(0).anyState == false)
                _animator.CrossFade("Strafe", _fixedTime);
        }

        if (Input.GetButtonDown("Roll") == true && IsEnoughToBehavior(RollingStamina))
        {
            Vector3 move = new Vector3(_vertical, 0, _horizontal);
            float camDot = Vector3.Dot(transform.forward, move);
            Vector3 upVec = Vector3.Cross(move, transform.forward);
            float upDot = Vector3.Dot(upVec, transform.up);

            _rollingDirection = new Vector3(camDot, 0, -upDot);

            State = Define.HeroState.Rolling;
        }
    }

    public void OnLeftMouseEvent(Define.MouseEvent mouseEvent)
    {
        if (State == Define.HeroState.Die || _parried == true)
            return;

        switch (mouseEvent)
        {
            case Define.MouseEvent.PointerDown:
                if (_rightWeapon == true && _leftWeapon == false || _rightWeapon == true && _leftWeapon == true
                    || _rightWeapon == false && _leftWeapon == false)
                {
                    if (CanCurruptState() && IsEnoughToBehavior(AttackStamina))
                    {
                        State = Define.HeroState.Attack;
                    }
                }
                else if (_leftWeapon == true && _rightWeapon == false)
                {
                    if ((CanCurruptState() || State == Define.HeroState.Attack) && IsEnoughToBehavior(BlockStamina))
                        State = Define.HeroState.Block;
                }
                break;
            case Define.MouseEvent.PointerUp:
                if (State == Define.HeroState.Block && (_leftWeapon == true && _rightWeapon == false))
                    _blockEnd = true;
                break;
        }
    }
    public void OnRightMouseEvent(Define.MouseEvent mouseEvent)
    {
        if (State == Define.HeroState.Die || _parried == true)
            return;

        switch (mouseEvent)
        {
            case Define.MouseEvent.PointerDown:
                if (_leftWeapon == true && _rightWeapon == false)
                {
                    if (CanCurruptState() && IsEnoughToBehavior(AttackStamina))
                        State = Define.HeroState.Attack;
                }
                else if (_leftWeapon == false && _rightWeapon == true || _leftWeapon == true && _rightWeapon == true || _rightWeapon == false && _leftWeapon == false)
                {
                    if ((CanCurruptState() || State == Define.HeroState.Attack) && IsEnoughToBehavior(BlockStamina))
                        State = Define.HeroState.Block;
                }
                break;
            case Define.MouseEvent.PointerUp:
                if (State == Define.HeroState.Block && (_leftWeapon == false && _rightWeapon == true || _leftWeapon == true && _rightWeapon == true
                    || _rightWeapon == false && _leftWeapon == false))
                    _blockEnd = true;
                break;
        }
    }

    #endregion
}
