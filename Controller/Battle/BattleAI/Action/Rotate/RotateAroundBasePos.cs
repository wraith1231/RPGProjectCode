using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class RotateAroundBasePos : NodeBase
{
    private BattleCharacterController _controller;
    private Vector2 _basePosition;

    public RotateAroundBasePos(BattleCharacterController controller)
    {
        _controller = controller;
        _basePosition = _controller.GetPositionByVector2();
    }

    public override BTResult Evaluate()
    {
        Vector2 temp = new Vector2();
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        temp.x = _basePosition.x + x;
        temp.y = _basePosition.y + z;

        _controller.HeadToDestination(temp);

        return BTResult.SUCCESS;
    }
}
