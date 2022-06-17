using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class MoveCloseTarget : SequenceNode
{
    public MoveCloseTarget(BattleCharacterController controller, BattleCharacterController target)     
    {
        Attach(new RotateHeadToTarget(controller, target));
        Attach(new SetVerticalNode(controller, 1.0f));
        PlayRunNode run = new PlayRunNode(controller);
        CheckFarTargetRange deco = new CheckFarTargetRange(controller, target, controller.RunRange, run);
        Attach(deco);
        Attach(new PlayStrafeNode(controller));
        //Attach(new RunForwardTarget(controller, target, controller.RunRange));
        //Attach(new WalkForwardTarget(controller, target, controller.AttackRange));
    }
}
