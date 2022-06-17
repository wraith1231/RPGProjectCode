using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SetVerticalNode : NodeBase
{
    private BattleCharacterController _controller;
    private float _value;

    public SetVerticalNode(BattleCharacterController controller, float value)
    {
        _controller = controller;
        _value = value;
    }

    public override BTResult Evaluate()
    {
        _controller.SetAnimatorVertical(_value);
        return BTResult.SUCCESS;
    }
}
