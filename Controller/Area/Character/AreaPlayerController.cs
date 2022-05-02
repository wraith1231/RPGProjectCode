using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPlayerController : AreaGroupController
{
    private AreaCameraController _camera;
    public AreaCameraController AreaCamera { get { return _camera; } set { _camera = value; } }

    private delegate void CurrentUpdate();
    private CurrentUpdate _currentUpdate;

    //terrain과 village만 일단
    private int _mouseMask = 1 << 7 | 1 << 9;
    private GameObject _currentObject = null;
    private Vector3 _currentPoint;

    public override Define.AreaStatus Status {
        get => base.Status; 
        set 
        {
            _status = value;
            switch (_status)
            {
                case Define.AreaStatus.Idle:
                    _currentUpdate = UpdateIdle;
                    break;
                case Define.AreaStatus.Move:
                    _currentUpdate = UpdateMove;
                    break;
                case Define.AreaStatus.Battle:
                    _currentUpdate = UpdateBattle;
                    break;
                case Define.AreaStatus.Unknown:
                    _currentUpdate = UpdateIdle;
                    break;
            }
        }
    }

    protected override void Initialize()
    {
        Managers.Input.LeftMouseAction -= OnLeftMouseEvent;
        Managers.Input.LeftMouseAction += OnLeftMouseEvent;

        Managers.Input.RightMouseAction -= OnRightMouseEvent;
        Managers.Input.RightMouseAction += OnRightMouseEvent;

        _currentUpdate = UpdateIdle;
    }

    public void OnLeftMouseEvent(Define.MouseEvent mouseEvent)
    {
        switch (mouseEvent)
        {
            case Define.MouseEvent.PointerDown:


                break;
        }
    }

    public void OnRightMouseEvent(Define.MouseEvent mouseEvent)
    {

        switch (mouseEvent)
        {
            case Define.MouseEvent.PointerDown:
                if(Input.GetKey(KeyCode.LeftShift) == true)
                {
                    if (_currentObject == null)
                    {
                        _destination.Enqueue(_currentPoint);
                    }
                    else
                    { 
                        _destination.Enqueue(_currentObject.transform.position);
                    }
                }
                else
                {
                    _destination.Clear();
                    if (_currentObject == null)
                    {
                        if (Status == Define.AreaStatus.Move)
                            _moveInterrupted = true;
                        _destination.Enqueue(_currentPoint);
                        //_transform.LookAt(_currentPoint);
                    }
                    else
                    {
                        if (Status == Define.AreaStatus.Move)
                            _moveInterrupted = true;
                        _destination.Enqueue(_currentObject.transform.position);
                        //_transform.LookAt(_currentPoint);
                    }
                }

                break;
        }
    }

    protected override void FixedUpdate()
    {
        _currentUpdate();
    }

    private void RayUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit rayHit;
        bool check = Physics.Raycast(ray, out rayHit, 300.0f, _mouseMask);

        _currentPoint = rayHit.point;
        if(rayHit.collider == null)
        {
            _currentObject = null;
            return;
        }
        if (rayHit.collider.tag != "Terrain")
            _currentObject = rayHit.collider.gameObject;
        else
            _currentObject = null;
    }

    private void UpdateIdle()
    {
        RayUpdate();

        if(_destination.Count > 0)
        {
            Status = Define.AreaStatus.Move;
        }
    }

    private void UpdateMove()
    {
        RayUpdate();

        if(_moveToDest == false)
        {
            StartCoroutine(MoveToTarget());
        }
    }

    private void UpdateBattle()
    {

    }
}
