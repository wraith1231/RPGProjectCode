using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameManager
{
    private GlobalPlayerController _globalPlayer;
    public GlobalPlayerController GlobalPlayer { get { return _globalPlayer; } }

    private List<GlobalNPCController> _globalCharacters;
    public List<GlobalNPCController> GlobalCharacters { get { return _globalCharacters; } }

    public List<GlobalGroupController> _globalGroups;
    public List<GlobalGroupController> GlobalGroups { get { return _globalGroups; } }

    private List<GlobalVillageController> _villages;
    public List<GlobalVillageController> GlobalVillages { get { return _villages; } }

    public void Init()
    {
        _globalPlayer = new GlobalPlayerController();
        _globalCharacters = new List<GlobalNPCController>();
        _globalGroups = new List<GlobalGroupController>();
        _villages = new List<GlobalVillageController>();
    }

    public void GroupSizeChange(int num)
    {
        //´Ã¸®´Â °Í¸¸ µÊ
        int size = num - _globalGroups.Count;
        for (int i = 0; i < size; i++)
            _globalGroups.Add(new GlobalGroupController(-1, "", 0));
    }

    public void VillageNewGame()
    {
        int size = Managers.Data.GetVillageCount();
        for(int i =0 ; i < size ; i++)
        {
            _villages.Add(new GlobalVillageController());
            _villages[i].NewGameLoad();
        }
    }

    public GlobalVillageData GetVillageData(string name)
    {
        int size = _villages.Count;
        for(int i = 0; i < size; i++)
        {
            if(name == _villages[i].Data.VillageName)
            {
                return _villages[i].Data;
            }
        }

        return null;
    }

    public bool ConstainsId(int id)
    {
        int size = _globalCharacters.Count;
        for(int i = 0; i < size; i++)
        {
            if (id == _globalCharacters[i].Data.HeroId)
                return true;
        }

        return false;
    }

}
