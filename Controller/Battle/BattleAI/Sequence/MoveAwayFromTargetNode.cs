using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class MoveAwayFromTargetNode : SequenceNode
{
    public MoveAwayFromTargetNode(BattleCharacterController con, float range)
    {
        Attach(new SetHVValueOppositeTargetNode(con));
        Attach(new PlayStrafeNode(con));
        Attach(new WaitRandomTime(range));
        //Attach(new WaitUntilIdle(con));
    }
}
