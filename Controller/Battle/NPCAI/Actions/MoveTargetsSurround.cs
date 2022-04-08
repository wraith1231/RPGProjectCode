using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class MoveTargetsSurround : HeroNode
{
    private Transform _transform;
    private Animator _animator;
    private float _fixedTime;
    private Vector3 _destPosition;
    private bool _isMoving = false;
    private float _moveTime = 0f;

    public MoveTargetsSurround(EnemyHeroController controller) : base(controller)
    {
        _transform = controller.transform;
        _animator = _parentController.HeroAnimator;
        _fixedTime = _parentController.AnimFixedTime;
        _destPosition = _transform.position;
    }

    public override NodeState Evaluate()
    {
        if (_parentController.NextState != Define.HeroState.Strafe)
        {
            _state = NodeState.Success;
            return _state;
        }

        _transform.LookAt(_parentController.GetNearestCharacter().transform);
        if(_isMoving == false)
        {
            _isMoving = true;
            _destPosition = new Vector3(_transform.position.x + Random.Range(-0.5f, 0.5f), 0, _transform.position.z + Random.Range(-0.5f, 0.5f));
            _parentController.InBattleTarget = true;
            _parentController.SetHeroState(Define.HeroState.Strafe);
            _animator.SetFloat("Horizontal", 0.0f);
            _animator.SetFloat("Vertical", 0.0f);
            _animator.CrossFade("Strafe", _fixedTime);
            _state = NodeState.Running;
            _moveTime = 0f;
        }
        else
        {
            _moveTime += Time.deltaTime;
            float dist = Vector3.Distance(_destPosition, _transform.position);

            if (dist > 0.5f || _parentController.TargetDistance < _parentController.AttackRange || _moveTime <= 1f)
            {
                ContinueMove();
            }
            else
            {
                EndMove();
            }

        }

        return _state;
    }

    private void ContinueMove()
    {
        Vector3 move = (_destPosition - _transform.position).normalized;
        float camDot = Vector3.Dot(_transform.forward, move);
        Vector3 upVec = Vector3.Cross(move, _transform.forward);
        float upDot = Vector3.Dot(upVec, _transform.up);

        _animator.SetFloat("Vertical", camDot);
        _animator.SetFloat("Horizontal", -upDot);
        _parentController.InBattleTarget = false;
        _state = NodeState.Running;
    }

    private void EndMove()
    {
        _parentController.InBattleTarget = false;
        _parentController.SetHeroState(Define.HeroState.Idle);
        _animator.SetFloat("Horizontal", 0.0f);
        _animator.SetFloat("Vertical", 0.0f);
        _animator.CrossFade("Idle", _fixedTime);
        _isMoving = false;
        _state = NodeState.Failed;
        _moveTime = 0f;
    }
}
