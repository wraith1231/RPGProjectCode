using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaUICamera : MonoBehaviour
{
    private Camera _camera;
    [SerializeField]
    private Vector3 _charPos;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        Managers.Map.UICam = _camera;
        //gameObject.SetActive(false);
    }

    public Vector3 GetCharPos()
    {
        return transform.position + _charPos;
    }

    private void OnDestroy()
    {
        Managers.Map.UICam = null;
    }
}
