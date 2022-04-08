using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleGameManager
{
    private PlayerHeroController _player;
    private CameraController _camera;
    private bool _playerInit = false;
    private bool _cameraInit = false;
    public bool BothInit { get { return _playerInit == true && _cameraInit == true; } }
    public PlayerHeroController Player { get { return _player; } }
    public CameraController Camera { get { return _camera; } }

    public List<List<BattleHeroController>> _groups = new List<List<BattleHeroController>>();
    public List<GameObject> _objects = new List<GameObject>();

    public List<CharacterData> _charList = new List<CharacterData>();

    public Dictionary<string, GlobalCharacterData> _charDataList = new Dictionary<string, GlobalCharacterData>();

    private int _id = 0;
    //게임 클리어 조건용
    private int _livingChar = 0;
    public void AnotherOneBiteDust()
    {
        _livingChar--;
    }

    public Terrain BattleTerrain { get; set; }

    public void GroupInitialize()
    {
        int listSize = _charList.Count;
        _livingChar = _charList.Count;
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
    }

    public void InstantiateCharacterPrefab()
    {
        _id = 0;
        //Managers.Resource.Load<GameObject>("Camera", null);
        //Managers.Resource.Load<GameObject>("Human", null);

        CharacterInstantiate();

        Managers.Resource.Instantiate("Camera", (go) => 
        { _objects.Add(go); GameObject.DontDestroyOnLoad(go); _camera = go.GetComponent<CameraController>(); });
        Managers.Resource.Instantiate("BattleTerrain", TerrainInstantiate);
    }

    private void CharacterInstantiate()
    {
        int listSize = _charList.Count;
        for(int listNum = 0; listNum < listSize; listNum++)
        {
            switch (_charList[listNum].Type)
            {
                case Define.CharacterType.Human:
                    Managers.Resource.Instantiate("Human", HumanCharacterInstantiate);
                    break;
                case Define.CharacterType.Animal:
                    break;
                case Define.CharacterType.Monster:
                    break;
                case Define.CharacterType.Unknown:
                    Managers.Resource.Instantiate("Human", HumanCharacterInstantiate);
                    break;
                default:
                    Managers.Resource.Instantiate("Human", HumanCharacterInstantiate);
                    break;
            }
        }
    }

    private void HumanCharacterInstantiate(GameObject go)
    {
        _objects.Add(go);
        GameObject.DontDestroyOnLoad(go);
        int id = _id++;
        BattleHeroController controller;
        if(_charList[id].Player == true)
        {
            controller = go.AddComponent<PlayerHeroController>();
            _player = controller as PlayerHeroController;
            _playerInit = true;
            if (BothInit)
            {
                _camera.SetPlayer(_player);
                _player.SetCamera(_camera);
            }
        }
        else
        {
            controller = go.AddComponent<TestEnemyController>();
        }

        CharacterOutfit outfit = go.GetComponent<CharacterOutfit>();

        outfit.SetOutfit(_charList[id].Outfit);
        controller.SetEquipWeapon(_charList[id].Left, _charList[id].Right);
        go.name = _charList[id].CharName;
        _charList[id].HeroId = id;
        controller.SetCharacterData(_charList[id]);
        if (_charDataList.ContainsKey(_charList[id].CharName))
            controller.SetBattleCharacterData(_charDataList[_charList[id].CharName]);
        else
            controller.SetBattleCharacterData(null);

        _groups[_charList[id].Group].Add(controller);

        controller.transform.position = controller.Data.StartPosition;

    }

    public void BattleSceneStart()
    {
        _cameraInit = true;
        if (BothInit)
        {
            _camera.SetPlayer(_player);
            _player.SetCamera(_camera);
        }
        _camera.BattleSceneInit();
    }

    public void AddCharList(CharacterData data)
    {
        _charList.Add(data);
    }

    private void TerrainInstantiate(GameObject go)
    {
        _objects.Add(go);
        GameObject.DontDestroyOnLoad(go);

        BattleTerrain = go.GetComponent<Terrain>();
    }

    public void Clear()
    {
        _charDataList.Clear();
        _charList.Clear();

        int size = _objects.Count;
        for (int i = 0; i < size; i++)
        {
            if(_objects[i] != null)
            Managers.Resource.Release(_objects[i]);
        }
        _objects.Clear();

        _groups.Clear();
    }

}
