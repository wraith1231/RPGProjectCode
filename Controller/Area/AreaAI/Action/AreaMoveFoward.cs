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
        dest += new Vector2(_controller.transform.forward.x, _controller.transform.forward.z);
        _controller.MoveToDestination(dest);

        return BTResult.SUCCESS;
    }
}
