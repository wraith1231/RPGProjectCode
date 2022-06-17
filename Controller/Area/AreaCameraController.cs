using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCameraController : MonoBehaviour
{
    private Transform _tranform;
    private Transform _target;
    private Camera _camera;

    private static float _vertical = 90;
    private static float _horizon = 50;
    private Vector3 _mapSize;

    [SerializeField]
    private Vector3 _offset = new Vector3(0, 100, 0);
    [SerializeField]
    private float _orthoSize = 50f;

    // Start is called before the first frame update
    void Start()
    {
        _tranform = GetComponent<Transform>();
        _camera = GetComponent<Camera>();
        _camera.orthographicSize = _orthoSize;
        _mapSize = Managers.Map.TerrainSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != null)
        {
            float x = _target.position.x + _offset.x;
            float z = _target.position.z + _offset.z;
            if (x < _vertical) x = _vertical;
            else if (x > _mapSize.x - _vertical) x = _mapSize.x - _vertical;

            if (z < _horizon) z = _horizon;
            else if (z > _mapSize.z - _horizon) z = _mapSize.z - _horizon;

            Vector3 pos = new Vector3(x, _target.position.y + _offset.y, z);
            _tranform.position = pos;
            pos.y = 0;
            _tranform.LookAt(pos);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
