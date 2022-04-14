using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGameManager
{
    private AreaPlayerController _player;
    private AreaCameraController _camera;
    private bool _playerInit = false;
    private bool _cameraInit = false;
    public bool BothInit { get { return _playerInit == true && _cameraInit == true; } }

    private List<GameObject> _charLists = new List<GameObject>(); //��ü ĳ���� ������
    private List<GameObject> _objects = new List<GameObject>();  //release ��
    private Dictionary<string, List<int>> _charIdList = new Dictionary<string, List<int>>();

    private Terrain _areaTerrain;

    private static int PROGRESS = 4;
    private int _progress = 0;
    public float CurrentProgress { get { return (float)_progress / (float)PROGRESS; } }

    private int _instantiatedChar = 0;

    //둘이 한 세트
    private Dictionary<int, List<int>> _groups;
    private List<int> _keys;

    public void DataInstantiate()
    {
        _playerInit = false;
        _cameraInit = false;
        _progress = 0;
        _instantiatedChar = 0;
        Debug.Log("Data Instantiate");

        Managers.Resource.Instantiate("AreaTerrain", TerrainInstantiated);
    }
    public void GroupInitialize()
    {
        _groups = new Dictionary<int, List<int>>();
        _keys = new List<int>();

        _groups[0] = new List<int>();
        _keys.Add(0);
        _groups[0].Add(Managers.General.GlobalPlayer.Data.HeroId);

        List<GlobalNPCController> data = Managers.General.GlobalCharacters;

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
        Debug.Log("Terrain Instantiated");
        CharacterInstantiate();
        CameraInstantiate();
    }

    private void CharacterInstantiate()
    {
        //근데 이거 캐릭터 말고 카트로 바꿀 수 있음
        Managers.Resource.Instantiate("AreaCart", PlayerInstantiated);
        NPCInstantiate();

    }

    //수정할때 HumanCharInstantiated 확인
    private void PlayerInstantiated(GameObject go)
    {
        _objects.Add(go);
        GameObject.DontDestroyOnLoad(go);

        _player = go.AddComponent<AreaPlayerController>();
        _player.SetGroupMembers(_groups[0]);
        _player.SetAppearance(go);

        go.name = Managers.General.GlobalPlayer.Data.CharName;
        Vector3 pos = Managers.General.GlobalPlayer.Data.StartPosition;
        pos.y = _areaTerrain.terrainData.GetInterpolatedHeight(pos.x / _areaTerrain.terrainData.size.x, pos.z / _areaTerrain.terrainData.size.z);

        _player.transform.position = pos;

        _progress++;
        Debug.Log("Player Instantiated");
        _playerInit = true;
        SetPlayerAndCameraIfInitialized();
    }


    private void NPCInstantiate()
    {
        
        List<GlobalNPCController> data = Managers.General.GlobalCharacters;
        int listSize = _keys.Count;
        string key = "";

        for (int listNum = 1; listNum < listSize; listNum++)
        {
            int mainCharId = _groups[_keys[listNum]][0];
            Debug.Log($"{_keys[listNum]} group  title {mainCharId}");
            switch (data[mainCharId].Data.Type)
            {
                case Define.CharacterType.Human:
                    key = "AreaCart";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(_keys[listNum]);
                    Managers.Resource.Instantiate("AreaCart", HumanCharInstantiated);
                    break;
                case Define.CharacterType.Animal:
                    break;
                case Define.CharacterType.Monster:
                    break;
                case Define.CharacterType.Unknown:
                    key = "AreaCart";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(listNum);
                    Managers.Resource.Instantiate("AreaCart", HumanCharInstantiated);
                    break;
                default:
                    key = "Cart";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(listNum);
                    Managers.Resource.Instantiate("AreaCart", HumanCharInstantiated);
                    break;
            }
        }
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
        controller = go.AddComponent<AreaGroupController>();

        controller.SetGroupMembers(_groups[id]);
        controller.SetAppearance(go);

        go.name = Managers.General.GlobalCharacters[_groups[id][0]].Data.CharName;
        Vector3 pos = Managers.General.GlobalCharacters[_groups[id][0]].Data.StartPosition;
        pos.y = _areaTerrain.terrainData.GetInterpolatedHeight(pos.x / _areaTerrain.terrainData.size.x, pos.z / _areaTerrain.terrainData.size.z);

        controller.transform.position = pos;
        _charLists.Add(go);

        Debug.Log($"{go.name} Instantiated");
        _progress++;
        if (_charLists.Count == _keys.Count)
            _progress++;
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
        Debug.Log("Camera Instantiated");
        _cameraInit = true;
        SetPlayerAndCameraIfInitialized();
    }

    private void SetPlayerAndCameraIfInitialized()
    {
        if (BothInit == true)
        {
            _camera.SetTarget(_player.transform);
            _player.Camera = _camera;
        }
    }
    public void Clear()
    {
        int size = _objects.Count;
        for (int i = 0; i < size; i++)
        {
            if (_objects[i] != null)
                Managers.Resource.Release(_objects[i]);
        }
        _objects.Clear();
        _charLists.Clear();
    }
}
