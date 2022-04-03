using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ResourceManager
{
    private Dictionary<string, UnityEngine.Object> _assets = new Dictionary<string, UnityEngine.Object>();

    public void Load<T>(string key, Action<T> foo) where T : UnityEngine.Object
    {
        if (_assets.ContainsKey(key) == true)
        {
            foo((T)_assets[key]);
            return;
        }

        Addressables.LoadAssetAsync<T>(key).Completed += (handle) =>
        {
            _assets[key] = handle.Result;
        };

        return;
    }

    //gameobject만 쓰세요
    public void Instantiate(string key, Action<GameObject> foo)
    {
        if(Managers.Pool.GetOriginal(key) == true)
        {
            foo(Managers.Pool.Pop(Managers.Pool.GetOriginal(key)).gameObject);
        }

        Addressables.InstantiateAsync(_assets[key]).Completed += (handle) =>
        {
            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                foo(handle.Result);
            }
        };
    }

    public void Release(string path)
    {
        Addressables.Release(_assets[path]);
    }
}
