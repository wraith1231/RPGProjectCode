using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScene : BaseScene
{
    [SerializeField] private List<VillageStatus> _villageStatus;

    public override void Clear()
    {
        Managers.Map.Clear();
    }
    
    void Awake()
    {
        base.Init();

        Managers.Map.AreaInit();
        Managers.Map.SceneInit();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Managers.Battle.AreaTerrainData = Managers.Map.MapData;
            Managers.Battle.AddCharList(Managers.General.GlobalPlayer.Data);
            Managers.Battle.AddCharList(Managers.General.GlobalCharacters[3].Data);
            Managers.Scene.LoadSceneAsync(Define.SceneType.TestScene);
        }
    }
}
