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

        //patrol or move close
        PatrolSequence patrolSeq = new PatrolSequence(this, 3);
        MoveCloseTarget moveCloseTargetSeq = new MoveCloseTarget(this);
        CheckNearEnemy checkNearDeco = new CheckNearEnemy(this, moveCloseTargetSeq);
        SelectorNode encountSel = new SelectorNode();
        encountSel.Attach(checkNearDeco);
        encountSel.Attach(patrolSeq);

        //battle seq
        AttackSequence attackNode = new AttackSequence(this);
        float battleRandom = 0.1f;
        MoveAwayFromTargetNode awayNode = new MoveAwayFromTargetNode(this, battleRandom);

        SequenceNode moveCloseNode = new SequenceNode();
        moveCloseNode.Attach(new SetTempValueNode(this, 1.0f, 0.0f));
        moveCloseNode.Attach(new PlayStrafeNode(this));
        moveCloseNode.Attach(new WaitRandomTime(battleRandom));
        
        CheckNextStateSetValueNode attackDeco = new CheckNextStateSetValueNode(this, Define.HeroState.Attack, attackNode);
        CheckNextStateSetValueNode moveAwayDeco = new CheckNextStateSetValueNode(this, Define.HeroState.Strafe, awayNode);
        CheckNextStateSetValueNode moveCloseDeco = new CheckNextStateSetValueNode(this, Define.HeroState.Running, moveCloseNode);

        SelectorNode fightSelector = new SelectorNode();
        fightSelector.Attach(attackDeco);
        fightSelector.Attach(moveAwayDeco);
        fightSelector.Attach(moveCloseDeco);

        SetTempIntRandomNode stateNode = new SetTempIntRandomNode(this, (int)Define.HeroState.Unknown);
        SequenceNode fightSequance = new SequenceNode();
        fightSequance.Attach(stateNode);
        fightSequance.Attach(fightSelector);
        
        NodeBase deco2 = new CheckNearTargetRange(this, _attackRange, fightSequance);
        moveCloseTargetSeq.Attach(deco2);
        
        SequenceNode finalSequance = new SequenceNode(encountSel);

        _root = finalSequance;
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
