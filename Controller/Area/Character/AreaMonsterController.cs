using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class AreaMonsterController : AreaGroupController
{
    protected GlobalVillageData _currentVillage;   

    protected override void Initialize()
    {
        SequenceNode mainSequence = new SequenceNode();
        mainSequence.Attach(new AreaPatrolSequence(this, 20, 2));
        mainSequence.Attach(new AreaCheckSurroundEnemy(this, new AreaChaseTarget(this)));

        _apperanceCheck.MateTag.Add("Monster");
        _apperanceCheck.EnemyTag.Add("AreaCharacter");
        _apperanceCheck.EnemyTag.Add("Village");

        SetAppearanceVisible(false);

        _root = mainSequence;
    }
    private void OnTriggerEnter(Collider other)
    {
        AreaGroupController group = other.transform.GetComponent<AreaGroupController>();

        if (group == null)
        {
            
            return;
        }

        
        if (group.GroupId == 0)
        {
            Managers.Battle.AddGroup(Managers.General.GlobalGroups[_groupId]);
            if (group.Status != Define.AreaStatus.Battle)
            {
                Managers.Battle.AddCharList(Managers.General.GlobalPlayer.Data);
                group.Status = Define.AreaStatus.Battle;
                Managers.Scene.LoadSceneAsync(Define.SceneType.TestScene);
            }
        }
        else if (group.CharacterType == Define.GroupType.Monster)
        {
            GlobalGroupController thisCon = Managers.General.GlobalGroups[_groupId];
            GlobalGroupController otherCon = Managers.General.GlobalGroups[group.GroupId];
            if(_groupId > group.GroupId)
            {
                List<int> list = thisCon.MemberList;
                while (list.Count > 0)
                {
                    otherCon.AddGroupMember(list[0]);
                    thisCon.RemoveMember(list[0]);
                }

                Managers.Map.ReleaseChar(this);
            }
            else
            {
                List<int> list = otherCon.MemberList;
                while(list.Count > 0)
                {
                    thisCon.AddGroupMember(list[0]);
                    otherCon.RemoveMember(list[0]);
                }

                Managers.Map.ReleaseChar(group);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        
    }


    public override void EnterVillage(List<Define.Facilities> facilities, GlobalVillageData village)
    {
        if(village.IsVillageConditionOK == true)
        {
            Status = Define.AreaStatus.Battle;
            _currentVillage = village;
        }
    }

    public override void ExitViilage(GlobalVillageData village)
    {
        //어차피 몬스터가 마을 나가는거면 죽은 상황 아닌가?
        //생각해보니 마을 파괴시킨 상황도 있네
    }

    protected override void DayChangeUpdate(int day)
    {
        base.DayChangeUpdate(day);


        if(Status == Define.AreaStatus.Battle)
        {


        }
    }

    protected override void FixedUpdate()
    {
        if (_root != null && Status == Define.AreaStatus.Idle)
            _root.Evaluate();
    }

}
