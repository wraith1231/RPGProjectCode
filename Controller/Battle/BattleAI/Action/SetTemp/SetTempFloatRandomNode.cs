using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SetTempFloatRandomNode : NodeBase
{
    private BattleCharacterController _controller;
    private float _range;
    public SetTempFloatRandomNode(BattleCharacterController con, float value)
    {
        _controller = con;
        _range = value;
    }

    public override BTResult Evaluate()
    {
        _controller.TempFloat = Random.Range(0f, _range);

        return BTResult.SUCCESS;
    }
}
