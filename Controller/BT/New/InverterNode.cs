using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class InverterNode : NodeBase
{
    protected NodeBase _child;

    public InverterNode(NodeBase node)
    {
        _child = node;
    }

    public override BTResult Evaluate()
    {
        BTResult result = _child.Evaluate();

        switch (result)
        {
            case BTResult.SUCCESS:
                return BTResult.FAILURE;
            case BTResult.RUNNING:
                return result;
            case BTResult.FAILURE:
                return BTResult.SUCCESS;
        }

        return BTResult.FAILURE;
    }
}
