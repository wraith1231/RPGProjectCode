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

    private Vector3 _currentPos;

    private bool _lockOn = false;
    //private Transform _target;

    private Vector3 _lookAt;

    public float MouseSpeed { get { return _mouseSpeed; } }

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        
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
                _lookAt = _player.transform.forward * 5f;
            }

            transform.position = _player.transform.position - _currentPos;

            if (_lockOn == false)
                transform.LookAt(_player.transform.position + _lookAt);
            //else
            //    transform.LookAt(_target);
        }
    }

    public void SetPlayer(PlayerHeroController player)
    {
        _player = player;
    }
}
