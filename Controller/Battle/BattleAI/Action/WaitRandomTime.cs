using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class WaitRandomTime : NodeBase
{
    private BattleCharacterController _controller;
    private float _setTime;
    private float _curTime;
    private float _range;

    public WaitRandomTime(BattleCharacterController controller, float range)
    {
        _controller = controller;
        _range = range;
        _setTime = Random.Range(0, _range);
        _curTime = 0;
    }
    public override BTResult Evaluate()
    {
        _curTime += Time.deltaTime;
        if (_curTime >= _setTime)
        {
            _curTime = 0;
            _setTime = Random.Range(0, _range);
            return BTResult.SUCCESS;
        }

        return BTResult.RUNNING;
    }
}
