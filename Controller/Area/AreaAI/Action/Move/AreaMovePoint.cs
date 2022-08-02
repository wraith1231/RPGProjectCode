using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaMovePoint : NodeBase
{
    private AreaGroupController _controller;

    public AreaMovePoint(AreaGroupController con)
    {
        _controller = con;
    }

    public override BTResult Evaluate()
    {
        _controller.MoveToDestination(_controller.TempPoint);

        return BTResult.SUCCESS;
    }
}
