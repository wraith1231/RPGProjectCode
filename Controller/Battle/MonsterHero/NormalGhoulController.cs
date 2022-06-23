using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class NormalGhoulController : BattleMonsterController
{
    public override Define.HeroState State { get { return _state; } protected set { _state = value; } }


    public override void Init()
    {
        base.Init();
        _attackAnimCount = 1;

        NodeBase action = new PatrolSequence(this);
        SelectorNode selector = new SelectorNode(action);
        _root = selector;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (State == Define.HeroState.Strafe)
            _transform.position += _transform.forward * _animationSpeed[(int)Define.HeroState.Strafe] * Time.deltaTime;
        if(State == Define.HeroState.Running)
            _transform.position += _transform.forward * _animationSpeed[(int)Define.HeroState.Running] * 2 * Time.deltaTime;
    }
}
