using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    private List<QuestBase> _quests = new List<QuestBase>();

    private Dictionary<string, List<QuestBase>> _questList = new Dictionary<string, List<QuestBase>>();
    public Dictionary<string, List<QuestBase>> QuestLists { get { return _questList; } }

    public void Init()
    {
        List<Data.VillageData> villages = Managers.Data.VillageDataList;
        int size = villages.Count;
        for(int i = 0; i < size; i++)
        {
            _questList[villages[i].Name] = new List<QuestBase>();
        }
    }

    public void AddQuest(string village, QuestBase quest)
    {
        if (_questList.ContainsKey(village) == false)
        {
            _questList[village] = new List<QuestBase>();
        }

        _questList[village].Add(quest);
        quest.ID = _quests.Count;
        switch (quest.Type)
        {
            case Define.QuestType.AttackCamp:
                break;
            case Define.QuestType.DefenseVillage:
                break;
            case Define.QuestType.Raid:
                break;
            case Define.QuestType.Hunt:
                int id = quest.Target;
                GlobalGroupController con = Managers.General.GlobalGroups[id];
                con.QuestObjective = true;
                con.RelatedQuest = quest.ID;
                break;
            case Define.QuestType.Unknown:
                break;
        }

        _quests.Add(quest);
    }

}