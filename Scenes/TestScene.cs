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

        Managers.Battle.BattleTerrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
        Managers.Battle.BattleSceneStart();
        //Managers.Battle.GroupInitialize();
        //Managers.Battle.LoadCharacterPrefab();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Managers.Scene.LoadSceneAsync(Define.SceneType.AreaScene);
        }

    }
}
