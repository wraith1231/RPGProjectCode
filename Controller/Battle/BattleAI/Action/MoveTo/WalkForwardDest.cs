using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class WalkForwardDest : NodeBase
{
    private BattleCharacterController _controller;
    private Vector2 _dest;
    private float _walkDist;

    public WalkForwardDest(BattleCharacterController controller, Vector2 dest, float dist)
    {
        _controller = controller;
        _dest = dest;
        _walkDist = dist;
    }
    public WalkForwardDest(BattleCharacterController controller, Vector3 dest, float dist)
    {
        _controller = controller;
        _dest.x = dest.x;
        _dest.y = dest.z;
        _walkDist = dist;
    }

    public override BTResult Evaluate()
    {
        if (_controller.PlayAnimation(Define.HeroState.Strafe) == true)
        {
            _controller.SetAnimatorVertical(1.0f);
            if (_walkDist >= _controller.GetDistanceWithVector2(_dest))
                return BTResult.SUCCESS;

        }
        return BTResult.RUNNING;
    }
}
