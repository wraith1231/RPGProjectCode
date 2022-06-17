using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGroupController
{
    //그룹 분류, 이후 battlescene 같은데서도 써먹음
    private int _group;
    public int Group { get { return _group; } set { _group = value; } }
    private string _groupName;
    public string GroupName { get { return _groupName; } set { _groupName = value; } }

    private int _currentVillageNumber = -1;
    public int CurrentVillageNumber { get { return _currentVillageNumber; } set { _currentVillageNumber = value; } }

    //퀘스트 목표물인지
    private bool _questObjective = false;
    public bool QuestObjective { get { return _questObjective; } set { _questObjective = value; } }

    //그룹 멤버 관련
    [SerializeField]
    private List<int> _memberList = new List<int>();
    public List<int> MemberList { get { return _memberList; } }
    public int GroupMemberCount() { return _memberList.Count; }

    //돈이 아니라 식량으로 쓴다
    private float _foods = 0f;
    public float Foods { get { return _foods; } set { _foods = value; } }

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
    public int Gold { get { return _gold; } set { _gold = value; } }

    public GlobalGroupController(int groupId, string groupName, float foods, int golds, Define.GroupType type)
    {
        _group = groupId;
        _groupName = groupName;
        _foods = foods;
        _gold = golds;
        _type = type;
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
            _memberList.Add(memberList[i]);
    }
    public void AddGroupMember(int id)
    {
        if (Managers.General.ConstainsId(id) == true)
        {
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


}
