using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public enum NodeState
    {
        Running,
        Success,
        Failed
    }

    public class HeroNode
    {
        public EnemyHeroController _parentController;
        protected NodeState _state;

        public HeroNode parent;
        protected List<HeroNode> _children = new List<HeroNode>();

        private Dictionary<string, UnityEngine.Object> _dataContext = new Dictionary<string, UnityEngine.Object>();
        private Dictionary<int, BattleHeroController> _enemies = new Dictionary<int, BattleHeroController>();
        private List<int> _enemyKeys = new List<int>();

        public HeroNode(EnemyHeroController controller)
        {
            _parentController = controller;
            parent = null;
        }

        public HeroNode(EnemyHeroController controller, List<HeroNode> children)
        {
            _parentController = controller;
            int length = children.Count;
            for (int i = 0; i < length; i++)
                Attach(children[i]);
        }

        private void Attach(HeroNode node)
        {
            node.parent = this;
            node._parentController = _parentController;
            _children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.Failed;

        #region Enemies
        public BattleHeroController GetEnemy(int key)
        {
            BattleHeroController res = null;
            _enemies.TryGetValue(key, out res);
            return res;
        }
        public int GetEnemySize() { return _enemyKeys.Count; }

        public void AddEnemy(BattleHeroController enemy)
        {
            int id = enemy.HeroId;
            _enemies.Add(id, enemy);
            _enemyKeys.Add(id);
        }

        public BattleHeroController GetNearestEnemy()
        {
            int size = _enemyKeys.Count;
            if (size == 0)
                return null;
            float min = 99999;
            int num = -1;
            float dist;
            Vector3 pos = _parentController.transform.position;
            for(int i =0; i < size; i++)
            {
                dist = Vector3.Distance(pos, _enemies[_enemyKeys[i]].transform.position);
                if (dist < min)
                {
                    min = dist;
                    num = _enemyKeys[i];
                }
            }
            if (num < 0)
                return null;
            else
                return _enemies[num];
        }

        public void ClearEnemies()
        {
            _enemyKeys.Clear();
            _enemies.Clear();
        }

        #endregion

        #region Datas
        public void SetData(string key, UnityEngine.Object value)
        {
            _dataContext[key] = value;
        }

        public UnityEngine.Object GetData(string key)
        {
            UnityEngine.Object value = null;
            if(_dataContext.TryGetValue(key, out value))
            {
                //_dataContext.Remove(key);
                
                return value;
            }

            HeroNode node = parent;
            while(node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if(_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            HeroNode node = parent;
            while(node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }

            return false;
        }
        #endregion
    }
}