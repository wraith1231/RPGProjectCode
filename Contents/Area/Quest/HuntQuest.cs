using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntQuest : QuestBase
{
    public HuntQuest()
    {
        Type = Define.QuestType.Hunt;
    }

    //���� �׷��̸� �� ����ϴ� ����Ʈ
    //���� ����
    public override void Progress(GameObject go)
    {

    }

    public override void Reward(params int[] groupId)
    {
        base.Reward(groupId);

    }

    public override void QuestExpired()
    {

    }
}
