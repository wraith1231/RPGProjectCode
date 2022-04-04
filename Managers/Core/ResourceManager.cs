using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager
{
    private Dictionary<string, UnityEngine.Object> _assets = new Dictionary<string, UnityEngine.Object>();

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
        han.Completed += (handle) => { foo(handle.Result as T); };

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
    public void Instantiate(string key, Action<GameObject> foo)
    {
        string name = GetName(key);

        if(_assets.ContainsKey(key) == false)
        {
            Debug.Log($"First you Need to Load {key}");
            foo(null);

            return;
        }

        GameObject go = _assets[key] as GameObject;
        if(go.GetComponent<Poolable>() != null)
        {
            foo(Managers.Pool.Pop(go).gameObject);
            return;
        }

        AsyncOperationHandle han = Addressables.InstantiateAsync(_assets[key]);
        
        han.Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                foo(handle.Result as GameObject);
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
        }
    }
}
