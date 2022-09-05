using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseVillageQuest : QuestBase
{
    public DefenseVillageQuest()
    {
        Type = Define.QuestType.DefenseVillage;
    }

    //���� ��Ű��
    //�����ϸ� ������ ���۳�
    public override void Progress(GameObject go)
    {

    }

    public override void QuestExpired()
    {
        base.QuestExpired();
        TargetVillage.Condition = Define.VillageCondition.Destroyed;
    }

    public override void Reward(params int[] groupId)
    {
        base.Reward(groupId);

    }
}
