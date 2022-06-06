using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class PatrolSequence : SequenceNode
{
    private BattleCharacterController _controller;

    public PatrolSequence(BattleCharacterController controller, float waitTime) : base()
    {
        Attach(new WaitUntilSetTime(controller, waitTime));
        Attach(new RotateAroundBasePos(controller));
        Attach(new WalkForwardTime(controller));
    }

    public override BTResult Evaluate()
    {
        base.Evaluate();

        return BTResult.SUCCESS;
    }
}
