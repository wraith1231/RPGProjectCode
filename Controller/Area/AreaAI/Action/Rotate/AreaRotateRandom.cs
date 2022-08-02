using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaRotateRandom : NodeBase
{
    private AreaGroupController _controller;

    public AreaRotateRandom(AreaGroupController con)
    {
        _controller = con;
    }

    public override BTResult Evaluate()
    {
        Vector2 target = _controller.GetPositionByVector2();
        target.x += Random.Range(-1f, 1f);
        target.y += Random.Range(-1f, 1f);

        _controller.HeadToDestination(target);

        return BTResult.SUCCESS;
    }
}
