using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScene : BaseScene
{

    public override void Clear()
    {
        Managers.Map.Clear();
    }
    
    void Awake()
    {
        base.Init();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Managers.Battle.AddCharList(Managers.General.GlobalPlayer.Data);
            Managers.Scene.LoadSceneAsync(Define.SceneType.TestScene);
        }
    }
}
