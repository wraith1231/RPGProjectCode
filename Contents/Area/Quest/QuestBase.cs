using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Rewards
{
    //player ����
    public float Foods;
    public float Experience;
    public float VillRelationship;

    //village ����
    public float Safety;
}

public abstract class QuestBase
{
    public int ID;
    //target�� type�� ���� villlage id, hero id �� ���缭 �־����
    public Define.QuestType Type;
    //���� ������� TargetVillage ����
    public int Target;
    public bool Cleared = false;
    public string QuestText;
    //��������
    public int Maturity;

    //����Ʈ ���� �׷�� ������ �ʿ������
    public List<int> AcceptedGroups = new List<int>();

    //�ൿ �Ҷ����� ȣ��� ����
    public abstract void Progress(GameObject go);

    //����, ������ ����� �Լ�������
    //reward text�� json���� ������ ���ø� �޾Ƽ�..? server\packetgenerator ������Ʈ ����
    //quest �ذ��� �׷� id
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

    //defense�� raid�� �������� ���� �����ϴ� �߰�
    public virtual void QuestExpired()
    {

    }
    public GlobalVillageData OrderedVillage;
    public GlobalVillageData TargetVillage;
}
