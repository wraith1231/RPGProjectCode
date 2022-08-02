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
        NodeBase action = new PatrolSequence(this, 5);
        SelectorNode selector = new SelectorNode(action);
        _root = selector;
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
