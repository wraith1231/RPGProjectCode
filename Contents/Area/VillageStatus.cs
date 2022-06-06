using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageStatus : MonoBehaviour
{
    [SerializeField] private string _name;
    public string Name { get { return _name; } }
    [SerializeField] private float _detectRadius = 50f;

    private GlobalVillageData _data;
    public GlobalVillageData Data { get { return _data; } set { _data = value; } }

    [SerializeField]
    private AreaNode _baseAreaNode;
    public void SetBaseAreaNode(AreaNode node) { _baseAreaNode = node; }
    public AreaNode BaseAreaNode { get { return _baseAreaNode; } }

    private Transform _transform;


    private void OnDestroy()
    {
        Managers.Map.DayChangeUpdate -= DayChangeUpdate;
    }
    //루트를 미리 연산을 할까 말까

    public void SceneInit()
    {
        Managers.Map.DayChangeUpdate -= DayChangeUpdate;
        Managers.Map.DayChangeUpdate += DayChangeUpdate;

        _transform = GetComponent<Transform>();

        _data = Managers.General.GetVillageData(_name);
        _detectRadius = _data.DetectRange;

        Debug.Log($"{_data.VillageName} scene init");
    }

    private void OnTriggerEnter(Collider other)
    {
        AreaGroupController controller = other.GetComponent<AreaGroupController>();

        if (controller == null)
            return;

        if (_data.Condition == Define.VillageCondition.Destroyed)
            return;

        if (controller.DestinationClosed(_transform.position) == false)
            return;

        controller.SetAppearanceVisible(false);
        controller.CurrentNode = _baseAreaNode;
        Managers.General.GlobalGroups[controller.GroupId].CurrentVillageNumber = _data.VillageId;
        
        if(other.GetComponent<AreaPlayerController>() != null)
        {
            if (_data.Condition == Define.VillageCondition.Battle)
            {
                
            }
            else
            {
                Managers.UI.MakePopupUI<UIVillageInterface>();

                controller.EnterVillage(_data.FacilityLists, _data);
            }
        }
        else
        {
            if(_data.Condition == Define.VillageCondition.Battle)
            {

            }
            else
            {
                controller.EnterVillage(_data.FacilityLists, _data);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AreaGroupController controller = other.GetComponent<AreaGroupController>();
        if (controller == null)
            return;

        controller.SetAppearanceVisible(true);
        controller.CurrentNode = null;

        Managers.General.GlobalGroups[controller.GroupId].CurrentVillageNumber = -1;

        if (other.GetComponent<AreaPlayerController>() != null)
        {
            Managers.UI.CloseAllPopup();

            controller.ExitViilage(_data);
        }
        else
        {
            controller.ExitViilage(_data);
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void DayChangeUpdate(int day)
    {
        if (_data.Condition == Define.VillageCondition.Battle)
            return;

        _data.VillageDayChange();

        Collider[] colliders = Physics.OverlapSphere(_transform.position, _detectRadius);
        int size = colliders.Length;
        for(int i = 0;  i < size; i++)
        {
            if(colliders[i].CompareTag("Monster"))
            {
                AreaGroupController controller = colliders[i].GetComponent<AreaGroupController>();
                if (Managers.General.GlobalGroups[controller.GroupId].QuestObjective == true)
                    continue;

                RaidQuest raid = new RaidQuest();
                raid.Target = controller.GroupId;
                Managers.General.GlobalGroups[controller.GroupId].QuestObjective = true;
                raid.OrderedVillage = _data;
                Managers.Quest.AddQuest(_data.VillageName, raid);
            }
            else if(colliders[i].CompareTag("Camp"))
            {
                AttackCampQuest attack = new AttackCampQuest();
                attack.OrderedVillage = _data;

                Managers.Quest.AddQuest(_data.VillageName, attack);
            }
        }

    }
}
