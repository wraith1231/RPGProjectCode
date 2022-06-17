using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaGroupController : MonoBehaviour
{
    protected class AStarNode
    {
        //total
        public float t;
        //todest
        public float d;
        //fromstart
        public float a;

        public bool isAlive = true;
        public AStarNode parent;
        public AreaNode currentNode;
    }

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
    protected Animator _animator;

    protected Transform _targetObject = null;
    protected float _moveSpeed = 10f;
    protected Queue<Vector3> _destination = new Queue<Vector3>();
    protected bool _moveToDest = false;
    protected bool _moveInterrupted = false;
    protected AreaNode _currentNode;
    protected Vector3 _currentDest = Vector3.zero;
    
    public bool DestinationClosed(Vector3 pos)
    {
        if(_targetObject != null)
        {
            if (_targetObject.position == pos)
                return true;

            return false;
        }

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
        _animator = GetComponent<Animator>();
        _status = Define.AreaStatus.Idle;
        _appearance = GetComponentInChildren<CartCheck>().gameObject;

        Managers.General.GlobalGroups[_groupId].AreaTransfrom = _transform;
    }

    public void SceneInit()
    {
        Initialize();

        Managers.Map.DayChangeUpdate -= DayChangeUpdate;
        Managers.Map.DayChangeUpdate += DayChangeUpdate;

        GlobalGroupController controller = Managers.General.GlobalGroups[_groupId];

        int targetId = controller.MoveTarget;
        if(targetId >= 0)
        {
            switch(controller.TargetType)
            {
                case "AreaCharacter":
                    _targetObject = Managers.General.GlobalGroups[targetId].AreaTransfrom;
                    break;
                case "Monster":
                    _targetObject = Managers.General.GlobalGroups[targetId].AreaTransfrom;
                    break;
                case "Village":
                    _targetObject = Managers.General.GlobalVillages[targetId].Data.AreaTransfrom;
                    break;
            }

            StartCoroutine(MoveToTarget());
            return;
        }

        Queue<Vector3> dest = controller.Destination;

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

    #region Move Sequence

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
                if(_targetObject == null)
                    _currentDest = _destination.Dequeue();
                
                _moveInterrupted = false;
            }   

            if(_targetObject != null)
            {
                transformPos = new Vector2(_transform.position.x, _transform.position.z);
                destPos = new Vector2(_targetObject.position.x, _targetObject.position.z);

                dist = Vector2.Distance(transformPos, destPos);

                if (dist <= 0.1f)
                {
                    break;
                }
                MoveToward(_targetObject.position);
                yield return null;
                continue;
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
        _targetObject = null;
    }
    #endregion

    private void OnDestroy()
    {
        Managers.Map.DayChangeUpdate -= DayChangeUpdate;

        GlobalGroupController controller = Managers.General.GlobalGroups[_groupId];

        controller.Position = _transform.position;

        controller.Status = _status;

        if(_targetObject != null)
        {
            controller.TargetType = _targetObject.tag;
            //AreaCharacter Monster Village
            switch (_targetObject.tag)
            {
                case "AreaCharacter":
                    controller.MoveTarget = _transform.GetComponent<AreaGroupController>().GroupId;
                    break;
                case "Monster":
                    controller.MoveTarget = _transform.GetComponent<AreaGroupController>().GroupId;

                    break;
                case "Village":
                    controller.MoveTarget = _transform.GetComponent<VillageStatus>().Data.VillageId;
                    break;
            }

            return;
        }

        controller.MoveTarget = -1;
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
    //128 20 19

    #region Village
    public virtual void EnterVillage(List<Define.Facilities> facilities, GlobalVillageData village)
    {
        _transform.position = village.AreaTransfrom.position;
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

    public virtual void ExitViilage(GlobalVillageData village)
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
    #endregion


    public void FindPathToVillage(string villName)
    {
        float dest = Vector3.Distance(_transform.position, Managers.Map.Villages[villName].transform.position);

        AStarNode temp = new AStarNode();
        temp.t = dest;
        temp.a = 0;
        temp.d = dest;
        if(_currentNode != null)
            temp.currentNode = _currentNode;
        else
        {
            temp.currentNode = new AreaNode();
            temp.currentNode.ConnectedNode.Add(Managers.Map.GetClosestNode(_transform.position));
        }
        temp.parent = null;

        List<AStarNode> queue = new List<AStarNode>();
        int size = _currentNode.ConnectedNode.Count;
        for (int i = 0; i < size; i++)
        {
            AStarNode qTemp = new AStarNode();
            qTemp.parent = temp;
            qTemp.currentNode = qTemp.parent.currentNode.ConnectedNode[i];
            qTemp.a = qTemp.parent.a + Vector3.Distance(qTemp.parent.currentNode.transform.position, qTemp.currentNode.transform.position);
            qTemp.d = Vector3.Distance(qTemp.currentNode.transform.position, Managers.Map.Villages[villName].transform.position);
            qTemp.t = qTemp.a + qTemp.d;

            queue.Add(qTemp);
        }

        int listSize = 0;
        int number = 0;
        bool isComputed = false;
        AStarNode result = null;
        int queueCount = 0;
        while (true)
        {
            queueCount = queue.Count;
            if (number >= queueCount)
                return;
            size = queue[number].currentNode.ConnectedNode.Count;

            for (int i = 0; i < size; i++)
            {
                AStarNode qTemp = new AStarNode();
                qTemp.parent = queue[number];
                qTemp.currentNode = qTemp.parent.currentNode.ConnectedNode[i];
                listSize = queue.Count;
                isComputed = false;
                for (int j = 0; j < listSize; j++)
                {
                    if (qTemp.currentNode == queue[j].currentNode)
                    {
                        queue[j].isAlive = false;
                        isComputed = true;
                        break;
                    }
                }
                if (isComputed == true)
                    continue;

                qTemp.a = qTemp.parent.a + Vector3.Distance(qTemp.parent.currentNode.transform.position, qTemp.currentNode.transform.position);
                qTemp.d = Vector3.Distance(qTemp.currentNode.transform.position, Managers.Map.Villages[villName].transform.position);
                qTemp.t = qTemp.a + qTemp.d;

                if (qTemp.currentNode.Village != null)
                {
                    if (qTemp.currentNode.Village.Data.VillageName == villName)
                    {
                        result = qTemp;
                        break;
                    }
                }

                queue.Add(qTemp);
            }

            if (result != null)
            {
                Stack<AStarNode> stack = new Stack<AStarNode>();

                while (result.parent != null)
                {
                    stack.Push(result);
                    result = result.parent;
                }

                while (stack.Count > 0)
                {
                    _destination.Enqueue(stack.Pop().currentNode.transform.position);
                }

                Status = Define.AreaStatus.Move;

                break;
            }
            number++;
        }

    }


    #region General Public Function Zone
    public void SetAnimatorVertical(float vertical)
    {
        _animator.SetFloat("Vertical", vertical);
    }
    public void SetAnimatorHorizontal(float horizontal)
    {
        _animator.SetFloat("Horizontal", horizontal);
    }
    public void SetAnimatorDirection(float vertical, float horizontal)
    {
        SetAnimatorVertical(vertical);
        SetAnimatorHorizontal(horizontal);
    }

    public void HeadToDestination(Vector2 destination)
    {
        _transform.LookAt(new Vector3(destination.x, _transform.position.y, destination.y));
    }
    public void HeadToDestination(Vector3 destination)
    {
        _transform.LookAt(destination);
    }
    public void HeadToDestination(Transform transform)
    {
        _transform.LookAt(transform);
    }

    public Vector2 GetPositionByVector2()
    {
        return new Vector2(_transform.position.x, _transform.position.z);
    }
    public Vector3 GetPositionByVector3()
    {
        return _transform.position;
    }
    public float GetDistanceWithVector2(Vector2 dest)
    {
        Vector2 temp = new Vector2(_transform.position.x, _transform.position.z);

        return Vector2.Distance(temp, dest);
    }
    public float GetDistanceWithVector3(Vector3 dest)
    {
        return Vector3.Distance(_transform.position, dest);
    }
    public float GetDistanceVector2WithVector3(Vector3 dest)
    {
        Vector2 temp1 = new Vector2(_transform.position.x, _transform.position.z);
        Vector2 temp2 = new Vector2(dest.x, dest.z);

        return Vector2.Distance(temp1, temp2);
    }
    public float GetDistanceTarget(Transform target)
    {
        return Vector3.Distance(_transform.position, target.position);
    }

    #endregion

    #region Move Function
    public void MoveToDestination(Vector2 dest)
    {
        _destination.Enqueue(new Vector3(dest.x, 0, dest.y));
    }

    #endregion
}
