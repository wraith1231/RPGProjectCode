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

    private int _id = 0;

    private List<AreaCharController> _charLists = new List<AreaCharController>(); //��ü ĳ���� ������
    private List<GameObject> _objects = new List<GameObject>();  //release ��
    private Dictionary<string, List<int>> _charIdList = new Dictionary<string, List<int>>();

    private Terrain _areaTerrain;

    private static int PROGRESS = 4;
    private int _progress = 0;
    public float CurrentProgress { get { return (float)_progress / (float)PROGRESS; } }

    private int _instantiatedChar = 0;

    public void DataInstantiate()
    {
        _id = 0;
        _playerInit = false;
        _cameraInit = false;
        _progress = 0;
        _instantiatedChar = 0;

        Managers.Resource.Instantiate("AreaTerrain", TerrainInstantiated);
    }

    private void TerrainInstantiated(GameObject go)
    {
        _objects.Add(go);
        GameObject.DontDestroyOnLoad(go);

        _areaTerrain = go.GetComponent<Terrain>();
        _progress++;
        CharacterInstantiate();
        CameraInstantiate();
    }

    private void CharacterInstantiate()
    {
        //근데 이거 캐릭터 말고 카트로 바꿀 수 있음
        Managers.Resource.Instantiate("Human", PlayerInstantiated);
        NPCInstantiate();

    }

    private void PlayerInstantiated(GameObject go)
    {
        _objects.Add(go);
        GameObject.DontDestroyOnLoad(go);
        int id = _id++;

        AreaCharController controller = null;

        List<GlobalNPCController> data = Managers.General.GlobalCharacters;
        controller = go.AddComponent<AreaPlayerController>();
        _player = controller as AreaPlayerController;
        _playerInit = true;
        SetPlayerAndCameraIfInitialized();
        //go.AddComponent<UnityEngine.AI.NavMeshAgent>();

        controller.SetCharacterData(Managers.General.GlobalPlayer.Data);
        CharacterOutfit outfit = go.GetComponent<CharacterOutfit>();
        outfit.SetOutfit( controller.Data.Outfit);
        go.name = controller.Data.CharName;

        Vector3 pos = controller.Data.StartPosition;
        pos.y = _areaTerrain.terrainData.GetInterpolatedHeight(pos.x / _areaTerrain.terrainData.size.x, pos.z / _areaTerrain.terrainData.size.z);
        controller.transform.position = pos;
        _charLists.Add(controller);
        _progress++;
    }

    private void NPCInstantiate()
    {
        List<GlobalNPCController> data = Managers.General.GlobalCharacters;
        int listSize = data.Count;
        string key = "";
        for (int listNum = 0; listNum < listSize; listNum++)
        {
            switch (data[listNum].Data.Type)
            {
                case Define.CharacterType.Human:
                    key = "Human";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(listNum);
                    Managers.Resource.Instantiate("Human", HumanCharInstantiated);
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
                    Managers.Resource.Instantiate("Human", HumanCharInstantiated);
                    break;
                default:
                    key = "Human";
                    if (_charIdList.ContainsKey(key) == false)
                        _charIdList[key] = new List<int>();
                    _charIdList[key].Add(listNum);
                    Managers.Resource.Instantiate("Human", HumanCharInstantiated);
                    break;
            }
        }
    }

    private void HumanCharInstantiated(GameObject go)
    {
        _objects.Add(go);
        _instantiatedChar++;
        GameObject.DontDestroyOnLoad(go);
        int id = _charIdList[go.name][0];
        _charIdList[go.name].RemoveAt(0);

        AreaCharController controller = null;

        List<GlobalNPCController> data = Managers.General.GlobalCharacters;
        controller = go.AddComponent<AreaNPCController>();

        CharacterOutfit outfit = go.GetComponent<CharacterOutfit>();

        outfit.SetOutfit(data[id].Data.Outfit);
        go.name = data[id].Data.CharName;
        controller.SetCharacterData(data[id].Data);
        
        Vector3 pos = controller.Data.StartPosition;
        pos.y = _areaTerrain.terrainData.GetInterpolatedHeight(pos.x / _areaTerrain.terrainData.size.x, pos.z / _areaTerrain.terrainData.size.z);
        controller.transform.position = pos;
        _charLists.Add(controller);
        
        if (_charLists.Count == data.Count)
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

        _cameraInit = true;
        SetPlayerAndCameraIfInitialized();

        _camera = go.GetComponent<AreaCameraController>();
        _progress++;
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
