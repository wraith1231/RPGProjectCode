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

    //player 체력, 스태미너용
    public PlayerHeroController Player { get { return _player; } }

    //player가 나중에 init됐을때 카메라 세팅용
    public CameraController Camera { get { return _camera; } }

    //release를 위한 데이터 모아두기용
    public List<GameObject> _objects = new List<GameObject>();

    //인게임에 필요할 듯
    public Dictionary<int, List<BattleHeroController>> _groups = new Dictionary<int, List<BattleHeroController>>();
    //public List<List<BattleHeroController>> _groups = new List<List<BattleHeroController>>();

    //캐릭터 생성용
    public List<CharacterData> _charList = new List<CharacterData>();
    public Dictionary<string, GlobalCharacterData> _charDataList = new Dictionary<string, GlobalCharacterData>();
    private Dictionary<string, List<int>> _charIdList = new Dictionary<string, List<int>>();

    //Loading Scene용
    private static int PROGRESS = 4;
    private int _progress = 0;
    public float CurrentProgress { get { return (float)_progress / (float)PROGRESS; } }

    private int _instantiatedChar = 0;

    private TerrainData _areaTerrainData;
    public TerrainData AreaTerrainData { get { return _areaTerrainData; } set { _areaTerrainData = value; } }

    private Vector3 _playerPos;
    private Vector3 _startPos;

    private TerrainData TerrainData { get { return BattleTerrain.terrainData; } }

    //게임 클리어 조건용
    private int _livingChar = 0;

    public void AnotherOneBiteDust()
    {
        _livingChar--;
    }

    public Terrain BattleTerrain { get; set; }

    public void BattleSceneInitialize()
    {
        _instantiatedChar = 0;
        _playerInit = false;
        _cameraInit = false;
        _progress = 0;

        GroupInitialize();

        InstantiateTerrain();
    }

    private void GroupInitialize()
    {
        int listSize = _charList.Count;
        _livingChar = _charList.Count;

        for (int listNum = 0; listNum < listSize; listNum++)
        {
            if (_groups.ContainsKey(_charList[listNum].Group) == false)
                _groups[_charList[listNum].Group] = new List<BattleHeroController>();
        }
        
        _progress++;
    }

    private void InstantiateTerrain()
    {
        Managers.Resource.Instantiate("BattleTerrain", TerrainInstantiate);
    }

    private void TerrainInstantiate(GameObject go)
    {
        _objects.Add(go);
        GameObject.DontDestroyOnLoad(go);

        BattleTerrain = go.GetComponent<Terrain>();

        int heightmapResolution = TerrainData.heightmapResolution;
        int alphamapResolution = TerrainData.alphamapResolution;

        float[,] heights = new float[heightmapResolution, heightmapResolution];

        //player position 기준 -15~15 사이의 값을 300 x 300에 맞게 불려야함

        GlobalGroupController player = Managers.General.GlobalGroups[0];

        Vector3 terrainSize = _areaTerrainData.size;
        float startX = player.Position.x - heightmapResolution * 0.5f;
        float startZ = player.Position.z - heightmapResolution * 0.5f;

        float endX = player.Position.x + heightmapResolution * 0.5f;
        float endZ = player.Position.z + heightmapResolution * 0.5f;

        if (startX < 0)
        {
            startX = 0;
            endX = startX + heightmapResolution;
        }
        if (startZ < 0)
        { 
            startZ = 0;
            endZ = startZ + heightmapResolution; 
        }

        if (endX > _areaTerrainData.size.x)
        {
            endX = _areaTerrainData.size.x;
            startX = endX - heightmapResolution;
        }
        if (endZ > _areaTerrainData.size.z)
        {
            endZ = _areaTerrainData.size.z;
            startZ = endZ - heightmapResolution;
        }
        _playerPos = player.Position;
        _startPos = new Vector3(startX , 0, startZ);
        heights = _areaTerrainData.GetHeights((int)startX, (int)startZ, heightmapResolution, heightmapResolution);

        int z = 0;
        int x = 0;

        float playerY = _playerPos.y / _areaTerrainData.heightmapResolution;
        while(true)
        {
            float dist = heights[z, x] - playerY;

            heights[z, x] = heights[z, x] + dist * 0.5f;
            x++;
            if (x >= heightmapResolution)
            {
                x = 0;
                z++;
            }

            if (z >= heightmapResolution)
                break;
        }

        float[,,] alphas = _areaTerrainData.GetAlphamaps((int)startX, (int)startZ, alphamapResolution, alphamapResolution);

        TerrainData.SetHeightsDelayLOD(0, 0, heights);
      
        TerrainData.SetAlphamaps(0, 0, alphas);
        
        _progress++;

        InstantiateCharacterPrefab();
    }

    private void InstantiateCharacterPrefab()
    {
        Managers.Resource.Instantiate("Camera", (go) =>
        { _objects.Add(go); GameObject.DontDestroyOnLoad(go); _camera = go.GetComponent<CameraController>(); _progress++; });


        CharacterInstantiate();

    }

    private void CharacterInstantiate()
    {
        int listSize = _charList.Count;
        string key = "";
        for(int listNum = 0; listNum < listSize; listNum++)
        {
            switch (_charList[listNum].Type)
            {
                case Define.CharacterType.Human:
                    key = "Human";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(listNum);
                    Managers.Resource.Instantiate("Human", HumanCharacterInstantiate);
                    break;
                case Define.CharacterType.Animal:
                    break;
                case Define.CharacterType.Monster:
                    break;
                case Define.CharacterType.Unknown:
                    key = "Human";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(listNum);
                    Managers.Resource.Instantiate("Human", HumanCharacterInstantiate);
                    break;
                default:
                    key = "Human";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(listNum);
                    Managers.Resource.Instantiate("Human", HumanCharacterInstantiate);
                    break;
            }
        }
    }

    private void HumanCharacterInstantiate(GameObject go)
    {
        _objects.Add(go);
        _instantiatedChar++;
        GameObject.DontDestroyOnLoad(go);
        int id = _charIdList[go.name][0];
        _charIdList[go.name].RemoveAt(0);
        BattleHeroController controller;
        bool isPlayer = false;

        if(isPlayer == false)
            isPlayer = _charList[id].Player;
        if(isPlayer)
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

        controller.HeroId = _charList[id].HeroId;
        controller.Group = _charList[id].Group;

        if (_charDataList.ContainsKey(_charList[id].CharName))
            controller.SetBattleCharacterData(_charDataList[_charList[id].CharName]);
        else
            controller.SetBattleCharacterData(null);
        
        _groups[_charList[id].Group].Add(controller);

        Vector3 pos = _charList[id].StartPosition;
        if(isPlayer)
        {
            pos = _playerPos - _startPos;
            pos.x = (pos.x / TerrainData.heightmapResolution) * TerrainData.size.x;
            pos.z = (pos.z / TerrainData.heightmapResolution) * TerrainData.size.z;
            Debug.Log($"{TerrainData.heightmapResolution}, {_playerPos}, {_startPos}");
        }
        else
        {
            pos = _player.transform.position;
            pos.x += 10f;
            pos.z += 10f;
        }
        pos.y = TerrainData.GetInterpolatedHeight(pos.x / TerrainData.size.x, pos.z / TerrainData.size.z);
        controller.transform.position = pos;

        if (_instantiatedChar == _charList.Count)
            _progress++;
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
