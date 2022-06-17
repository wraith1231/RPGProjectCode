using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaWaitRandomTime : NodeBase
{
    private float _waitTime;
    private float _range;
    private float _setTime;

    public AreaWaitRandomTime(float range)
    {
        _range = range;
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
        _setTime = Random.Range(0, _range);
    }
}
