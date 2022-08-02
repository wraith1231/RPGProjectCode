using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class BlockSequence : SequenceNode
{
    public BlockSequence(BattleCharacterController con)
    {
        Attach(new PlayBlockNode(con));
        Attach(new WaitUntilIdle(con));
    }

}
