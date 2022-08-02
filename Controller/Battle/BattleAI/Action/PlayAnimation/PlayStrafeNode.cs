using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class PlayStrafeNode : NodeBase
{
    private BattleCharacterController _controller;
    public PlayStrafeNode(BattleCharacterController controller)
    {
        _controller = controller;
    }
    public override BTResult Evaluate()
    {
        if (_controller.PlayAnimation(Define.HeroState.Strafe) == true) return BTResult.RUNNING;

        return BTResult.SUCCESS;
    }
}
