using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaWaitSetTime : NodeBase
{
    private float _waitTime;
    private float _setTime;
    private float _initTime;
    public AreaWaitSetTime(float time)
    {
        _initTime = time;
        Initialize();
    }

    public override BTResult Evaluate()
    {
        _waitTime += Time.deltaTime;
        if(_waitTime >= _setTime)
        {
            Initialize();
            return BTResult.SUCCESS;
        }

        return BTResult.RUNNING;
    }

    private void Initialize()
    {
        _waitTime = 0;
        _setTime = _initTime;
    }
}
