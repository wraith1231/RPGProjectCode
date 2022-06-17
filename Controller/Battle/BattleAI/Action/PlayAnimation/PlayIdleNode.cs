using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class PlayIdleNode : NodeBase
{
    private BattleCharacterController _controller;
    public PlayIdleNode(BattleCharacterController controller)
    {
        _controller = controller;
    }
    public override BTResult Evaluate()
    {
        _controller.PlayAnimation(Define.HeroState.Idle);
        return BTResult.SUCCESS;
    }
}
