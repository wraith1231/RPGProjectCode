using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager
{
    private List<string> _keys = new List<string>();
    private Dictionary<string, UnityEngine.Object> _assets = new Dictionary<string, UnityEngine.Object>();

    public void ReleaseStock()
    {
        int size = _keys.Count;
        for (int i = 0; i < size; i++)
        {
            if(_assets[_keys[i]] != null)
            Addressables.Release(_assets[_keys[i]]);
        }

        _keys.Clear();
        _assets.Clear();
    }

    public void Load<T>(string key, Action<T> foo) where T : UnityEngine.Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = GetName(key);

            GameObject go = Managers.Pool.GetOriginal(name);

            if (go != null)
            {
                foo(go as T);
                return;
            }
        }

        AsyncOperationHandle han = Addressables.LoadAssetAsync<T>(key);
        han.Completed += (handle) =>
        {
            if (_assets.ContainsKey(key) == false)
            {
                if (handle.Result.GetType() == typeof(GameObject))
                { 
                    _keys.Add(key);
                    _assets[key] = handle.Result as T;
                }
            }
            if(foo != null) foo(handle.Result as T); 
        };

        return;
    }

    private string GetName(string key)
    {
        string name = key;
        int index = name.LastIndexOf('/');
        if (index >= 0)
            name = name.Substring(index + 1);

        return name;
    }

    //gameobject만 쓰세요
    public void Instantiate(string key, Action<GameObject> foo, Transform parent = null)
    {
        string name = GetName(key);

        UnityEngine.Object go;
        if(_assets.TryGetValue(key, out go) == true)
        {
            GameObject gameObject = go as GameObject;
            if (gameObject.GetComponent<Poolable>() != null)
            {
                foo(Managers.Pool.Pop(gameObject).gameObject);
                return;
            }
        }

        AsyncOperationHandle han = Addressables.InstantiateAsync(key, parent);

        han.Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject go = handle.Result as GameObject;
                go.name = key;
                if(foo != null)
                    foo(go);
            }
        };

        return ;
    }

    public void Release(UnityEngine.Object obj)
    {
        if (obj.GetType() == typeof(GameObject))
        {
            Poolable poolable = (obj as GameObject).GetComponent<Poolable>();
            if (poolable != null)
            {
                Managers.Pool.Push(poolable);
                return;
            }
            else
                Addressables.ReleaseInstance(obj as GameObject);
        }
        else
        {
            UnityEngine.Object temp;
            if(_assets.TryGetValue(obj.name, out temp))
            {
                _assets[obj.name] = null;
                Addressables.Release(temp);
            }
            else
            {
                Addressables.Release(obj);
            }
        }
    }
}
