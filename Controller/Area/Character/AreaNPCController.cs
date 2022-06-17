using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaNPCController : AreaGroupController
{
    
    protected override void FixedUpdate()
    {

    }

    protected override void Initialize()
    {
        int size = Managers.Map.VillageLists.Count;
        int start = Random.Range(0, size);


    }

    protected override void DayChangeUpdate(int day)
    {
        base.DayChangeUpdate(day);

        if (_moveToDest == false)
            TestRandomMove();
    }

    protected void TestRandomMove()
    {
        int size = Managers.Map.VillageLists.Count;
        int num = Random.Range(0, size);
        string villName = Managers.Map.VillageLists[num].Data.VillageName;

        FindPathToVillage(villName);    

        if(_destination.Count > 0)
            StartCoroutine(MoveToTarget());
    }

}
