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
        //StatDataRead();
        LoadJson("StatData", StatDataLoad);
        LoadJson("WeaponList", WeaponDataLoad);
        //WeaponDataRead();
    }

    //private Loader LoadJson<Loader, Key, Value>(string path, Action<Loader> action) where Loader : ILoader<Key, Value>
    //{
    //    TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}", null).Result as TextAsset;
    //    return JsonUtility.FromJson<Loader>(textAsset.text);
    //}

    private void LoadJson(string key, Action<TextAsset> action)
    {
        Managers.Resource.Load<TextAsset>($"Data/{key}", action);
    }
    //
    //private void StatDataRead()
    //{
    //    Data.StatList list = LoadJson<Data.StatList, int, Data.StatData>("StatData");
    //    list.MakeDict();
    //    list.GetStatList(out _statDict);
    //}
    private void StatDataLoad(TextAsset asset)
    {
        Data.StatList loader = JsonUtility.FromJson<Data.StatList>(asset.text);
        loader.MakeDict();
        loader.GetStatList(out _statDict);
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
