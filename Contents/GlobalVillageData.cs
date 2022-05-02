using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVillageData
{
    private string _villageName;
    public string VillageName { get { return _villageName; } }

    private float _growth;
    public float Growth { get { return _growth; } set { _growth = value; } }
    private float _growthPerDay;    //plus
    public float GrowthPerDay { get { return _growthPerDay; } set { _growthPerDay = value; } }

    private float _safety;
    public float Safety { get { return _safety; } set { _safety = value; } }
    private float _safetyPerDay;    //minus
    public float SafetyPerDay { get { return _safetyPerDay; } set { _safetyPerDay = value; } }

    private Dictionary<Define.Facillities, List<int>> _facillityMembers = new Dictionary<Define.Facillities, List<int>>();
    private Dictionary<int, float> _charFavorites = new Dictionary<int, float>();

    private List<QuestBase> _quests = new List<QuestBase>();

    private Define.VillageCondition _condition = Define.VillageCondition.Idle;
    public Define.VillageCondition Condition { get { return _condition; } set { _condition = value; } }

    public GlobalVillageData()
    {
        Data.VillageData data = Managers.Data.GetVillageData();

        _villageName = data.Name;
        _growth = data.Growth;
        _growthPerDay = data.GrowthPerDay;
        _safety = data.Safety;
        _safetyPerDay = data.SafetyPerDay;
    }

    public GlobalVillageData(Data.VillageData data)
    {
        _villageName = data.Name;
        _growth = data.Growth;
        _growthPerDay = data.GrowthPerDay;
        _safety = data.Safety;
        _safetyPerDay = data.SafetyPerDay;
    }

    public void VillageDayChange()
    {
        _growth += _growthPerDay;
        _safety -= _safetyPerDay;
    }

    public void AddQuest(QuestBase quest)
    {
        _quests.Add(quest);
    }
}
