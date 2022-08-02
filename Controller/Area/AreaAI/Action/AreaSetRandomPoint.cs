using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaRandomPoint : NodeBase
{
    private AreaGroupController _controller;
    private Vector2 _startPoint;
    private float _range;

    public AreaRandomPoint(AreaGroupController con, float range)
    {
        _controller = con;
        _range = range;

        _startPoint = con.GetPositionByVector2();
    }

    public override BTResult Evaluate()
    {
        Vector2 temp = new Vector2(_startPoint.x + Random.Range(-_range, _range),
            _startPoint.y + Random.Range(-_range, _range));

        _controller.TempPoint = temp;

        return BTResult.SUCCESS;
    }
}
