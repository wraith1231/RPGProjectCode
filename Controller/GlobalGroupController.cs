using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGroupController
{
    //�׷� �з�, ���� battlescene ���������� �����
    private int _group;
    public int Group { get { return _group; } set { _group = value; } }
    private string _groupName;
    public string GroupName { get { return _groupName; } set { _groupName = value; } }

    private int _currentVillageNumber = -1;
    public int CurrentVillageNumber { get { return _currentVillageNumber; } set { _currentVillageNumber = value; } }

    //����Ʈ ��ǥ������
    private bool _questObjective = false;
    public bool QuestObjective { get { return _questObjective; } set { _questObjective = value; } }
    private int _relatedQuest;
    public int RelatedQuest { get { return _relatedQuest; } set { _relatedQuest = value; } }
    private QuestBase _currentQuest = null;
    public QuestBase CurrentQuest { get { return _currentQuest; } set { _currentQuest = value; } }

    //�׷� ��� ����
    [SerializeField]
    private List<int> _memberList = new List<int>();
    public List<int> MemberList { get { return _memberList; } }
    public int GroupMemberCount() { return _memberList.Count; }

    //���� �ƴ϶� �ķ����� ����
    private float _foods = 0f;
    public float Foods { get { return _foods; } set { _foods = value; if(CallFoodChange != null) CallFoodChange(_foods); } }

    private Define.GroupType _type = Define.GroupType.Unknown;
    public Define.GroupType Type { get { return _type; } set { _type = value; } }

    //area move
    private Queue<Vector3> _destination = new Queue<Vector3>();
    public Queue<Vector3> Destination { get { return _destination; } set { _destination = value; } }

    private string _targetType;
    public string TargetType { get { return _targetType; } set { _targetType = value; } }
    private int _target = -1;
    public int MoveTarget { get { return _target; } set { _target = value; } }

    private Transform _areaTranfrom;
    public Transform AreaTransfrom { get { return _areaTranfrom; } set { _areaTranfrom = value; } }

    private Define.AreaStatus _status = Define.AreaStatus.Idle;
    public Define.AreaStatus Status { get { return _status; } set { _status = value; } }

    private Vector3 _position = new Vector3();
    public Vector3 Position { get { return _position; } set { _position = value; } }

    private int _gold;
    public int Gold { get { return _gold; } set { _gold = value; if(CallMoneyChange != null) CallMoneyChange(_gold); } }

    private float _goodFame;
    private float _badFame;
    private float _performance;
    public float Performance { get { return _performance; } set { _performance = value; } }

    public delegate void MoneyChange(int val);
    public MoneyChange CallMoneyChange;
    public delegate void FoodChange(float val);
    public FoodChange CallFoodChange;

    public float GroupPower
    {
        get
        {
            List<GlobalCharacterController> members = Managers.General.GlobalCharacters;
            int size = _memberList.Count;
            float result = 0;
            for(int i =0; i < size;i ++)
            {
                result += members[_memberList[i]].BattleData.CurrentPower;
            }

            return result;
        }
    }

    public GlobalGroupController(int groupId, string groupName, float foods, int golds, Define.GroupType type)
    {
        _group = groupId;
        _groupName = groupName;
        _foods = foods;
        _gold = golds;
        _type = type;
        _goodFame = 0;
        _badFame = 0;
        _performance = 0;
    }

    #region Group Member Management
    public void SetGroupMembers(List<int> memberList)
    {
        if (_memberList != null)
            _memberList.Clear();
        else
            _memberList = new List<int>();
        int size = memberList.Count;
        for (int i = 0; i < size; i++)
        {
            _memberList.Add(memberList[i]);
        }
    }
    public void AddGroupMember(int id)
    {
        if (Managers.General.ConstainsId(id) == true)
        {
            GlobalCharacterController cont = Managers.General.GlobalCharacters[id];
            cont.CurrentGroup = _group;
            _memberList.Add(id);
        }
    }
    public void RemoveMember(int id)
    {
        if (_memberList.Contains(id) == true)
        {
            _memberList.Remove(id);
        }
    }
    #endregion

    public void GroupBattleCharacterGroup(GlobalGroupController cont)
    {

    }

    public void GroupBattleVillage(GlobalVillageData data)
    {

    }

    public void InnRest()
    {
        int size = _memberList.Count;
        for(int  i=0; i < size; i++)
        {
            Managers.General.GlobalCharacters[_memberList[i]].InnRest();
        }
    }

    public void AddGoodFame(float value)
    {
        _goodFame += value;
        float point = value * 0.1f;
        int size = _memberList.Count;
        for (int i = 0; i < size; i++)
        {
            Managers.General.GlobalCharacters[_memberList[i]].GoodFame += point;
        }
    }
    public void AddBadFame(float value)
    {
        _badFame += value;
        float point = value * 0.1f;
        int size = _memberList.Count;
        for (int i = 0; i < size; i++)
        {
            Managers.General.GlobalCharacters[_memberList[i]].BadFame += point;
        }
    }
    public void AddPerformance(float value)
    {
        _performance += value;
    }
}
