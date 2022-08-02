using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaWaitSetDaytime : NodeBase
{
    private AreaGroupController _controller;
    private int _wait;

    private bool _waitStart;

    public AreaWaitSetDaytime(AreaGroupController con, int wait)
    {
        _controller = con;
        _wait = wait;
        _waitStart = false;
    }

    public override BTResult Evaluate()
    {
        if(_waitStart == false)
        {
            _waitStart = true;
        }


        if (_controller.IdleDayTime >= _wait)
        {
            _waitStart = false;
            return BTResult.SUCCESS;
        }


        return BTResult.RUNNING;
    }
}
