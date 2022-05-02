using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVillageController
{
    private GlobalVillageData _data;
    public GlobalVillageData Data { get { return _data; } }

    public GlobalVillageController()
    {

    }

    public void NewGameLoad()
    {
        _data = new GlobalVillageData();
    }

    public void LoadGameInit(Data.VillageData data)
    {
        _data = new GlobalVillageData(data);
    }
}
