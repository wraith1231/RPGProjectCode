using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

class CheckEnemyDistance : HeroNode
{
    private Transform _transform;
    
    private float _attackRange;
    
    public CheckEnemyDistance(EnemyHeroController controller) : base(controller)
    {
        _transform = _parentController.transform;
        _attackRange = _parentController.AttackRange;
    }

    public override NodeState Evaluate()
    {
        BattleHeroController target = _parentController.GetNearestCharacter();

        if (target == null)
        {
            _state = NodeState.Failed;
            return _state;
        }

        float dist = Vector3.Distance(_transform.position, target.transform.position);
        _parentController.TargetDistance = dist;

        if (_parentController.InBattleTarget == true)
        {
            _state = NodeState.Failed;
            return _state;
        }

        if (dist > _attackRange)
        {
            _state = NodeState.Success;
            return _state;
        }

        _state = NodeState.Failed;
        return _state;
    }
}