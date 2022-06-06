using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCampQuest : QuestBase
{
    //몬스터 캠프 공격 퀘스트
    //보상만 있음
    public AttackCampQuest( )
    {
        Type = Define.QuestType.AttackCamp;
    }

    public override void Progress(GameObject go)
    {

    }

    public override void QuestExpired()
    {

    }

    public override void Reward(params int[] groupId)
    {
        base.Reward(groupId);

    }
}
