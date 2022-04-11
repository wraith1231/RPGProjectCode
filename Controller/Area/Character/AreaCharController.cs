using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AreaCharController : MonoBehaviour
{
    private Transform _transform;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    
    private Vector3 _destination;
    private bool _moveToDest = false;

    protected CharacterData _data;
    public CharacterData Data { get { return _data; } }
    protected List<AreaCharController> _partners = new List<AreaCharController>();

    void Start()
    {
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _destination = _transform.position;
        _animator.applyRootMotion = false;
        
        Initialize();
    }

    protected abstract void Initialize();

    private void Update()
    {

    }

    internal void SetCharacterData(CharacterData data)
    {
        _data = data;
    }
}
