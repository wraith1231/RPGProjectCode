using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaMonsterController : AreaGroupController
{
    public NodeBase _root;

    protected override void Initialize()
    {
        SequenceNode sequence = new SequenceNode();



        _root = sequence;
    }
    protected void OnCollisionEnter(Collision collision)
    {

    }
    protected void OnCollisionExit(Collision collision)
    {

    }

    public override void EnterVillage(List<Define.Facilities> facilities, GlobalVillageData village)
    {

    }

    public override void ExitViilage(GlobalVillageData village)
    {

    }

    protected override void DayChangeUpdate(int day)
    {

    }

    protected override void FixedUpdate()
    {

    }

}
