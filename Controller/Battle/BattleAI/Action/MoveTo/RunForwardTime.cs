using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class RunForwardTime : NodeBase
{
    private BattleCharacterController _controller;
    private float _runTime = 0.0f;
    private float _SetRunTime = 0.0f;

    public RunForwardTime(BattleCharacterController controller)
    {
        _controller = controller;
    }
    public override BTResult Evaluate()
    {
        _controller.SetAnimatorVertical(1.0f);
        if (_controller.PlayAnimation(Define.HeroState.Strafe) == false)
        {
            _SetRunTime = Random.Range(0f, 2f);
        }
        else
        {
            _runTime += Time.deltaTime;
            if (_runTime >= _SetRunTime)
            {
                _runTime = 0f;
                _controller.PlayAnimation(Define.HeroState.Idle);
                return BTResult.SUCCESS;
            }
        }

        return BTResult.RUNNING;
    }
}
