using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaWaitUntilIdle : NodeBase
{
    private AreaGroupController _controller;
    public AreaWaitUntilIdle(AreaGroupController con)
    {
        _controller = con;
    }

    public override BTResult Evaluate()
    {
        if (_controller.Status != Define.AreaStatus.Idle)
            return BTResult.RUNNING;

        return BTResult.SUCCESS;
    }
}
