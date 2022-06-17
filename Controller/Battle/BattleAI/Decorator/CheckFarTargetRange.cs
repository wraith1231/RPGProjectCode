using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckFarTargetRange : DecoratorNode
{
    private BattleCharacterController _controller;
    private BattleCharacterController _target;
    private float _range;

    public CheckFarTargetRange(BattleCharacterController con, BattleCharacterController target, float range, NodeBase node) : base(node)
    {
        _controller = con;
        _target = target;
        _range = range;
    }

    protected override bool Condition()
    {
        float dist = _controller.GetDistanceTarget(_target.transform);
        if (dist < _range)
            return false;

        return true;

    }
}
