using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class WalkForwardTime : NodeBase
{
    private BattleCharacterController _controller;
    private float _walkTime = 0.0f;
    private float _SetWalkTime = 0.0f;

    public WalkForwardTime(BattleCharacterController controller)
    {
        _controller = controller;
    }
    public override BTResult Evaluate()
    {
        _controller.SetAnimatorVertical(1.0f);
        if(_controller.PlayAnimation(Define.HeroState.Strafe) == false)
        {
            _SetWalkTime = Random.Range(0f, 2f);
        }
        else
        {
            _walkTime += Time.deltaTime;
            if (_walkTime >= _SetWalkTime)
            {
                _walkTime = 0f;
                _controller.PlayAnimation(Define.HeroState.Idle);
                return BTResult.SUCCESS;
            }
        }

        return BTResult.RUNNING;
    }
}
