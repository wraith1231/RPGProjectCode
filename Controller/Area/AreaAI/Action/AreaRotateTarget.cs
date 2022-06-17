using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaRotateTarget : NodeBase
{
    private AreaGroupController _controller;
    private Transform _target;

    public AreaRotateTarget(AreaGroupController con, Transform tar)
    {
        _controller = con;
        _target = tar;
    }

    public override BTResult Evaluate()
    {
        _controller.HeadToDestination(_target);

        return BTResult.SUCCESS;
    }
}
