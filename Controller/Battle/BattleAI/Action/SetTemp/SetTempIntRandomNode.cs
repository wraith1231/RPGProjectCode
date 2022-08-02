using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SetTempIntRandomNode : NodeBase
{
    private BattleCharacterController _controller;
    private int _range;
    public SetTempIntRandomNode(BattleCharacterController con, int value)
    {
        _controller = con;
        _range = value;
    }

    public override BTResult Evaluate()
    {
        _controller.TempInt = Random.Range(0, _range);

        return BTResult.SUCCESS;
    }
}
