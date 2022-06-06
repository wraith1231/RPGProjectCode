using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class RunForwardDest : NodeBase
{
    private BattleCharacterController _controller;
    private Vector2 _dest;
    private float _runDist;

    public RunForwardDest(BattleCharacterController controller, Vector2 dest, float dist)
    {
        _controller = controller;
        _dest = dest;
        _runDist = dist;
    }
    public RunForwardDest(BattleCharacterController controller, Vector3 dest, float dist)
    {
        _controller = controller;
        _dest.x = dest.x;
        _dest.y = dest.z;
        _runDist = dist;
    }

    public override BTResult Evaluate()
    {
        if(_controller.PlayAnimation(Define.HeroState.Running) == true)
        {
            _controller.SetAnimatorVertical(1.0f);
            if (_runDist >= _controller.GetDistanceWithVector2(_dest))
                return BTResult.SUCCESS;

        }
        return BTResult.RUNNING;
    }
}
