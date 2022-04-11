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

    private Terrain _areaTerrain;

    public void DataInstantiate()
    {
        _id = 0;
        _playerInit = false;
        _cameraInit = false;
        Managers.Resource.Instantiate("AreaTerrain", TerrainInstantiated);

    }

    private void TerrainInstantiated(GameObject go)
    {
        _objects.Add(go);
        GameObject.DontDestroyOnLoad(go);

        _areaTerrain = go.GetComponent<Terrain>();

        CharacterInstantiate();
        CameraInstantiate();
    }

    private void CharacterInstantiate()
    {
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
        go.AddComponent<UnityEngine.AI.NavMeshAgent>();

        CharacterOutfit outfit = go.GetComponent<CharacterOutfit>();

        outfit.SetOutfit(data[id].Data.Outfit);
        go.name = data[id].Data.CharName;
        data[id].Data.HeroId = id;
        controller.SetCharacterData(data[id].Data);

        controller.transform.position = controller.Data.StartPosition;

    }

    private void NPCInstantiate()
    {
        List<GlobalNPCController> data = Managers.General.GlobalCharacters;
        int listSize = data.Count;
        for (int listNum = 1; listNum < listSize; listNum++)
        {
            switch (data[listNum].Data.Type)
            {
                case Define.CharacterType.Human:
                    Managers.Resource.Instantiate("Human", HumanCharInstantiated);
                    break;
                case Define.CharacterType.Animal:
                    break;
                case Define.CharacterType.Monster:
                    break;
                case Define.CharacterType.Unknown:
                    Managers.Resource.Instantiate("Human", HumanCharInstantiated);
                    break;
                default:
                    Managers.Resource.Instantiate("Human", HumanCharInstantiated);
                    break;
            }
        }
    }

    private void HumanCharInstantiated(GameObject go)
    {
        _objects.Add(go);
        GameObject.DontDestroyOnLoad(go);
        int id = _id++;

        AreaCharController controller = null;

        List<GlobalNPCController> data = Managers.General.GlobalCharacters;
        controller = go.AddComponent<AreaNPCController>();
        go.AddComponent<UnityEngine.AI.NavMeshAgent>();

        CharacterOutfit outfit = go.GetComponent<CharacterOutfit>();

        outfit.SetOutfit(data[id].Data.Outfit);
        go.name = data[id].Data.CharName;
        data[id].Data.HeroId = id;
        controller.SetCharacterData(data[id].Data);

        Vector3 pos = controller.Data.StartPosition;
        pos.y = _areaTerrain.terrainData.GetInterpolatedHeight(pos.x, pos.z);
        controller.transform.position = pos;
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
