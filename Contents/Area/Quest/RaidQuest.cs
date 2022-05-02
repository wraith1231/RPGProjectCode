using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidQuest : QuestBase
{
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

    public override void Reward(int groupId)
    {

    }

    public override void QuestExpired()
    {

    }
}
