using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaWaitRandomDaytime : NodeBase
{
    private AreaGroupController _controller;
    private int _range;
    private int _waitTime;

    private bool _waitStart;

    public AreaWaitRandomDaytime(AreaGroupController con, int range )
    {
        _controller = con;
        _range = range;
        _waitStart = false;
    }

    public override BTResult Evaluate()
    {
        if(_waitStart == false)
        {
            _waitStart = true;
            _waitTime = Random.Range(1, _range);
        }

        if(_controller.IdleDayTime >= _waitTime)
        {
            _waitStart = false;

            return BTResult.SUCCESS;
        }


        return BTResult.RUNNING;
    }
}
