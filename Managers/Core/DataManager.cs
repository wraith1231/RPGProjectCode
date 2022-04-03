using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    //Dictionary<Key, Value> MakeDict();
    void MakeDict();
}

public class DataManager 
{
    private Dictionary<int, Data.StatData> _statDict = new Dictionary<int, Data.StatData>();
    public Dictionary<int, Data.StatData> StatDict { get { return _statDict; } private set { _statDict = value; } }

    private Dictionary<string, Data.WeaponData> _onehandDict = new Dictionary<string, Data.WeaponData>();
    private Dictionary<string, Data.WeaponData> _twohandDict = new Dictionary<string, Data.WeaponData>();
    private Dictionary<string, Data.WeaponData> _shieldDict = new Dictionary<string, Data.WeaponData>();
    public Dictionary<string, Data.WeaponData> OnehandDict { get { return _onehandDict; } private set { _onehandDict = value; } }
    public Dictionary<string, Data.WeaponData> TwohandDict { get { return _twohandDict; } private set { _twohandDict = value; } }
    public Dictionary<string, Data.WeaponData> SheildDict { get { return _shieldDict; } private set { _shieldDict = value; } }

    public void Init()
    {
        StatDataRead();
        WeaponDataRead();
    }

    private Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Datas/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    private void StatDataRead()
    {
        Data.StatList list = LoadJson<Data.StatList, int, Data.StatData>("StatData");
        list.MakeDict();
        list.GetStatList(out _statDict);
    }

    private void WeaponDataRead()
    {
        Data.WeaponList list = LoadJson<Data.WeaponList, string, Data.WeaponData>("WeaponList");
        list.MakeDict();
        list.GetWeaponDict(out _onehandDict, out _twohandDict, out _shieldDict);
    }
}
