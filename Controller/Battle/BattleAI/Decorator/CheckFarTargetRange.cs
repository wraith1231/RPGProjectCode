using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckFarTargetRange : DecoratorNode
{
    private BattleCharacterController _controller;
    private float _range;

    public CheckFarTargetRange(BattleCharacterController con,  float range, NodeBase node) : base(node)
    {
        _controller = con;
        _range = range;
    }

    protected override bool Condition()
    {
        float dist = _controller.GetDistanceTarget(_controller.GetTarget().transform);

        Debug.Log($"dist : {dist}, range { _range}");
        if (dist < _range)
        {
            return false;
        }

        return true;

    }
}
