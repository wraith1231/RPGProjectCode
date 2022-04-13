using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaGroupController : MonoBehaviour
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
    private List<int> _memberList = new List<int>();

    #region Group Member Management
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
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    protected abstract void FixedUpdate();
}
