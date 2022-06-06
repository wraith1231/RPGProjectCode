using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface ILoader<Key, Value>
{
    //Dictionary<Key, Value> MakeDict();
    void MakeDict();
}

public class DataManager 
{
    #region Stat Data
    private Data.StatData _playerStartStat = new Data.StatData();
    private Data.StatData _npcMinimumStat = new Data.StatData();
    private Data.StatData _npcMaximumStat = new Data.StatData();
    public Data.StatData PlayerStartStat { get { return _playerStartStat; } private set { _playerStartStat = value; } }
    public Data.StatData NPCMinimumStat { get { return _npcMinimumStat; } private set { _npcMinimumStat = value; } }
    public Data.StatData NPCMaximumStat { get { return _npcMaximumStat; } private set { _npcMaximumStat = value; } }
    #endregion

    #region Weapon Data
    private Dictionary<string, Data.WeaponData> _onehandDict = new Dictionary<string, Data.WeaponData>();
    private Dictionary<string, Data.WeaponData> _twohandDict = new Dictionary<string, Data.WeaponData>();
    private Dictionary<string, Data.WeaponData> _shieldDict = new Dictionary<string, Data.WeaponData>();
    public Dictionary<string, Data.WeaponData> OnehandDict { get { return _onehandDict; } private set { _onehandDict = value; } }
    public Dictionary<string, Data.WeaponData> TwohandDict { get { return _twohandDict; } private set { _twohandDict = value; } }
    public Dictionary<string, Data.WeaponData> SheildDict { get { return _shieldDict; } private set { _shieldDict = value; } }

    private List<List<Data.WeaponData>> _onehandList = new List<List<Data.WeaponData>>();
    private List<List<Data.WeaponData>> _twohandList = new List<List<Data.WeaponData>>();
    private List<List<Data.WeaponData>> _shieldList = new List<List<Data.WeaponData>>();
    public List<List<Data.WeaponData>> OnehandList { get { return _onehandList; } private set { _onehandList = value; } }
    public List<List<Data.WeaponData>> TwohandList { get { return _twohandList; } private set { _twohandList = value; } }
    public List<List<Data.WeaponData>> ShieldList { get { return _shieldList; } private set { _shieldList = value; } }
    #endregion

    #region Village Data
    private List<Data.VillageData> _villageDataList = new List<Data.VillageData>();
    public List<Data.VillageData> VillageDataList { get { return _villageDataList; } private set { _villageDataList = value; } }
    private Dictionary<string, Data.VillageData> _villageDatas = new Dictionary<string, Data.VillageData>();
    public Dictionary<string, Data.VillageData> VillageDatas { get { return _villageDatas; } private set { _villageDatas = value; } }

    private int _villageNumber = 0;
    public int GetVillageNumber() { return _villageNumber; }
    #endregion

    public void Init()
    {
        LoadJson("StatData", StatDataLoad);
        LoadJson("WeaponList", WeaponDataLoad);
        LoadJson("VillageData", VillageDataLoad);
    }

    private void LoadJson(string key, Action<TextAsset> action)
    {
        Managers.Resource.Load<TextAsset>($"Data/{key}", action);
    }

    private void StatDataLoad(TextAsset asset)
    {
        Data.StatList loader = JsonUtility.FromJson<Data.StatList>(asset.text);
        loader.GetStatList(out _playerStartStat, out _npcMinimumStat, out _npcMaximumStat);
    }
    private void WeaponDataLoad(TextAsset asset)
    {
        Data.WeaponList loader = JsonUtility.FromJson<Data.WeaponList>(asset.text);
        loader.MakeDict();
        loader.GetOnehandList(out _onehandList);
        loader.GetTwohandList(out _twohandList);
        loader.GetShieldList(out _shieldList);
        loader.GetWeaponDict(out _onehandDict, out _twohandDict, out _shieldDict);
    }
    private void VillageDataLoad(TextAsset asset)
    {
        Data.VillageList loader = JsonUtility.FromJson<Data.VillageList>(asset.text);
        loader.MakeDict();
        _villageDatas = loader.VillageDicts;
        _villageDataList = loader.VillageDatas;
    }

    public int GetWeaponListSize(Define.WeaponCategory category, Define.WeaponType type)
    {
        if (category == Define.WeaponCategory.OneHand)
            return _onehandList[(int)type].Count;
        else if (category == Define.WeaponCategory.TwoHand)
            return _twohandList[(int)type].Count;
        else if (category == Define.WeaponCategory.Shield)
            return _shieldList[(int)type].Count;
        else
            return 0;
    }

    public Data.VillageData GetVillageData()
    {
        if(_villageDataList.Count > _villageNumber)
            return _villageDataList[_villageNumber++];

        return null;
    }

    public int GetVillageCount()
    {
        return _villageDataList.Count;
    }

    public Vector3 GetVillagePosition(int id)
    {
        if (id > _villageDataList.Count)
            return Vector3.zero;

        return _villageDataList[id].Position;
    }
}
