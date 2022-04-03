using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ResourceManager
{
    private Dictionary<string, UnityEngine.Object> _assets = new Dictionary<string, UnityEngine.Object>();

    public void Load<T>(string key) where T : UnityEngine.Object
    {
        if (_assets.ContainsKey(key) == true)
        {
            return;
        }

        Addressables.LoadAssetAsync<T>(key).Completed += (handle) =>
        {
            _assets[key] = handle.Result;
        };

        return;
    }

    public void Instantiate<T>(string key, Action<T> foo) where T : UnityEngine.Object
    {
        if(_assets.ContainsKey(key) == false)
        {
            Load<T>(key);
        }

        if(Managers.Pool.GetOriginal(key) == true)
        {
            foo(Managers.Pool.Pop(Managers.Pool.GetOriginal(key)).GetComponent<T>());
        }

        if (typeof(T) == typeof(GameObject))
        {
            Addressables.InstantiateAsync(_assets[key]).Completed += (handle) =>
            {
                if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    foo(handle.Result.GetComponent<T>());
                }
            };
        }
        else
        {
            Addressables.LoadAssetAsync<T>(key).Completed += (handle) =>
            {
                if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    foo(handle.Result);
                }
            };
        }
    }

    public void Release(string path)
    {
        Addressables.Release(_assets[path]);
    }
}
