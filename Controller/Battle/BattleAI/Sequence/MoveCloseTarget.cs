using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class MoveCloseTarget : SequenceNode
{
    public MoveCloseTarget(BattleCharacterController controller)     
    {
        Attach(new RotateHeadToTarget(controller));
        Attach(new SetTempValueNode(controller, 1.0f, 0.0f));
        Attach(new SetHVValueNode(controller));
        PlayRunNode run = new PlayRunNode(controller);
        PlayStrafeNode strafe = new PlayStrafeNode(controller);
        SelectorNode selector = new SelectorNode();
        selector.Attach(new CheckFarTargetRange(controller, controller.RunRange, run));
        selector.Attach( strafe);
        Attach(selector);
    }

    public override BTResult Evaluate()
    {
        return base.Evaluate();
    }
}
