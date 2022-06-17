using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SetHorizontalNode : NodeBase
{
    private BattleCharacterController _controller;
    private float _value;

    public SetHorizontalNode(BattleCharacterController controller, float value)
    {
        _controller = controller;
        _value = value;
    }

    public override BTResult Evaluate()
    {
        _controller.SetAnimatorHorizontal(_value);
        return BTResult.SUCCESS;
    }
}
