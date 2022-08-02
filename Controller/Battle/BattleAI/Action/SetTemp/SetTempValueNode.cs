using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SetTempValueNode : NodeBase
{
    private BattleCharacterController _controller;
    private Vector2 _tempValue;
    public SetTempValueNode(BattleCharacterController con, float x, float y)
    {
        _controller = con;

        _tempValue = new Vector2(x, y);
    }

    public override BTResult Evaluate()
    {
        _controller.TempValue = _tempValue;

        return BTResult.SUCCESS;
    }
}
