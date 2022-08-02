using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SetTempIntSetNode : NodeBase
{
    private BattleCharacterController _controller;
    private int _value;
    public SetTempIntSetNode(BattleCharacterController con, int value)
    {
        _controller = con;
        _value = value;
    }

    public override BTResult Evaluate()
    {
        _controller.TempInt = _value;

        return BTResult.SUCCESS;
    }
}
