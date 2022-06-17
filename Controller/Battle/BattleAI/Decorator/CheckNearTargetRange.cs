using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckNearTargetRange : DecoratorNode
{
    private BattleCharacterController _controller;
    private BattleCharacterController _target;
    private float _range;

    public CheckNearTargetRange(BattleCharacterController con, BattleCharacterController target, float range, NodeBase node) : base(node)
    {
        _controller = con;
        _target = target;
        _range = range;
    }

    protected override bool Condition()
    {
        float dist = _controller.GetDistanceTarget(_target.transform);
        if (dist < _range)
            return true;

        return false;

    }
}
