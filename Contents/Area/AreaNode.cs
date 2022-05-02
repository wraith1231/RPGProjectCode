using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaNode : MonoBehaviour
{
    [SerializeField] private List<AreaNode> _connectedNodes = new List<AreaNode>();
    public List<AreaNode> ConnectedNode { get { return _connectedNodes; } }

    [SerializeField] public VillageStatus Village = null;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        if(Village != null)
            Village.SetBaseAreaNode(this);
    }

    public float GetDistance(Vector3 node)
    {
        return Vector3.Distance(_transform.position, node);
    }
}
