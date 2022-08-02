using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class WaitRandomTime : NodeBase
{
    private float _setTime;
    private float _curTime;
    private float _range;

    public WaitRandomTime( float range)
    {
        _range = range;
        Initialize();
    }
    public override BTResult Evaluate()
    {
        _curTime += Time.deltaTime;
        if (_curTime >= _setTime)
        {
            Initialize();
            return BTResult.SUCCESS;
        }

        return BTResult.RUNNING;
    }

    private void Initialize()
    {
        _curTime = 0;
        _setTime = Random.Range(0, _range);
    }
}
