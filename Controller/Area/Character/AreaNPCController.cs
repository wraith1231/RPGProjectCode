using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaNPCController : AreaGroupController
{
    //mercenary

    protected override void Initialize()
    {
        _apperanceCheck.EnemyTag.Add("Monster");
        _apperanceCheck.MateTag.Add("AreaCharacter");
        _apperanceCheck.MateTag.Add("Village");
    }

    protected override void FixedUpdate()
    {
        if (_root != null && Status == Define.AreaStatus.Idle)
            _root.Evaluate();
    }

    protected override void DayChangeUpdate(int day)
    {
        base.DayChangeUpdate(day);

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
