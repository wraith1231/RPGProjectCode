using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckNextStateSetValueNode : DecoratorNode
{
    private BattleCharacterController _controller;
    private Define.HeroState _state;

    public CheckNextStateSetValueNode(BattleCharacterController con, Define.HeroState state, NodeBase child) : base(child)
    {
        _controller = con;
        _state = state;
    }

    protected override bool Condition()
    {
        if (_state == (Define.HeroState)_controller.TempInt)
            return true;

        return false;
    }
}
