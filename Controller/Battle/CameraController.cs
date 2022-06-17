using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerHeroController _player;

    [SerializeField]
    private float _offsetY = 1.7f;
    [SerializeField]
    private float _distance = 1.5f;
    [SerializeField]
    private float _mouseSpeed = 5f;

    [SerializeField]
    private float _mouseYUpRange = 5f;
    [SerializeField]
    private float _mouseYDownRange = -3f;
    private float _prevMouseY;

    private Vector3 _currentPos;

    private bool _lockOn = false;
    //private Transform _target;

    private Vector3 _lookAt;

    private Transform _transform;

    public float MouseSpeed { get { return _mouseSpeed; } }

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        _prevMouseY = 0;
        _transform = GetComponent<Transform>();
    }

    public void BattleSceneInit()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            if (_player.State != Define.HeroState.Rolling)
            {
                _currentPos = new Vector3(_player.transform.forward.x * _distance, -_offsetY, _player.transform.forward.z * _distance);

                float mouseY = Input.GetAxis("Mouse Y") * _mouseSpeed * 0.2f * Time.deltaTime;
                _prevMouseY += mouseY;
                if (_prevMouseY > _mouseYUpRange) _prevMouseY = _mouseYUpRange;
                else if (_prevMouseY < _mouseYDownRange) _prevMouseY = _mouseYDownRange;
                _lookAt = _player.transform.forward * 5f;
                _lookAt.y += _prevMouseY;

                _currentPos.y += _prevMouseY * 0.2f;
                
            }

            _transform.position = _player.transform.position - _currentPos;

            if (_lockOn == false)
                _transform.LookAt(_player.transform.position + _lookAt);
            //else
            //    transform.LookAt(_target);
        }
    }

    public void SetPlayer(PlayerHeroController player)
    {
        _player = player;
    }
}
