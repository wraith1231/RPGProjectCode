using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaGroupController : MonoBehaviour
{
    protected int _groupId;
    public int GroupId { get { return _groupId; } set { _groupId = value; } }

    protected Define.AreaStatus _status = Define.AreaStatus.Unknown;
    public virtual Define.AreaStatus Status { get { return _status; } set { _status = value; } }

    protected TerrainData _terrainData;
    public void SetTerrainData(TerrainData data) { _terrainData = data; }

    //캐릭터 외형용, 마을 외부에 있으면 끈다
    protected GameObject _appearance;

    //이동 관련
    protected Transform _transform;
    protected Rigidbody _rigidBody;

    protected float _moveSpeed = 10f;
    protected Queue<Vector3> _destination = new Queue<Vector3>();
    protected bool _moveToDest = false;
    protected bool _moveInterrupted = false;
    protected AreaNode _currentNode;
    protected Vector3 _currentDest = Vector3.zero;
    
    public bool DestinationClosed(Vector3 pos)
    {
        if (_destination.Count > 1)
            return false;

        if (_currentDest == Vector3.zero)
            return true;

        float dist = Vector3.Distance(_currentDest, pos);
        if (dist < 8f)
            return true;

        return false;
    }

    public AreaNode CurrentNode { get { return _currentNode; } set { _currentNode = value; } }
    protected Vector3 _prevNode;
    public Vector3 PrevNode { get { return _prevNode; } set { _prevNode = value; } }

    public void SetAppearanceVisible(bool visible)
    {
        _appearance.SetActive(visible);
    }

    void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidBody = GetComponent<Rigidbody>();
        _status = Define.AreaStatus.Idle;
        _appearance = GetComponentInChildren<CartCheck>().gameObject;
    }

    public void SceneInit()
    {
        Initialize();

        Managers.Map.DayChangeUpdate -= DayChangeUpdate;
        Managers.Map.DayChangeUpdate += DayChangeUpdate;

        Queue<Vector3> dest = Managers.General.GlobalGroups[_groupId].Destination;
        if(dest.Count > 0)
        {
            int size = dest.Count;
            for (int i = 0; i < size; i++)
                _destination.Enqueue(dest.Dequeue());
            //_destination = dest;

            StartCoroutine(MoveToTarget());
        }
        else
        {
            _transform.position = _prevNode;
        }
    }

    protected abstract void Initialize();

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void DayChangeUpdate(int day)
    {

    }

    protected virtual void BeforeMove()
    {
        Status = Define.AreaStatus.Move;
        _moveToDest = true;
        _moveInterrupted = false;
    }

    //현재 위치 기준
    protected void MoveToward(Vector3 pos)
    {
        Vector3 dir = pos - _transform.position;
        dir.y = 0;
        dir.Normalize();

        _transform.LookAt(_transform.position + dir);

        float speed = Time.deltaTime * _moveSpeed;
        _transform.Translate(Vector3.forward * speed);
        float height = _terrainData.GetInterpolatedHeight(_transform.position.x / _terrainData.size.x, _transform.position.z / _terrainData.size.z);
        _transform.Translate(Vector3.up * (height - _transform.position.y));
    }

    protected IEnumerator MoveToTarget()
    {
        BeforeMove();

        float dist;
        _currentDest = _destination.Dequeue();
        _transform.LookAt(_currentDest);
        Vector2 transformPos, destPos;

        while(true)
        {
            if(_moveInterrupted == true)
            {
                _currentDest = _destination.Dequeue();
                _moveInterrupted = false;
            }    
            transformPos = new Vector2(_transform.position.x, _transform.position.z);
            destPos = new Vector2(_currentDest.x, _currentDest.z);

            dist = Vector2.Distance(transformPos, destPos);

            if (dist <= 0.1f)
            {
                if(_destination.Count == 0)
                {
                    break;
                }

                _currentDest = _destination.Dequeue();
                _prevNode = _currentDest;
            }
            MoveToward(_currentDest);

            yield return null;
        }
        AfterMove();
    }

    protected virtual void AfterMove()
    {
        _destination.Clear();
        Status = Define.AreaStatus.Idle;
        _moveToDest = false;
        _moveInterrupted = false;
    }

    private void OnDestroy()
    {
        Managers.Map.DayChangeUpdate -= DayChangeUpdate;

        GlobalGroupController controller = Managers.General.GlobalGroups[_groupId];

        controller.Position = _transform.position;

        controller.Status = _status;

        if (_destination.Count > 0)
        {
            controller.Destination.Enqueue(_prevNode);

            int size = _destination.Count;
            for (int i = 0; i < size; i++)
                controller.Destination.Enqueue(_destination.Dequeue());
            //controller.Destination = _destination;
        }
        else
        {
            controller.Destination.Enqueue(_prevNode);
        }
    }

    public void EnterVillage(List<Define.Facilities> facilities, GlobalVillageData village)
    {
        List<int> memberList = Managers.General.GlobalGroups[_groupId].MemberList;
        int size = memberList.Count;
        for(int i = 0; i < size; i++)
        {
            GlobalCharacterController controller = Managers.General.GlobalCharacters[memberList[i]];
            if (controller.Data.Player == true) continue;

            int num = Random.Range(0, facilities.Count);
            controller.Data.CurrentFacillity = facilities[num];
            village.Facility[facilities[num]].Add(controller.Data.HeroId);
            //if(village.VillageId == 0)
            //    Debug.Log($"{controller.Data.CharName} enter {village.VillageName} {num}");
        }
    }

    public void ExitViilage(GlobalVillageData village)
    {
        List<int> memberList = Managers.General.GlobalGroups[_groupId].MemberList;
        int size = memberList.Count;
        for(int i = 0; i < size; i++)
        {
            GlobalCharacterController controller = Managers.General.GlobalCharacters[memberList[i]];
            if (controller.Data.Player == true) continue;
            if (controller.Data.CurrentFacillity == Define.Facilities.Unknown) continue;

            village.Facility[controller.Data.CurrentFacillity].Remove(controller.Data.HeroId);
            controller.Data.CurrentFacillity = Define.Facilities.Unknown;
        }
    }
}
