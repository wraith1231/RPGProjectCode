using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    public override void Clear()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        base.Init();

        //Managers.Resource.Load<GameObject>("Human", HumanResourcePooling);
    }

    private void HumanResourcePooling(GameObject go)
    {
        Managers.Pool.CreatePool(go, 100);
    }
}
