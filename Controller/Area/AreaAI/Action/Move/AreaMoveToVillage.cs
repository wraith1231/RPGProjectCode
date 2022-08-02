using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaMoveToVillage : NodeBase
{
    private AreaGroupController _controller;

    public AreaMoveToVillage(AreaGroupController con)
    {
        _controller = con;
    }

    public override BTResult Evaluate()
    {
        _controller.FindPathToVillage(_controller.TargetVillage);

        return BTResult.SUCCESS;
    }
}
