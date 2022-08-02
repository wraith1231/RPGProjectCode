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
         * s1 - 적이 근처에 있는가? -> success 뜨면 다음으로, fail 뜨면 주변 정찰
         * s2 - 적이 거리 안에 있는가? -> success 뜨면 다음으로, fail이면 적에게 다가감
         * s3 - 적에 대한 행동 정의
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
