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
    
    void Start()
    {
        base.Init();
#if UNITY_EDITOR
        Cursor.lockState = CursorLockMode.None;
#else
        Cursor.lockState = CursorLockMode.Confined;
#endif

        Managers.Map.AreaInit();
        Managers.Map.SceneInit();

        Managers.UI.ShowSceneUI<UIAreaSceneInterface>();
        Managers.Resource.Instantiate("Popup/UIGroupName", GroupNamePopup);
        Managers.Battle.AreaTerrainData = Managers.Map.MapData;
        //Managers.UI.MakePopupUI<UIGroupName>(foo:GroupNamePopup);
    }

    private void GroupNamePopup(GameObject go)
    {
        Managers.Map.SetGroupPopup(go);
    }

    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.K))
        {
            Managers.Battle.AddCharList(Managers.General.GlobalPlayer.Data);
            for(int i =1; i < 3;i++)
                Managers.Battle.AddGroup(Managers.General.GlobalGroups[i]);
            Managers.Scene.LoadSceneAsync(Define.SceneType.TestScene);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.Scene.LoadSceneAsync(Define.SceneType.TitleScene);
        }
#endif
    }
}
