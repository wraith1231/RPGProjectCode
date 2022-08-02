using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class PlayRollNode : NodeBase
{
    private BattleCharacterController _controller;
    public PlayRollNode(BattleCharacterController con)
    {
        _controller = con;
    }

    public override BTResult Evaluate()
    {
        _controller.PlayAnimation(Define.HeroState.Rolling);

        return BTResult.SUCCESS;
    }
}
