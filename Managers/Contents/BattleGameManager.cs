using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BattleGameManager
{
    private struct AreaObjects
    {
        public string Name;
        public Vector3 Position;
    }

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
    public Dictionary<int, List<BattleCharacterController>> _groups = new Dictionary<int, List<BattleCharacterController>>();
    private List<int> _groupKeys;
    //public List<List<BattleHeroController>> _groups = new List<List<BattleHeroController>>();

    //캐릭터 생성용
    public List<CharacterData> _charList = new List<CharacterData>();
    public Dictionary<string, GlobalCharacterData> _charDataList = new Dictionary<string, GlobalCharacterData>();
    private Dictionary<string, List<int>> _charIdList = new Dictionary<string, List<int>>();

    //오브젝트 생성용
    private Dictionary<string, AreaObjects> _areaObjectInstantiateData = new Dictionary<string, AreaObjects>();
    private List<AreaObjects> _areaObjects = new List<AreaObjects>();
    private int _areaObjectCount = 0;
    public void AddAreaObject(string name, Vector3 pos) 
    {
        AreaObjects temp = new AreaObjects();
        temp.Name = name;
        temp.Position = pos;
        _areaObjects.Add(temp);
    }

    //Loading Scene용
    //terrain, player, npc, group, areaObject
    private static int PROGRESS = 5;
    private int _progress = 0;
    public float CurrentProgress { get { return (float)_progress / (float)PROGRESS; } }

    private int _instantiatedChar = 0;

    private TerrainData _areaTerrainData;
    public TerrainData AreaTerrainData { get { return _areaTerrainData; } set { _areaTerrainData = value; } }

    private Vector3 _playerPos;
    private Vector3 _startPos;

    private TerrainData TerrainData { get { return BattleTerrain.terrainData; } }

    private int _generated = 0;

    //게임 클리어 조건용
    private int _livingChar = 0;

    public void AnotherOneBiteDust()
    {
        _livingChar--;

        if(_livingChar == _groups[0].Count)
        {
            GlobalGroupController controller = Managers.General.GlobalGroups[0];
            List<GlobalGroupController> list = Managers.General.GlobalGroups;
            int size = _groupKeys.Count;
            for(int i = 0; i < size; i++)
            {
                if(list[_groupKeys[i]].QuestObjective == true)
                {
                    
                }

            }
            Managers.Scene.LoadSceneAsync(Define.SceneType.AreaScene);
        }
    }

    public Terrain BattleTerrain { get; set; }

    public void BattleSceneInitialize()
    {
        _instantiatedChar = 0;
        _playerInit = false;
        _cameraInit = false;
        _progress = 0;
        _areaObjectCount = 0;
        _generated = 0;

        GroupInitialize();

        InstantiateTerrain();
    }

    private void GroupInitialize()
    {
        int listSize = _charList.Count;
        _livingChar = _charList.Count;
        _groupKeys = new List<int>();

        for (int listNum = 0; listNum < listSize; listNum++)
        {
            if (_groups.ContainsKey(_charList[listNum].Group) == false)
            {
                _groupKeys.Add(_charList[listNum].Group);
                _groups[_charList[listNum].Group] = new List<BattleCharacterController>();
            }
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

        float playerY = _playerPos.y / _areaTerrainData.heightmapScale.y;
        while(true)
        {
            float dist = heights[z, x] - playerY;

            heights[z, x] = heights[z, x] + dist * 2f;
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

        AreaObjectInstantiate();
        
    }

    private void InstantiateCharacterPrefab()
    {
        Managers.Resource.Instantiate("Camera", (go) =>
        { _objects.Add(go); GameObject.DontDestroyOnLoad(go); _camera = go.GetComponent<CameraController>(); _progress++; });


        CharacterInstantiate();
        //AreaObjectInstantiate();
    }

    private void CharacterInstantiate()
    {
        int listSize = _charList.Count;
        string key = "";
        for(int listNum = 0; listNum < listSize; listNum++)
        {
            _generated++;
            switch (_charList[listNum].Type)
            {
                case Define.CharacterType.Human:
                    key = "Human";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(_charList[listNum].HeroId);
                    Managers.Resource.Instantiate("Human", HumanCharacterInstantiate);
                    break;
                case Define.CharacterType.Animal:
                    break;
                case Define.CharacterType.Monster:
                    key = _charList[listNum].CharName;
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(_charList[listNum].HeroId);
                    Managers.Resource.Instantiate(key, MonsterCharacterInstantiate);
                    break;
                case Define.CharacterType.Unknown:
                    key = "Human";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(_charList[listNum].HeroId);
                    Managers.Resource.Instantiate("Human", HumanCharacterInstantiate);
                    break;
                default:
                    key = "Human";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(_charList[listNum].HeroId);
                    Managers.Resource.Instantiate("Human", HumanCharacterInstantiate);
                    break;
            }
        }
        _charList.Clear();
    }

    private Vector3 GetScalingPosition(Vector3 pos)
    {
        Vector3 ret = pos - _startPos;
        ret.x = (ret.x / TerrainData.heightmapResolution) * TerrainData.size.x;
        ret.z = (ret.z / TerrainData.heightmapResolution) * TerrainData.size.z;
        ret.y = TerrainData.GetInterpolatedHeight(ret.x / TerrainData.size.x, ret.z / TerrainData.size.z);

        return ret;
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

        GlobalCharacterController cont = Managers.General.GlobalCharacters[id];

        if(isPlayer == false)
            isPlayer = cont.Data.Player;

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

        outfit.SetOutfit(cont.Data.Outfit);
        controller.SetEquipWeapon(cont.Data.Left, cont.Data.Right);
        cont.BattleDataUpdate();
        BattleControllerSetting(cont, go, controller);

        if (_instantiatedChar == _generated)
            _progress++;
    }

    private void BattleControllerSetting(GlobalCharacterController gc, GameObject go, BattleCharacterController controller)
    {
        go.name = gc.Data.CharName;
        controller.HeroId = gc.Data.HeroId;
        controller.Group = gc.Data.Group;
        controller.SetBattleCharacterData(gc.BattleData);
        _groups[gc.Data.Group].Add(controller);

        GlobalGroupController groupCon = Managers.General.GlobalGroups[controller.Group];

        Vector3 pos = groupCon.Position;
        while(true)
        {
            pos = GetScalingPosition(pos);
            if (Physics.OverlapSphere(pos + Vector3.up, 0.5f).Length == 0)
                break;
            pos.x += 1f;
            pos.z += 1f;
            Debug.Log(pos);
        }
        //pos.x += UnityEngine.Random.Range(-30, 31);
        //pos.z += UnityEngine.Random.Ra
        Debug.Log(pos);
        controller.transform.position = pos;
    }

    private void MonsterCharacterInstantiate(GameObject go)
    {
        _objects.Add(go);
        _instantiatedChar++;
        GameObject.DontDestroyOnLoad(go);
        int id = _charIdList[go.name][0];
        _charIdList[go.name].RemoveAt(0);
        BattleCharacterController controller;
        GlobalCharacterController con = Managers.General.GlobalCharacters[id];

        controller = go.GetComponent<BattleMonsterController>();

        BattleControllerSetting(con, go, controller);
    

        if (_instantiatedChar == _generated)
            _progress++;
    }

    private void AreaObjectInstantiate()
    {
        int size = _areaObjects.Count;
        if(size == 0)
        {
            _progress++; 
            InstantiateCharacterPrefab();
        }

        for (int i = 0; i < size; i++)
        {
            _areaObjectInstantiateData[_areaObjects[i].Name] = _areaObjects[i];
            Managers.Resource.Instantiate("Battle/" + _areaObjects[i].Name, AreaObjectInstantiated);
        }
    }

    private void AreaObjectInstantiated(GameObject go)
    {
        _objects.Add(go);
        GameObject.DontDestroyOnLoad(go);
        string[] name =go.name.Split('/');

        AreaObjects data = _areaObjectInstantiateData[name[1]];

        Vector3 pos = data.Position;
        go.transform.position = GetScalingPosition(pos);
        
        _areaObjectCount++;

        if (_areaObjectCount >= _areaObjects.Count)
        {
            _progress++;
            InstantiateCharacterPrefab();
        }
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
    public void AddGroup(GlobalGroupController group)
    {
        List<int> members = group.MemberList;
        int size = members.Count;
        for (int i = 0; i < size; i++)
            AddCharList(Managers.General.GlobalCharacters[members[i]].Data);
    }

    public void Clear()
    {
        _areaObjectInstantiateData.Clear();
        _areaObjects.Clear();
        _charDataList.Clear();
        _charList.Clear();

        int size = _objects.Count;
        for (int i = 0; i < size; i++)
        {
            if(_objects[i] != null)
            Managers.Resource.Release(_objects[i]);
        }
        _objects.Clear();

        _groupKeys.Clear();
        _groups.Clear();
    }

}
