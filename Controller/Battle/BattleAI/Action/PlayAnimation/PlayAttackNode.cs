using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class PlayAttackNode : NodeBase
{
    private BattleCharacterController _controller;
    public PlayAttackNode(BattleCharacterController controller)
    {
        _controller = controller;
    }
    public override BTResult Evaluate()
    {
        _controller.PlayAnimation(Define.HeroState.Attack);
        return BTResult.SUCCESS;
    }
}