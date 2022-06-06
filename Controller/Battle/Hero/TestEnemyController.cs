using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TestEnemyController : EnemyHeroController
{

    public override void Init()
    {
        base.Init();

        SetupTree();
    }

    private void SetupTree()
    {
        /*
         * s1 - ���� ��ó�� �ִ°�? -> success �߸� ��������, fail �߸� �ֺ� ����
         * s2 - ���� �Ÿ� �ȿ� �ִ°�? -> success �߸� ��������, fail�̸� ������ �ٰ���
         * s3 - ���� ���� �ൿ ����
         */
        NodeBase action = new PatrolSequence(this, 3f);
        SelectorNode selector = new SelectorNode(action);
        _root = selector;
        PlayAnimation(Define.HeroState.Idle);
        //List<HeroNode> actions1 = new List<HeroNode>();
        //actions1.Add(new CheckEnemyFovRange(this));
        //actions1.Add(new PatrolAround(this));
        //
        //List<HeroNode> actions2 = new List<HeroNode>();
        //actions2.Add(new CheckEnemyDistance(this));
        //actions2.Add(new HeadToTarget(this));
        //
        //List<HeroNode> actions3 = new List<HeroNode>();
        //actions3.Add(new CheckBehaviorNearTarget(this));
        //actions3.Add(new AttackTarget(this));
        //actions3.Add(new BlockHeadToTarget(this));
        //actions3.Add(new MoveTargetsSurround(this));
        //actions3.Add(new RollTargetsSurround(this));
        //
        //List<HeroNode> sequences = new List<HeroNode>();
        //sequences.Add( new Sequence(this, actions1));
        //sequences.Add(new Sequence(this, actions2));
        //sequences.Add(new Sequence(this, actions3));
        //
        //Selector selector = new Selector(this, sequences);
        //selector._parentController = this;
        //
        //_root = selector;
    }

    public override Define.HeroState CheckNextState()
    {
        float rand = Random.Range(0.0f, 1.0f);

        if (rand < 0.4f) NextState = Define.HeroState.Attack;
        else if (rand < 0.6f) NextState = Define.HeroState.Block;
        else if (rand < 0.8f) NextState = Define.HeroState.Strafe;
        else NextState = Define.HeroState.Rolling;

        return NextState;
    }
}
