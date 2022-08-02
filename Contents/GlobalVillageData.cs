using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVillageData
{
    private int _villageId;
    public int VillageId { get { return _villageId; } set { _villageId = value; } }

    private string _villageName;
    public string VillageName { get { return _villageName; } }

    private float _power;
    public float Power { get { return _power; }set { _power = value; } }

    private float _foods;
    public float Foods { get { return _foods; } set { _foods = value; } }

    private float _gold;
    public float Gold { get { return _gold; } set { _gold = value; } }

    private float _currentEndurance;
    public float CurrentEndurance { get { return _currentEndurance; } set { _currentEndurance = value; } }
    private float _maxEndurance;
    public float MaxEndurance { get { return _maxEndurance; } set { _maxEndurance = value; } }

    private float _growth;
    public float Growth { get { return _growth; } set { _growth = value; } }
    private float _growthPerDay;    //plus
    public float GrowthPerDay { get { return _growthPerDay; } set { _growthPerDay = value; } }

    private float _safety;
    public float Safety { get { return _safety; } set { _safety = value; } }
    private float _safetyPerDay;    //minus
    public float SafetyPerDay { get { return _safetyPerDay; } set { _safetyPerDay = value; } }

    private float _detectRange;
    public float DetectRange { get { return _detectRange; } set { _detectRange = value; } }

    private List<int> _currentGroups = new List<int>();
    public List<int> CurrentGroups { get { return _currentGroups; } set { _currentGroups = value; } }

    private List<Define.Facilities> _facilities = new List<Define.Facilities>();
    public List<Define.Facilities> FacilityLists { get { return _facilities; } }
    private Dictionary<Define.Facilities, List<int>> _facilityMembers = new Dictionary<Define.Facilities, List<int>>();
    public Dictionary<Define.Facilities, List<int>> Facility { get { return _facilityMembers; } }
    public int FacilityCount() { return _facilities.Count; }
    
    private Dictionary<int, float> _charFavorites = new Dictionary<int, float>();

    private Define.VillageCondition _condition = Define.VillageCondition.Idle;
    public Define.VillageCondition Condition { get { return _condition; } set { _condition = value; } }

    private Transform _areaTranfrom;
    public Transform AreaTransfrom { get { return _areaTranfrom; } set { _areaTranfrom = value; } }
    
    public bool IsVillageConditionOK { get { return _condition == Define.VillageCondition.Idle; } }

    public GlobalVillageData()
    {
        Data.VillageData data = Managers.Data.GetVillageData();
        _villageId = Managers.Data.GetVillageNumber() - 1;

        _villageName = data.Name;
        _growth = data.Growth;
        _growthPerDay = data.GrowthPerDay;
        _safety = data.Safety;
        _safetyPerDay = data.SafetyPerDay;
        _detectRange = data.DetectRange;
        _power = data.Power;
        _foods = data.Foods;
        _gold = data.Gold;
        _maxEndurance = data.Endurance;
        _currentEndurance = data.Endurance;

        int size = (int)Define.Facilities.Unknown;
        for(int i = 0; i < size; i++)
        {
            if (data.Facilities[i] == 1)
            {
                _facilities.Add((Define.Facilities)i);
                _facilityMembers[(Define.Facilities)i] = new List<int>();
            }
        }
    }

    public GlobalVillageData(Data.VillageData data)
    {
        _villageName = data.Name;
        _growth = data.Growth;
        _growthPerDay = data.GrowthPerDay;
        _safety = data.Safety;
        _safetyPerDay = data.SafetyPerDay;
        _detectRange = data.DetectRange;
        _power = data.Power;
        _foods = data.Foods;
        _gold = data.Gold;
        _maxEndurance = data.Endurance;
        _currentEndurance = data.Endurance;

        int size = (int)Define.Facilities.Unknown;
        for (int i = 0; i < size; i++)
        {
            if (data.Facilities[i] == 1)
                _facilityMembers[(Define.Facilities)i] = new List<int>();
        }
    }

    public void VillageDayChange()
    {
        _growth += _growthPerDay;
        _safety -= _safetyPerDay;
    }

    public void IncreaseCharacterFavor(int id, float value)
    {
        if(_charFavorites.ContainsKey(id) == true)
        {
            _charFavorites[id] += value;
        }
        else
        {
            _charFavorites[id] = value;
        }
    }

    public void BattlePhase(GlobalGroupController cont)
    {
        List<int> members = cont.MemberList;
        List<GlobalCharacterController> charContList = Managers.General.GlobalCharacters;
        int size = members.Count;
        for(int i =0; i < size; i++)
        {
            BattleCharacterData data = charContList[members[i]].BattleData;

            //vill -> char
            data.BattlePhaseVillage(this);
        }

        float damage = (cont.GroupPower - _power) / _safety;
        _currentEndurance -= damage;

    }
}
