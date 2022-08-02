using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class PatrolSequence : SequenceNode
{
    private BattleCharacterController _controller;

    public PatrolSequence(BattleCharacterController controller, float waitRange) : base()
    {
        Attach(new WaitRandomTime(waitRange));
        //Attach(new SetTempValueRandomNode(controller));
        Attach(new RotateAroundBasePos(controller));
        Attach(new SetTempValueNode(controller, 1.0f, 0.0f));
        Attach(new SetHVValueNode(controller));
        Attach(new PlayStrafeNode(controller));
        Attach(new WaitRandomTime(waitRange));
        Attach(new PlayIdleNode(controller));
    }

    public override BTResult Evaluate()
    {
        base.Evaluate();

        return BTResult.SUCCESS;
    }
}
