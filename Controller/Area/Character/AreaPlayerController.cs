using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPlayerController : AreaGroupController
{
    private static int ObjectLayer = 1 << 8 | 1 << 9;

    private AreaCameraController _camera;
    public AreaCameraController AreaCamera { get { return _camera; } set { _camera = value; } }

    private delegate void CurrentUpdate();
    private CurrentUpdate _currentUpdate;

    //terrain과 village만 일단
    private int _mouseMask = 1 << 6 | 1 << 7 | 1 << 9;
    private GameObject _currentObject = null;
    private Vector3 _currentPoint;

    private UIGroupName _groupPopup;
    public UIGroupName GroupPopup { set { _groupPopup = value; } }

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
    private void OnDestroy()
    {
        Managers.Map.DayChangeUpdate -= DayChangeUpdate;

        Managers.Input.LeftMouseAction -= OnLeftMouseEvent;
        Managers.Input.RightMouseAction -= OnRightMouseEvent;

        Vector3 boxSize = new Vector3(128, 128, 128);
        Vector3 center = _transform.position;
        if (center.x <= 64) center.x = 64;
        if (center.x >= Managers.Map.TerrainSize2.x - 64) center.x = Managers.Map.TerrainSize2.x - 64;
        if (center.z <= 64) center.z = 64;
        if (center.z >= Managers.Map.TerrainSize2.z - 64) center.z = Managers.Map.TerrainSize2.z - 64;

        //Physics.CheckBox(center, boxSize, Quaternion.identity, ObjectLayer);
        Collider[] colliders = Physics.OverlapBox(center, boxSize, Quaternion.identity, ObjectLayer);
        int cSize = colliders.Length;
        for (int i = 0; i < cSize; i++)
            Managers.Battle.AddAreaObject(colliders[i].name, colliders[i].transform.position);

        GlobalGroupController controller = Managers.General.GlobalGroups[_groupId];

        controller.Position = _transform.position;

        controller.Status = _status;

        if (_destination.Count > 0)
        {
            int size = _destination.Count;
            for (int i = 0; i < size; i++)
                controller.Destination.Enqueue(_destination.Dequeue());
            //controller.Destination = _destination;
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
                if (_currentObject != null)
                {
                    if (_currentObject.tag == "MoveBlock")
                        break;
                }
                if(Input.GetKey(KeyCode.LeftShift) == true)
                {
                    if (_targetObject == null)
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
                }
                else
                {
                    _destination.Clear();
                    _targetObject = null;
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
                        _targetObject = _currentObject.transform;
                        //_destination.Enqueue(_currentObject.transform.position);
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
            if(_groupPopup != null)
                _groupPopup.TheresNoGroup();
            return;
        }
        else if(rayHit.collider.tag == "Terrain")
        {
            _currentObject = null;
            if (_groupPopup != null)
                _groupPopup.TheresNoGroup();
            return;
        }
        else
        {
            _currentObject = rayHit.collider.gameObject;
            string tag = rayHit.collider.tag;
            if (_groupPopup != null)
            {
                if (tag == "AreaCharacter" || tag == "Monster")
                {
                    AreaGroupController controller = rayHit.transform.GetComponent<AreaGroupController>();
                    _groupPopup.ChangeName(controller.GroupId, _currentPoint);
                }
                if (tag == "Village")
                    _groupPopup.ChangeName(rayHit.transform.GetComponent<VillageStatus>().Name, _currentPoint);
            }
        }
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
