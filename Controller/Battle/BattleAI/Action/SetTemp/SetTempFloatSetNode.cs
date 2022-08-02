using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SetTempFloatSetNode : NodeBase
{
    private BattleCharacterController _controller;
    private float _value;
    public SetTempFloatSetNode(BattleCharacterController con, float value)
    {
        _controller = con;
        _value = value;
    }

    public override BTResult Evaluate()
    {
        _controller.TempFloat = _value;

        return BTResult.SUCCESS;
    }
}
