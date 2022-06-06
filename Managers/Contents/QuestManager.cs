using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
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
            return;

        _questList[village].Add(quest);
    }
}