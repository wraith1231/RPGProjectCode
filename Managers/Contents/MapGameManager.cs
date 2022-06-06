using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGameManager
{
    private static float DayChangeTime = 10f;

    private AreaPlayerController _player;
    private AreaCameraController _camera;
    private bool _playerInit = false;
    private bool _cameraInit = false;
    public bool BothInit { get { return _playerInit == true && _cameraInit == true; } }

    private List<VillageStatus> _villageLists = new List<VillageStatus>();
    public List<VillageStatus> VillageLists { get { return _villageLists; } }
    private Dictionary<string, VillageStatus> _villages = new Dictionary<string, VillageStatus>();
    public Dictionary<string, VillageStatus> Villages { get { return _villages; } }

    private List<GameObject> _roads = new List<GameObject>();
    public List<GameObject> Roads { get { return _roads; } }

    private List<GameObject> _charLists = new List<GameObject>(); //��ü ĳ���� ������
    private List<GameObject> _objects = new List<GameObject>();  //release ��
    private Dictionary<string, List<int>> _charIdList = new Dictionary<string, List<int>>();
    private List<AreaGroupController> _controllers = new List<AreaGroupController>();

    private Terrain _areaTerrain;
    public TerrainData MapData { get { return _areaTerrain.terrainData; } }

    private static int PROGRESS = 4;
    private int _progress = 0;
    public float CurrentProgress { get { return (float)_progress / (float)PROGRESS; } }

    public bool AreaSceneNow = false;
    private float _time = 0.0f;
    private int _day = 0;
    public int Day { get { return _day; } set { _day = value; } }

    public Action<int> DayChangeUpdate;

    private int _instantiatedChar = 0;

    //둘이 한 세트
    private Dictionary<int, List<int>> _groups;
    private List<int> _keys;

    #region Initialize
    public void DataInstantiate()
    {
        _playerInit = false;
        _cameraInit = false;
        _progress = 0;
        _instantiatedChar = 0;
        //Debug.Log("Data Instantiate");

        Managers.Resource.Instantiate("AreaTerrain", TerrainInstantiated);
    }
    public void GroupInitialize()
    {
        _groups = new Dictionary<int, List<int>>();
        _keys = new List<int>();

        _groups[0] = new List<int>();
        _keys.Add(0);
        _groups[0].Add(Managers.General.GlobalPlayer.Data.HeroId);

        List<GlobalCharacterController> data = Managers.General.GlobalCharacters;

        int listSize = data.Count;
        
        for (int i = 0; i < listSize; i++)
        {
            if (data[i].Data.Type == Define.CharacterType.Human)
            {
                if (_groups.ContainsKey(data[i].Data.Group) == false)
                {
                    _keys.Add(data[i].Data.Group);
                    _groups[data[i].Data.Group] = new List<int>();

                }
                _groups[data[i].Data.Group].Add(data[i].Data.HeroId);
            }
        }
    }

    private void TerrainInstantiated(GameObject go)
    {
        _objects.Add(go);
        GameObject.DontDestroyOnLoad(go);

        _areaTerrain = go.GetComponent<Terrain>();
        _progress++;
        //Debug.Log($"Terrain Instantiated{_progress}");
        CharacterInstantiate();
        CameraInstantiate();
    }

    private void CharacterInstantiate()
    {
        //근데 이거 캐릭터 말고 카트로 바꿀 수 있음
        Managers.Resource.Instantiate("AreaCart", PlayerInstantiated);
        NPCInstantiate();

    }

    private void ControllerSetting(GameObject go, AreaGroupController controller, int id)
    {
        controller.SetTerrainData(_areaTerrain.terrainData);
        controller.GroupId = id;

        go.name = Managers.General.GlobalGroups[id].GroupName;

        Vector3 pos = Managers.General.GlobalGroups[id].Position;
        pos.y = _areaTerrain.terrainData.GetInterpolatedHeight(pos.x / _areaTerrain.terrainData.size.x, pos.z / _areaTerrain.terrainData.size.z);
        go.transform.position = pos;

        controller.PrevNode = Managers.General.GlobalGroups[id].Position;
    }

    //수정할때 HumanCharInstantiated 확인
    private void PlayerInstantiated(GameObject go)
    {
        _objects.Add(go);
        GameObject.DontDestroyOnLoad(go);

        _player = go.AddComponent<AreaPlayerController>();

        ControllerSetting(go, _player, 0);

        _progress++;
        //Debug.Log($"Player Instantiated{_progress}");
        _playerInit = true;
        SetPlayerAndCameraIfInitialized();
    }

    private void NPCInstantiate()
    {
        List<GlobalGroupController> data = Managers.General.GlobalGroups;
        int listSize = data.Count;
        string key = "";
        int count = 1;
        for (int listNum = 1; listNum < listSize; listNum++)
        {
            if (data[listNum].GroupMemberCount() == 0)
                continue;

            switch (data[listNum].Type)
            {
                case Define.GroupType.Mercenary:
                    key = "AreaCart";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(_keys[count++]);
                    Managers.Resource.Instantiate("AreaCart", HumanCharInstantiated);
                    
                    break;
                case Define.GroupType.Merchant:
                    break;
                case Define.GroupType.Monster:
                    break;
                case Define.GroupType.Unknown:
                    key = "AreaCart";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(_keys[count++]);
                    Managers.Resource.Instantiate("AreaCart", HumanCharInstantiated);
                    break;
                default:
                    key = "Cart";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(_keys[count++]);
                    Managers.Resource.Instantiate("AreaCart", HumanCharInstantiated);
                    break;
            }
        }
        Debug.Log($"char id list : {_charIdList.Count}");
    }

    //수정할때 PlayerInstantiated도 확인
    private void HumanCharInstantiated(GameObject go)
    {
        _objects.Add(go);
        _instantiatedChar++;
        GameObject.DontDestroyOnLoad(go);
        int id = _charIdList[go.name][0];
        _charIdList[go.name].RemoveAt(0);

        AreaGroupController controller = null;
        controller = go.AddComponent<AreaNPCController>();

        ControllerSetting(go, controller, id);
        _charLists.Add(go);
        _controllers.Add(controller);

        //Debug.Log($"{go.name} Instantiated");
        _progress++;
        if (_charLists.Count == _keys.Count)
        {
            _progress++;
            //Debug.Log($"npc instantiated {_progress}");
        }
    }

    private void CameraInstantiate()
    {
        Managers.Resource.Instantiate("AreaCamera", CameraInstantiated);
    }

    private void CameraInstantiated(GameObject go)
    {
        _objects.Add(go);
        GameObject.DontDestroyOnLoad(go);

        _camera = go.GetComponent<AreaCameraController>();

        _progress++;
        //Debug.Log($"Camera Instantiated {_progress}");
        _cameraInit = true;
        SetPlayerAndCameraIfInitialized();
    }

    private void SetPlayerAndCameraIfInitialized()
    {
        if (BothInit == true)
        {
            _camera.SetTarget(_player.transform);
            _player.AreaCamera = _camera;
        }
    }

    public void AreaInit()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Village");

        int size = objects.Length;
        Debug.Log(size);
        for(int i = 0; i < size; i++)
        {
            VillageStatus status = objects[i].GetComponent<VillageStatus>();
            _villageLists.Add(status);
            status.SceneInit();
            _villages.Add(status.Data.VillageName, status);
        }

        GameObject[] roads = GameObject.FindGameObjectsWithTag("Road");

        size = roads.Length;
        for(int i = 0; i < size; i++)
        {
            _roads.Add(roads[i]);
        }
    }

    public void SceneInit()
    {
        _player.SceneInit();
        int size = _controllers.Count;
        for (int i = 0; i < size; i++)
            _controllers[i].SceneInit();

        size = _villageLists.Count;
        for (int i = 0; i < size; i++)
            _villageLists[i].SceneInit();

        AreaSceneNow = true;
    }
    #endregion

    public void Clear()
    {
        AreaSceneNow = false;
        _controllers.Clear();
        _villageLists.Clear();
        int size = _objects.Count;
        for (int i = 0; i < size; i++)
        {
            if (_objects[i] != null)
                Managers.Resource.Release(_objects[i]);
        }
        _objects.Clear();
        _charLists.Clear();

        _villages.Clear();
        _roads.Clear();
    }

    public void OnUpdate()
    {
        _time += Time.deltaTime;
        if(_time >= DayChangeTime)
        {
            _time = 0;
            _day++;
            Debug.Log($"Day {_day}");
            DayChangeUpdate(_day);
        }
    }

    public AreaNode GetClosestNode(Vector3 pos)
    {
        float min = Vector3.Distance(pos, _roads[0].transform.position);
        int size = _roads.Count;
        int number = 0;
        float distTemp = min;
        for(int i = 1; i < size; i++)
        {
            distTemp = Vector3.Distance(_roads[i].transform.position, pos);
            if(distTemp < min)
            {
                number = i;
                min = distTemp;
            }
        }

        return _roads[number].GetComponent<AreaNode>();
    }
}
