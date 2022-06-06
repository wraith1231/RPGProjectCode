using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class MoveCloseTarget : SequenceNode
{
    public MoveCloseTarget(BattleCharacterController controller, BattleCharacterController target)     
    {
        Attach(new RotateHeadToTarget(controller, target));
        Attach(new RunForwardTarget(controller, target, controller.RunRange));
        Attach(new WalkForwardTarget(controller, target, controller.AttackRange));
    }
}
