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
    public void SetAppearance(GameObject go)
    {
        _appearance = go;
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
        Vector3 currentDest = _destination.Dequeue();
        _transform.LookAt(currentDest);
        Vector2 transformPos, destPos;

        while(true)
        {
            if(_moveInterrupted == true)
            {
                currentDest = _destination.Dequeue();
                _moveInterrupted = false;
            }    
            transformPos = new Vector2(_transform.position.x, _transform.position.z);
            destPos = new Vector2(currentDest.x, currentDest.z);

            dist = Vector2.Distance(transformPos, destPos);

            if (dist <= 0.1f)
            {
                if(_destination.Count == 0)
                    break;

                currentDest = _destination.Dequeue();
            }
            MoveToward(currentDest);

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
}
