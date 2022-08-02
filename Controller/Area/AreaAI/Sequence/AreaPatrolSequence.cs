using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaPatrolSequence : SequenceNode
{
    private AreaGroupController _controller;
    public AreaPatrolSequence(AreaGroupController con, float moveRange, int waitRange)
    {
        _controller = con;

        Attach(new AreaRandomPoint(_controller, moveRange));
        Attach(new AreaMovePoint(_controller));
        Attach(new AreaWaitUntilIdle(_controller));
        Attach(new AreaWaitRandomDaytime(_controller, waitRange));
    }

    public override BTResult Evaluate()
    {
        base.Evaluate();

        return BTResult.SUCCESS;
    }
}
