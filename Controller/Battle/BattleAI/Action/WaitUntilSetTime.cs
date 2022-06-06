using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class WaitUntilSetTime : NodeBase
{
    private BattleCharacterController _controller;
    private float _setTime;

    public WaitUntilSetTime(BattleCharacterController controller, float time)
    {
        _controller = controller;
        _setTime = time;
    }
    public override BTResult Evaluate()
    {
        if(_controller.IdleTime >= _setTime)
        {
            return BTResult.SUCCESS;
        }


        return BTResult.FAILURE;
    }
}
