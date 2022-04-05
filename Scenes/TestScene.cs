using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    public override void Clear()
    {

    }

    public override void SceneInitialize()
    {
    }

    void Awake()
    {
        base.Init();

        Managers.Battle.BattleTerrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
        Managers.Battle.GroupInitialize();
        Managers.Battle.LoadCharacterPrefab();
    }
}
