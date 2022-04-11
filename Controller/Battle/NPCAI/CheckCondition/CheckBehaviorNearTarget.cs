using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckBehaviorNearTarget : HeroNode
{

    public CheckBehaviorNearTarget(EnemyHeroController controller) : base(controller)
    {

    }

    public override NodeState Evaluate()
    {
        if (_parentController.GetNearestCharacter() == null)
        {
            _state = NodeState.Failed;
            return _state;
        }
        if(_parentController.InBattleTarget == true)
        {
            _state = NodeState.Success;
            return _state;
        }
        if (_parentController.IsEnoughToBehavior(BattleHeroController.RollingStamina) == false)
        {
            _state = NodeState.Success;
            if (_parentController.State != Define.HeroState.Damaged)
            {
                _parentController.InBattleTarget = true;
                _parentController.NextState = Define.HeroState.Strafe;
            }
            return _state;
        }

        _parentController.InBattleTarget = true;
        _parentController.CheckNextState();

        _state = NodeState.Success;
        return _state;
        
    }
}
