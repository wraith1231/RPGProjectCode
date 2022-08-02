using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class WaitUntilIdle : NodeBase
{
    private BattleCharacterController _controller;
    public WaitUntilIdle(BattleCharacterController con)
    {
        _controller = con;
    }
    public override BTResult Evaluate()
    {
        if (_controller.State != Define.HeroState.Idle)
            return BTResult.RUNNING;

        return BTResult.SUCCESS;
    }
}
