using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.SceneType SceneType { get; protected set; } = Define.SceneType.TitleScene;

    private void Awake()
    {
        
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
        {
            Managers.Resource.Instantiate("EventSystem", EventSystemIntantiate);
        }
    }

    public virtual void SceneInitialize()
    {

    }

    private void EventSystemIntantiate(GameObject obj)
    {
        obj.name = "@EventSystem";
    }

    public abstract void Clear();
}
