using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class RotateHeadToTarget : NodeBase
{
    private BattleCharacterController _controller;

    public RotateHeadToTarget(BattleCharacterController controller)
    {
        _controller = controller;
    }

    public override BTResult Evaluate()
    {
        _controller.HeadToDestination(_controller.GetTarget().transform);

        return BTResult.SUCCESS;
    }
}
