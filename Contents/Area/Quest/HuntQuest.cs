using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntQuest : QuestBase
{
    //같은 그룹이면 다 사냥하는 퀘스트
    //보상만 있음
    public override void Progress(GameObject go)
    {

    }

    public override void Reward(int groupId)
    {

    }

    public override void QuestExpired()
    {

    }
}
