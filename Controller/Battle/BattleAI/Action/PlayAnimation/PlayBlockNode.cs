using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class PlayBlockNode : NodeBase
{
    private BattleCharacterController _controller;
    public PlayBlockNode(BattleCharacterController con)
    {
        _controller = con;
    }

    public override BTResult Evaluate()
    {
        _controller.PlayAnimation(Define.HeroState.Block);

        return BTResult.SUCCESS;
    }
}
