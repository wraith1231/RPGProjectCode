using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class RotateToTempValueNode : NodeBase
{
    private BattleCharacterController _controller;
    public RotateToTempValueNode(BattleCharacterController con)
    {
        _controller = con;
    }
    public override BTResult Evaluate()
    {
        _controller.HeadToDestination(_controller.TempValue);

        return BTResult.SUCCESS;
    }
}
