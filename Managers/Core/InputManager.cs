using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager 
{
    public Action KeyAction = null;

    //Left Mouse
    public Action<Define.MouseEvent> LeftMouseAction = null;
    float _leftPressedTime = 0.0f;
    bool _leftPressed = false;

    //Right Mouse
    public Action<Define.MouseEvent> RightMouseAction = null;
    float _rightPressedTime = 0.0f;
    bool _rightPressed = false;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true)
            return;

        if (KeyAction != null && Input.anyKey != false)
            KeyAction.Invoke();

        if (LeftMouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                _leftPressedTime += Time.deltaTime;
                if (_leftPressed == false)
                {
                    LeftMouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _leftPressedTime = 0.0f;
                }
                LeftMouseAction.Invoke(Define.MouseEvent.Press);
                _leftPressed = true;
            }
            else
            {
                if (_leftPressed)
                {
                    if (_leftPressedTime < 0.1f)
                        LeftMouseAction.Invoke(Define.MouseEvent.Click);

                    LeftMouseAction.Invoke(Define.MouseEvent.PointerUp);
                }

                _leftPressed = false;
                _leftPressedTime = 0f;
            }
        }
        if (RightMouseAction != null)
        {
            if (Input.GetMouseButton(1))
            {
                _rightPressedTime += Time.deltaTime;
                if (_rightPressed == false)
                {
                    RightMouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _rightPressedTime = 0.0f;
                }
                RightMouseAction.Invoke(Define.MouseEvent.Press);
                _rightPressed = true;
            }
            else
            {
                if (_rightPressed)
                {
                    if (_rightPressedTime < 0.1f)
                        RightMouseAction.Invoke(Define.MouseEvent.Click);

                    RightMouseAction.Invoke(Define.MouseEvent.PointerUp);
                }

                _rightPressed = false;
                _rightPressedTime = 0f;
            }
        }
    }

    public void Clear()
    {
        KeyAction = null;
        LeftMouseAction = null;
        RightMouseAction = null;
    }
}
