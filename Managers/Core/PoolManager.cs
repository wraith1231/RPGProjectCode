using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Origin { get; private set; }
        public Transform Root { get; set; }

        private Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject origin, int count = 10)
        {
            Origin = origin;
            Root = new GameObject().transform;
            Root.name = $"{Origin.name}_Root";
            for (int i = 0; i < count; i++)
                Push(Create());
        }

        private Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Origin);
            go.name = Origin.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable._isUsing = false;

            _poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();

            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            poolable.transform.parent = parent;
            poolable.gameObject.SetActive(true);
            poolable._isUsing = true;

            return poolable;
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();

    Transform _root;

    public void Init()
    {
        if(_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        if(_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject origin, Transform parent = null)
    {
        if (_pool.ContainsKey(origin.name) == false)
            CreatePool(origin);

        return _pool[origin.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;

        return _pool[name].Origin;
    }

    public void CreatePool(GameObject origin, int count = 10)
    {
        Pool pool = new Pool();
        pool.Init(origin, count);
        pool.Root.parent = _root.transform;

        _pool.Add(origin.name, pool);
    }

    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}
