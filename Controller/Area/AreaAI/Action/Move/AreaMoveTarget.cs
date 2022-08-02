using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaMoveTarget : NodeBase
{
    private AreaGroupController _controller;
    public AreaMoveTarget(AreaGroupController con)
    {
        _controller = con;
    }

    public override BTResult Evaluate()
    {
        _controller.MoveToTarget(_controller.GetEnemy().transform);

        return BTResult.SUCCESS;

    }
}
