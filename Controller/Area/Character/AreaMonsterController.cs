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
    private void OnTriggerEnter(Collider other)
    {
        AreaGroupController group = other.transform.GetComponent<AreaGroupController>();

        if (group == null)
        {

            return;
        }

        if (other.transform.tag == "Monster")
        {

            return;
        }

        if (group.GroupId == 0)
        {
            if (group.Status != Define.AreaStatus.Battle)
            {
                group.Status = Define.AreaStatus.Battle;
                Managers.Battle.AddCharList(Managers.General.GlobalPlayer.Data);
                Managers.Battle.AddGroup(Managers.General.GlobalGroups[_groupId]);
                Managers.Scene.LoadSceneAsync(Define.SceneType.TestScene);
            }
        }
    }
    private void OnTriggerExit(Collider other)
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
