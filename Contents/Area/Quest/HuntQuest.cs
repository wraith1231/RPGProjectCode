using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntQuest : QuestBase
{
    public HuntQuest()
    {
        Type = Define.QuestType.Hunt;
    }

    //같은 그룹이면 다 사냥하는 퀘스트
    //보상만 있음
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
