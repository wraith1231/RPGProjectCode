using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SetHVValueNode : NodeBase
{
    private BattleCharacterController _controller;
    public SetHVValueNode(BattleCharacterController con)
    {
        _controller = con;
    }

    public override BTResult Evaluate()
    {
        _controller.SetAnimatorDirection(_controller.TempValue.x, _controller.TempValue.y);

        return BTResult.SUCCESS;
    }
}
