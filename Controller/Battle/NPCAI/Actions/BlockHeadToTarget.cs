using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class BlockHeadToTarget : HeroNode
{
    private Transform _transform;

    public BlockHeadToTarget(EnemyHeroController controller) : base(controller)
    {
        _transform = controller.transform;
    }

    public override NodeState Evaluate()
    {
        if (_parentController.NextState != Define.HeroState.Block)
        {
            _state = NodeState.Success;
            return _state;
        }

        _transform.LookAt(_parentController.GetNearestCharacter().transform);
        bool isRunning = _parentController.PlayBlockAnimation();
        if (isRunning == true)
        {
            _state = NodeState.Running;
        }
        else
        {
            _state = NodeState.Failed;
            _parentController.InBattleTarget = false;
        }

        return _state;

    }
}
