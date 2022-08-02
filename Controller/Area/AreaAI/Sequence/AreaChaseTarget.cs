using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaChaseTarget : SequenceNode
{
    public AreaChaseTarget(AreaGroupController con)
    {
        Attach(new AreaMoveTarget(con));
        Attach(new AreaWaitUntilIdle(con));

    }

}
