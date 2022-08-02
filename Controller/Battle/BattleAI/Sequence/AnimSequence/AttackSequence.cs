using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AttackSequence : SequenceNode
{
    public AttackSequence(BattleCharacterController controller)
    {
        Attach(new PlayAttackNode(controller));
        Attach(new WaitUntilIdle(controller));
    }

}
