using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGameManager
{
    private AreaPlayerController _player;
    private AreaCameraController _camera;

    private List<AreaCharController> _charLists = new List<AreaCharController>(); //전체 캐릭터 관리용
    private List<GameObject> _objects = new List<GameObject>();  //release 용

    private Terrain _areaTerrain;

    public void DataInstantiate()
    {
        Managers.Resource.Instantiate("AreaTerrain", TerrainInstantiated);

        CharacterInstantiate();


    }

    private void TerrainInstantiated(GameObject go)
    {
        _objects.Add(go);
        GameObject.DontDestroyOnLoad(go);

        _areaTerrain = go.GetComponent<Terrain>();
    }

    private void CharacterInstantiate()
    {
        Managers.Resource.Instantiate("Human", PlayerInstantiated);
        NPCInstantiate();

    }

    private void PlayerInstantiated(GameObject go)
    {

    }

    private void NPCInstantiate()
    {
        List<GlobalNPCController> data = Managers.General.GlobalCharacters;
        int listSize = data.Count;
        for (int listNum = 0; listNum < listSize; listNum++)
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
    }
}
