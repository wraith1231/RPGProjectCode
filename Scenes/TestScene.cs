using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    public override void Clear()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Managers.Battle.Clear();
    }

    public override void SceneInitialize()
    {
    }

    void Awake()
    {
        base.Init();

        Managers.UI.MakePopupUI<UIPlayerGauge>(foo: PlayerGaugeInstantiated);

        Managers.Battle.BattleSceneStart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Managers.Scene.LoadSceneAsync(Define.SceneType.AreaScene);
        }

    }

    private void PlayerGaugeInstantiated(GameObject go)
    {
        UIPlayerGauge gauge = go.GetComponent<UIPlayerGauge>();

        gauge.SetPlayer(Managers.Battle.Player);
    }
}
