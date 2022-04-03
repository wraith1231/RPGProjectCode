using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class StatData
    {
        public int Level;
        public float Hp;
        public float Attack;
        public float Defence;
        public float Dex;
    }

    [System.Serializable]
    public class StatList : ILoader<int, StatData>
    {
        public List<StatData> statList = new List<StatData>();
        public Dictionary<int, StatData> statDict = new Dictionary<int, StatData>();

        public void MakeDict()
        {
            foreach (StatData stat in statList)
                statDict.Add(stat.Level, stat);
        }

        public void GetStatList(out Dictionary<int, StatData> dict)
        {
            dict = statDict;
        }
    }

}