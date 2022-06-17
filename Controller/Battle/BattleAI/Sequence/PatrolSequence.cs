using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class PatrolSequence : SequenceNode
{
    private BattleCharacterController _controller;

    public PatrolSequence(BattleCharacterController controller) : base()
    {
        Attach(new WaitRandomTime(controller, 2f));
        Attach(new RotateAroundBasePos(controller));
        Attach(new SetVerticalNode(controller, 1.0f));
        Attach(new PlayStrafeNode(controller));
        Attach(new WaitRandomTime(controller, 2f));
        Attach(new PlayIdleNode(controller));
    }

    public override BTResult Evaluate()
    {
        base.Evaluate();

        return BTResult.SUCCESS;
    }
}
