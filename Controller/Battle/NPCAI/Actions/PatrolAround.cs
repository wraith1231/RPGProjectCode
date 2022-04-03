using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class PatrolAround : HeroNode
{
    private Transform _transform;
    private Animator _animator;

    private Vector3 _patrolDest;
    private Vector3 _originPosition;

    private float _fixedTime = 0.1f;
    private bool _animPlaying = false;

    private float _patrolDistance;

    private bool _waiting = false;
    private float _waitTime = 1f;
    private float _waitCounter = 0f;

    public PatrolAround(EnemyHeroController controller) : base(controller)
    {
        _transform = _parentController.transform;
        _animator = _parentController.HeroAnimator;
        _patrolDistance = _parentController.PatrolDist;
        _patrolDest = _transform.position;
        _originPosition = _transform.position;
        _fixedTime = _parentController.AnimFixedTime;
        _animator.SetFloat("speed", 1.0f);
    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            if (_animPlaying == false)
            {
                _animPlaying = true;
                _parentController.SetHeroState(Define.HeroState.Idle);
                _animator.CrossFade("Idle", _fixedTime);
            }

            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _animPlaying = false;
                _waiting = false;
            }
        }

        if (Vector3.Distance(_transform.position, _patrolDest) < 0.1f)
        {
            _transform.position = _patrolDest;
            _waitCounter = 0;
            _waiting = true;
            _waitTime = Random.Range(0.7f, 2.0f);
            _animPlaying = false;

            _patrolDest = _originPosition +
                new Vector3(Random.Range(-_patrolDistance, _patrolDistance), 0, Random.Range(-_patrolDistance, _patrolDistance));
        }
        else
        {
            _transform.LookAt(_patrolDest);
            if (_animPlaying == false)
            {
                _parentController.SetHeroState(Define.HeroState.Strafe);
                _animator.SetFloat("Vertical", 1.0f);
                _animPlaying = true;
                _animator.CrossFade("Strafe", _fixedTime);
            }
        }

        _state = NodeState.Success;
        return _state;
    }
}
