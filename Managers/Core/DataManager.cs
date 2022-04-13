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
    private Data.StatData _playerStartStat = new Data.StatData();
    private Data.StatData _npcMinimumStat = new Data.StatData();
    private Data.StatData _npcMaximumStat = new Data.StatData();
    public Data.StatData PlayerStartStat { get { return _playerStartStat; } private set { _playerStartStat = value; } }
    public Data.StatData NPCMinimumStat { get { return _npcMinimumStat; } private set { _npcMinimumStat = value; } }
    public Data.StatData NPCMaximumStat { get { return _npcMaximumStat; } private set { _npcMaximumStat = value; } }


    private Dictionary<string, Data.WeaponData> _onehandDict = new Dictionary<string, Data.WeaponData>();
    private Dictionary<string, Data.WeaponData> _twohandDict = new Dictionary<string, Data.WeaponData>();
    private Dictionary<string, Data.WeaponData> _shieldDict = new Dictionary<string, Data.WeaponData>();
    public Dictionary<string, Data.WeaponData> OnehandDict { get { return _onehandDict; } private set { _onehandDict = value; } }
    public Dictionary<string, Data.WeaponData> TwohandDict { get { return _twohandDict; } private set { _twohandDict = value; } }
    public Dictionary<string, Data.WeaponData> SheildDict { get { return _shieldDict; } private set { _shieldDict = value; } }

    public void Init()
    {
        LoadJson("StatData", StatDataLoad);
        LoadJson("WeaponList", WeaponDataLoad);
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
        loader.GetWeaponDict(out _onehandDict, out _twohandDict, out _shieldDict);
    }

    //private void WeaponDataRead()
    //{
    //    Data.WeaponList list = LoadJson<Data.WeaponList, string, Data.WeaponData>("WeaponList");
    //    list.MakeDict();
    //    list.GetWeaponDict(out _onehandDict, out _twohandDict, out _shieldDict);
    //}
}
