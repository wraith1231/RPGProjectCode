using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidQuest : QuestBase
{
    public RaidQuest()
    {
        Type = Define.QuestType.Raid;
    }

    //Ư�� ��� ����ϴ� ����Ʈ
    //�����ϸ� �������� ���� �� ��
    public override void Progress(GameObject go)
    {
        BattleHeroController con = go.GetComponent<BattleHeroController>();
        if (con == null)
            return;

        if (con.HeroId != Target)
            return;

        Cleared = true;
    }

    public override void Reward(params int[] groupId)
    {
        base.Reward(groupId);

        TargetVillage.Condition = Define.VillageCondition.Recontract;
    }

    public override void QuestExpired()
    {
        TargetVillage.Condition = Define.VillageCondition.Destroyed;
    }
}
