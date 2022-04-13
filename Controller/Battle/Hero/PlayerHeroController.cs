using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeroController : BattleHeroController
{
    //외부
    private CameraController _camera = null;

    //내부
    //상태에 따라 실행할 함수
    private delegate void CurrentState();
    private CurrentState currentState;

    //캐릭터 상태

    //입력값
    private float _vertical = 0f;
    private float _horizontal = 0f;
    private float _prevMouse = 0f;

    //rolling용
    private Vector3 _prevDir;

    #region PublicZone
    public override Define.HeroState State         //내부에서 _state 바꿔도 이걸 통해서 할 것
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
        //manager에 연결
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
        base.BeforeAttack();
    }
    protected override void AfterAttack()
    {
        base.AfterAttack();

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

            StartCoroutine(BlockProcess());
        }
    }
    protected override void BeforeBlocking()
    {
        base.BeforeBlocking();
    }
    protected override void AfterBlocking()
    {
        base.AfterBlocking();

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

        base.BeforeRolling();
    }
    protected override void AfterRolling()
    {
        base.AfterRolling();

        _transform.LookAt(_transform.position + _prevDir);

        AfterBehavior();
    }
    #endregion

    #region Damaged
    protected override void BeforeDamaged()
    {
        base.BeforeDamaged();
    }
    protected override void AfterDamaged()
    {
        base.AfterDamaged();

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
