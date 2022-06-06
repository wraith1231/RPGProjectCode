using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager
{
    private Dictionary<string, GameObject> _vfxs = new Dictionary<string, GameObject>();

    public void LoadEffects(string name)
    {
        if (_vfxs.ContainsKey(name))
            return;

        Managers.Resource.Load<GameObject>(name, AddEffect);
    }

    private void AddEffect(GameObject go)
    {
        _vfxs[go.name] = go;
        Debug.Log($"{go.name} is loaded!");
    }

    public void Instantiate(string name, Vector3 pos)
    {
        if(_vfxs.ContainsKey(name) == false)
        {
            Debug.LogError($"{name} is not loaded!!");
            return;
        }

        GameObject.Instantiate(_vfxs[name], pos, Quaternion.identity);
    }

    public void Init()
    {
        Managers.VFX.LoadEffects("HitEffect");
    }

    public void Clear()
    {
        foreach (KeyValuePair<string, GameObject> data in _vfxs)
        {
            if (data.Value == null)
                continue;

            Managers.Resource.Release(data.Value);
        }
        _vfxs.Clear();
    }
}
