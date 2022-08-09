using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public abstract class UIBase : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    private void Start()
    {
        Init();

    }

    public abstract void Init();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] types = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[types.Length];

        if(_objects.ContainsKey(typeof(T)) == false)
        {
            _objects.Add(typeof(T), objects);
        }

        for(int i = 0; i < types.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, types[i], true);
            else
                objects[i] = Util.FindChilds<T>(gameObject, types[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind {types[i]}");
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected GameObject GetGameObject(int idx) { return Get<GameObject>(idx); }
    protected Slider GetSlider(int idx) { return Get<Slider>(idx); }

    public static void BindUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UIEventHandler ueh = Util.GetOrAddComponent<UIEventHandler>(go);

        switch(type)
        {
            case Define.UIEvent.Click:
                ueh.OnClickHandler -= action;
                ueh.OnClickHandler += action;
                break;
            case Define.UIEvent.DragBegin:
                ueh.OnBeginDragHandler -= action;
                ueh.OnBeginDragHandler += action;
                ueh.OnDragHandler -= action;
                ueh.OnDragHandler += action;
                break;
            case Define.UIEvent.DragEnd:
                ueh.OnBeginDragHandler -= action;
                ueh.OnDragHandler -= action;
                break;
        }
    }

}
