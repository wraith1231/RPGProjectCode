using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SetTempValueRandomNode : NodeBase
{
    private BattleCharacterController _controller;

    public SetTempValueRandomNode(BattleCharacterController con)
    {
        _controller = con;
    }

    public override BTResult Evaluate()
    {
        _controller.TempValue = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1f, 1f));

        return BTResult.SUCCESS;
    }
}
