using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidQuest : QuestBase
{
    //특정 대상 사냥하는 퀘스트
    //실패하면 마을에도 영향 좀 감
    public override void Progress(GameObject go)
    {
        BattleHeroController con = go.GetComponent<BattleHeroController>();
        if (con == null)
            return;

        if (con.HeroId != Target)
            return;

        Cleared = true;
    }

    public override void Reward(int groupId)
    {

    }

    public override void QuestExpired()
    {

    }
}
