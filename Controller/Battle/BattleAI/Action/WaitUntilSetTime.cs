using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class WaitUntilSetTime : NodeBase
{
    private float _setTime;
    private float _curTime;

    public WaitUntilSetTime( float time)
    {
        _setTime = time;
        _curTime = 0;
    }
    public override BTResult Evaluate()
    {
        _curTime += Time.deltaTime;
        if(_curTime >= _setTime)
        {
            _curTime = 0;
            return BTResult.SUCCESS;
        }

        return BTResult.RUNNING;
    }
}
