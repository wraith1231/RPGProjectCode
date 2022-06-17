using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class PlayRunNode : NodeBase
{
    private BattleCharacterController _controller;
    public PlayRunNode(BattleCharacterController controller)
    {
        _controller = controller;
    }
    public override BTResult Evaluate()
    {
        _controller.PlayAnimation(Define.HeroState.Running);
        return BTResult.SUCCESS;
    }
}