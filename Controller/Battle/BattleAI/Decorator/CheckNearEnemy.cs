using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckNearEnemy : DecoratorNode
{
    private BattleCharacterController _controller;

    public CheckNearEnemy(BattleCharacterController controller, NodeBase node) : base(node)
    {
        _controller = controller;
    }

    protected override bool Condition()
    {
        BattleCharacterController near = _controller.GetNearestCharacter();

        if (near == null) return false;

        return true;
    }
}
