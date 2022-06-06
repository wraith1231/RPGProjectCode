using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class RotateHeadToTarget : NodeBase
{
    private BattleCharacterController _controller;
    private BattleCharacterController _target;

    public RotateHeadToTarget(BattleCharacterController controller, BattleCharacterController target)
    {
        _controller = controller;
        _target = target;
    }

    public override BTResult Evaluate()
    {
        _controller.HeadToDestination(_target.transform);

        return BTResult.SUCCESS;
    }
}
