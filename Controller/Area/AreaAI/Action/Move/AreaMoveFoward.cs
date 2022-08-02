using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaMoveFoward : NodeBase
{
    private AreaGroupController _controller;
    

    public AreaMoveFoward(AreaGroupController con)
    {
        _controller = con;

    }

    public override BTResult Evaluate()
    {
        Vector2 dest = _controller.GetPositionByVector2();
        dest += _controller.GetForwardVector2() * _controller.TempValue;
        _controller.MoveToDestination(dest);

        return BTResult.SUCCESS;
    }
}
