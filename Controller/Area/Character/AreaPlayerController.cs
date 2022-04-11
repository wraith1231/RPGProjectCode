using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPlayerController : AreaCharController
{
    private AreaCameraController _camera;
    public AreaCameraController Camera { get { return _camera; } set { _camera = value; } }

    protected override void Initialize()
    {
        Managers.Input.LeftMouseAction -= OnLeftMouseEvent;
        Managers.Input.LeftMouseAction += OnLeftMouseEvent;
    }

    public void OnLeftMouseEvent(Define.MouseEvent mouseEvent)
    {
        switch (mouseEvent)
        {
            case Define.MouseEvent.PointerDown:


                break;
        }
    }
}
