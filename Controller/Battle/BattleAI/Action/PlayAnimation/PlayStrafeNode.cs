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
        _controller.PlayAnimation(Define.HeroState.Strafe);
        return BTResult.SUCCESS;
    }
}
