using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCameraController : MonoBehaviour
{
    private Transform _tranform;
    private Transform _target;
    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _tranform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != null)
        {
            _tranform.position = _target.position + _offset;
            _tranform.LookAt(_target);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
