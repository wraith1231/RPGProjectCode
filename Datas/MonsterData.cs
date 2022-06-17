using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class MonsterData
    {
        public string Name;

        public float HealthPoint;
        public float StaminaPoint;

        public float HealthRecovery;
        public float StaminaRecovery;

        public int Power;
        public int Agility;
        public int Defense;

        public float AttackAdvantage;
        public float DefenseAdvantage;
        public float AgilityAdvantage;
    }

    [System.Serializable]
    public class MonsterList : ILoader<string, MonsterData>
    {
        public List<MonsterData> MonsterDatas = new List<MonsterData>();
        private Dictionary<string, MonsterData> _monsterDict = new Dictionary<string, MonsterData>();
        public Dictionary<string, MonsterData> MonsterDict { get { return _monsterDict; } set { _monsterDict = value; } }

        public void MakeDict()
        {
            int size = MonsterDatas.Count;
            for (int i = 0; i < size; i++)
                _monsterDict[MonsterDatas[i].Name] = MonsterDatas[i];
        }
    }
}