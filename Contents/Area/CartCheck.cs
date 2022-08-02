using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class CartCheck : MonoBehaviour
{
    private AreaGroupController _parentController;
    public void SetParent(AreaGroupController con) { _parentController = con; }

    public List<string> MateTag = new List<string>();
    public List<string> EnemyTag = new List<string>();

    [SerializeField]
    private List<AreaGroupController> _nearMate = new List<AreaGroupController>();
    [SerializeField]
    private List<AreaGroupController> _nearEnemy = new List<AreaGroupController>();

    public AreaGroupController GetEnemy()
    {
        if (_nearEnemy.Count > 0)
            return _nearEnemy[0];

        return null;
    }

    public AreaGroupController GetMate()
    {
        if (_nearMate.Count > 0)
            return _nearMate[0];

        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;

        if ((layer == 6 || layer == 9) == false) return;

        string tag = other.tag;
        if(MateTag.Contains(tag))
        {
            _nearMate.Add(other.GetComponent<AreaGroupController>());
        }
        else if(EnemyTag.Contains(tag))
        {
            _nearEnemy.Add(other.GetComponent<AreaGroupController>());
            if (_parentController.CharacterType == Define.GroupType.Monster)
                _parentController.SetAppearanceVisible(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        int layer = other.gameObject.layer;

        if ((layer == 6 || layer == 9) == false) return;

        string tag = other.tag;
        if (MateTag.Contains(tag))
        {
            _nearMate.Remove(other.GetComponent<AreaGroupController>());
        }
        else if (EnemyTag.Contains(tag))
        {
            _nearEnemy.Remove(other.GetComponent<AreaGroupController>());
        }
    }
}
