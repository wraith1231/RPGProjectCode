using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Rewards
{
    //player 보상
    public float Foods;
    public float Experience;
    public float VillRelationship;

    //village 보상
    public float Safety;
}

public abstract class QuestBase
{
    public int ID;
    //target은 type에 따라서 villlage id, hero id 등 맞춰서 넣어야함
    public Define.QuestType Type;
    //마을 방어전은 TargetVillage 설정
    public int Target;
    public bool Cleared = false;
    public string QuestText;
    //만기일자
    public int Maturity;

    //퀘스트 받은 그룹들 당장은 필요없을듯
    public List<int> AcceptedGroups = new List<int>();

    //행동 할때마다 호출될 예정
    public abstract void Progress(GameObject go);

    //보수, 보수의 방식은 함수형으로
    //reward text는 json같은 곳에서 템플릿 받아서..? server\packetgenerator 프로젝트 참조
    //quest 해결한 그룹 id
    public virtual void Reward(params int[] groupId)
    {
        int size = groupId.Length;
        for (int i = 0; i < size; i++)
        {
            Managers.General.GlobalGroups[groupId[i]].Foods += RewardList.Foods;

            TargetVillage.Safety += RewardList.Safety;
            TargetVillage.IncreaseCharacterFavor(groupId[i], RewardList.VillRelationship);
        }
    }

    public Rewards RewardList;
    public string RewardText;

    //defense나 raid는 마을에도 영향 가야하니 추가
    public virtual void QuestExpired()
    {

    }
    public GlobalVillageData OrderedVillage;
    public GlobalVillageData TargetVillage;
}
