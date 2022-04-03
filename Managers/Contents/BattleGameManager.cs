using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGameManager
{
    public PlayerHeroController _player;
    public CameraController _camera;

    public List<List<BattleHeroController>> _groups = new List<List<BattleHeroController>>();

    public List<CharacterData> _charList = new List<CharacterData>();

    public Dictionary<string, GlobalCharacterData> _charDataList = new Dictionary<string, GlobalCharacterData>();

    public Terrain BattleTerrain { get; set; }

    public List<BattleHeroController> GetGroupList(int num)
    {
        if (num < 0 || _groups.Count <= num)
            return null;

        return _groups[num];
    }

    public CharacterData GetData(int id)
    {
        return _charList[id];
    }

    public int GetMaxGroupList()
    {
        return _groups.Count;
    }

    public void AddCharacterData(string name, GlobalCharacterData data)
    {
        _charDataList[name] = data;
    }

    public void BattleSceneStart()
    {
        int id = 0;

        int listSize = _charList.Count;
        int max = -1;
        int num = 0;

        for (int listNum = 0; listNum < listSize; listNum++)
        {
            num = _charList[listNum].Group;
            if (num > max)
                max = num;
        }

        for (int i = 0; i <= max; i++)
            _groups.Add(new List<BattleHeroController>());

        for(int listNum = 0; listNum < listSize; listNum++)
        {
            GameObject go = Managers.Resource.Instantiate("Characters/Human");

            BattleHeroController controller;
            if (_charList[listNum].Player == true)
            {
                controller = go.AddComponent<PlayerHeroController>();
                _player = (PlayerHeroController)controller;
            }
            else
            {
                //원래 캐릭터 성격에 따라, 미션에 따라 다른 식으로 할 예정이지만
                //지금은 전부 test 박아넣음
                controller = go.AddComponent<TestEnemyController>();
            }

            CharacterOutfit outfit = go.GetComponent<CharacterOutfit>();
            outfit.SetOutfit(_charList[listNum].Outfit);
            controller.SetEquipWeapon(_charList[listNum].Left, _charList[listNum].Right);

            _charList[listNum].HeroId = id++;
            controller.SetCharacterData(_charList[listNum]);
            if (_charDataList.ContainsKey(_charList[listNum].CharName))
                controller.SetBattleCharacterData(_charDataList[_charList[listNum].CharName]);
            else
                controller.SetBattleCharacterData(null);

            _groups[_charList[listNum].Group].Add(controller);

            controller.transform.position = controller.Data.StartPosition;
        }

        _camera = Managers.Resource.Instantiate("Camera").GetComponent<CameraController>();

        _camera.SetPlayer(_player);
        _player.SetCamera(_camera);

        _camera.BattleSceneInit();
    }

    public void AddCharList(CharacterData data)
    {
        _charList.Add(data);
    }

    public void Clear()
    {
        _charDataList.Clear();
        _charList.Clear();

        foreach (List<BattleHeroController> group in _groups)
            group.Clear();

        _groups.Clear();
    }
}
