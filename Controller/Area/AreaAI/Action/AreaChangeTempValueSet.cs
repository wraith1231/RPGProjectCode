using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaChangeTempValueSet : NodeBase
{
    private AreaGroupController _controller;
    private float _setValue;

    public AreaChangeTempValueSet(AreaGroupController con, float value)
    {
        _controller = con;
        _setValue = value;
    }
    public override BTResult Evaluate()
    {
        _controller.TempValue = _setValue;

        return BTResult.SUCCESS;
    }
}
