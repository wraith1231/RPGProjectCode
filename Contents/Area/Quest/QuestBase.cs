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
    //target�� type�� ���� villlage id, hero id �� ���缭 �־����
    public Define.QuestType Type;
    public int Target;
    public bool Cleared = false;
    public string QuestText;

    //����Ʈ ���� �׷�� ������ �ʿ������
    //public List<int> AcceptedGroups = new List<int>();

    //�ൿ �Ҷ����� ȣ��� ����
    public abstract void Progress(GameObject go);

    //����, ������ ����� �Լ�������
    //reward text�� json���� ������ ���ø� �޾Ƽ�..? server\packetgenerator ������Ʈ ����
    //quest �ذ��� �׷� id
    public abstract void Reward(int groupId);
    public Rewards RewardList;
    public string RewardText;

    //defense�� raid�� �������� ���� �����ϴ� �߰�
    public abstract void QuestExpired();
    public GlobalVillageData OrderedVillage;
}
