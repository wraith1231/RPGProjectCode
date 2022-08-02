using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class RollSequence : SequenceNode
{
    public RollSequence(BattleCharacterController con)
    {
        Attach(new SetHVValueOppositeTargetNode(con));
        Attach(new PlayRollNode(con));
        Attach(new WaitUntilIdle(con));
    }

}
