using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckIsTargetAlive : DecoratorNode
{
    private BattleCharacterController _controller;
    public CheckIsTargetAlive(BattleCharacterController con, NodeBase child) : base(child)
    {
        _controller = con;
    }

    protected override bool Condition()
    {
        if (_controller.GetTarget() == null)
            return false;

        return true;
    }
}
