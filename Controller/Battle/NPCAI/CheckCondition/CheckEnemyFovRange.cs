using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckEnemyFovRange : HeroNode
{

    public CheckEnemyFovRange(EnemyHeroController controller) : base(controller)
    {

    }

    public override NodeState Evaluate()
    {
        if (_parentController.InBattleTarget == true)
        {
            _state = NodeState.Failed;
            return _state;
        }

        BattleHeroController controller = _parentController.CalculateNearestCharacter();

        if (controller != null)
        {
            _state = NodeState.Failed;
            return _state;
        }

        _state = NodeState.Success;
        return _state;
    }
}
