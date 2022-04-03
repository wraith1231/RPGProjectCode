using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class HeadToTarget : HeroNode
{
    private Transform _transform;
    private Animator _animator;
    
    private float _attackRange;
    private float _runRange;

    private float _fixedTime;

    public HeadToTarget(EnemyHeroController controller) : base(controller)
    {
        _transform = _parentController.transform;
        _attackRange = _parentController.AttackRange;
        _runRange = _parentController.RunRange;
        _animator = _parentController.HeroAnimator;
        _fixedTime = _parentController.AnimFixedTime;
    }

    public override NodeState Evaluate()
    {
        BattleHeroController target = _parentController.CalculateNearestCharacter();

        float dist = _parentController.TargetDistance;
        if (dist > _attackRange)
        {
            _transform.LookAt(target.transform);

            if (dist > _runRange)
            {
                if (_parentController.State != Define.HeroState.Running)
                {
                    _animator.SetFloat("Vertical", 1.0f);
                    _parentController.SetHeroState(Define.HeroState.Running);
                    _animator.CrossFade("Run", _fixedTime);
                }
            }
            else
            {
                if(_parentController.State != Define.HeroState.Strafe)
                {
                    _animator.SetFloat("Vertical", 1.0f);
                    _parentController.SetHeroState(Define.HeroState.Strafe);
                    _animator.CrossFade("Strafe", _fixedTime);
                }
            }
            _state = NodeState.Success;
            return _state;
        }

        _state = NodeState.Success;
        return _state;
    }

}
