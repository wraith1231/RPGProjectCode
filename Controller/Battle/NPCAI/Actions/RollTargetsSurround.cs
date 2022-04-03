using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class RollTargetsSurround : HeroNode
{
    private Transform _transform;
    private bool _isRolling = false;
    private Vector3 _destPosition;
    
    public RollTargetsSurround(EnemyHeroController controller) : base(controller)
    {
        _transform = controller.transform;
    }

    public override NodeState Evaluate()
    {
        if (_parentController.NextState != Define.HeroState.Rolling)
        {
            _state = NodeState.Success;
            return _state;
        }

        if(_isRolling == false)
        {
            _isRolling = true;

            _parentController.RollingDirection = new Vector3(_transform.position.x + Random.Range(-1f, 1f), 0, _transform.position.z + Random.Range(-1f, 1f));
            _parentController.InBattleTarget = true;
        }

        bool isRunning = _parentController.PlayRollAnimation();
        if (isRunning == true)
        {
            _state = NodeState.Running;
        }
        else
        {
            _transform.LookAt(_parentController.GetNearestCharacter().transform);
            _state = NodeState.Failed;
            _parentController.InBattleTarget = false;
        }

        return _state;
    }
}
