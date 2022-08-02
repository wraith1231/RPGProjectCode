using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SetHVValueOppositeTargetNode : NodeBase
{
    private BattleCharacterController _controller;
    public SetHVValueOppositeTargetNode(BattleCharacterController con)
    {
        _controller = con;
    }
    public override BTResult Evaluate()
    {
        Vector2 value = new Vector2(Random.Range(-1f, 0f), Random.Range(-1f, 0f));

        _controller.SetAnimatorDirection(value);

        return BTResult.SUCCESS;
    }
}
