using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaChangeTempValueRandom : NodeBase
{
    private AreaGroupController _controller;
    private float _min;
    private float _max;

    public AreaChangeTempValueRandom(AreaGroupController con, float max, float min)
    {
        _controller = con;
        _min = min;
        _max = max;
    }
    public override BTResult Evaluate()
    {
        _controller.TempValue = Random.Range(_min, _max);

        return BTResult.SUCCESS;
    }
}
