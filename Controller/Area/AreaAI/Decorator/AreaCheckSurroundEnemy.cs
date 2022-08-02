using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaCheckSurroundEnemy : DecoratorNode
{
    private AreaGroupController _controller;


    public AreaCheckSurroundEnemy(AreaGroupController con, NodeBase node) : base(node)
    {
        _controller = con;
    }

    protected override bool Condition()
    {
        return _controller.CheckNearEnemy();
    }
}
