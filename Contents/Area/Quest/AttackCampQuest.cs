using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCampQuest : QuestBase
{
    //���� ķ�� ���� ����Ʈ
    //���� ����
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
