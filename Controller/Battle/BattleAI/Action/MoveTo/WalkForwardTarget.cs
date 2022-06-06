using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class WalkForwardTarget : NodeBase
{
    private BattleCharacterController _controller;
    private BattleCharacterController _target;
    private float _distance;

    public WalkForwardTarget(BattleCharacterController controller, BattleCharacterController target, float distance)
    {
        _controller = controller;
        _target = target;
        _distance = distance;
    }

    public override BTResult Evaluate()
    {
        _controller.SetAnimatorVertical(1.0f);
        if (_controller.PlayAnimation(Define.HeroState.Strafe) == true)
        {
            if (_controller.GetDistanceTarget(_target.transform) < _distance)
            {
                return BTResult.SUCCESS;
            }

        }

        return BTResult.FAILURE;
    }
}
