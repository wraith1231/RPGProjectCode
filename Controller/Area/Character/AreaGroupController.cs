using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaGroupController : MonoBehaviour
{
    //�׷� �з�, ���� battlescene ���������� �����
    private int _group;
    public int Group { get { return _group; } set { _group = value; } }
    private string _groupName;
    public string GroupName { get { return _groupName; } set { _groupName = value; } }

    //ĳ���� ������, ���� �ܺο� ������ ����
    protected GameObject _appearance;

    //�̵� ����
    Transform _transform;

    private Vector3 _destination;
    private bool _moveToDest = false;

    //�׷� ��� ����
    [SerializeField]
    private List<int> _memberList = null;

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

    public void SetAppearance(GameObject go)
    {
        _appearance = go;
    }

    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    protected virtual void Initialize()
    {

    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {

    }
}
