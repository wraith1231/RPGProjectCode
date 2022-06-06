using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AttackFront : NodeBase
{
    private BattleCharacterController _controller;

    public AttackFront(BattleCharacterController controller)
    {
        _controller = controller;
    }

    public override BTResult Evaluate()
    {
        if (_controller.PlayAnimation(Define.HeroState.Attack) == true)
            return BTResult.RUNNING;

        return BTResult.SUCCESS;
    }
}
