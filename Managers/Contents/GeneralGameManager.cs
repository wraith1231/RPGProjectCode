using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameManager
{
    private GlobalPlayerController _globalPlayer;
    public GlobalPlayerController GlobalPlayer { get { return _globalPlayer; } }

    private List<GlobalNPCController> _globalCharacters;
    public List<GlobalNPCController> GlobalCharacters { get { return _globalCharacters; } }
    public void Init()
    {
        _globalPlayer = new GlobalPlayerController();
        _globalCharacters = new List<GlobalNPCController>();
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
