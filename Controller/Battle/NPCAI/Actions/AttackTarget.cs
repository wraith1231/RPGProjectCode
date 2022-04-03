using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AttackTarget : HeroNode
{
    private Transform _transform;

    public AttackTarget(EnemyHeroController controller) : base(controller)
    {
        _transform = controller.transform;
    }

    public override NodeState Evaluate()
    {
        if (_parentController.NextState != Define.HeroState.Attack)
        {
            _state = NodeState.Success;
            return _state;
        }

        bool isRunning = _parentController.PlayAttackAnimation();
        if (isRunning == true)
        {
            _state = NodeState.Running;
        }
        else
        {
            _transform.LookAt(_parentController.GetNearestCharacter().transform);
            _state = NodeState.Failed;
            _parentController.InBattleTarget = false;
        }

        return _state;
    }
}
