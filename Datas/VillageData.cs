using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class VillageData
    {
        public string Name;

        public float Growth;
        public float GrowthPerDay;
        public float Safety;
        public float SafetyPerDay;
    }

    [System.Serializable]
    public class VillageList : ILoader<string, VillageData>
    {
        public List<VillageData> VillageDatas = new List<VillageData>();

        private Dictionary<string, VillageData> _villageDicts = new Dictionary<string, VillageData>();
        public Dictionary<string, VillageData> VillageDicts { get { return _villageDicts; } set { _villageDicts = value; } }

        public void MakeDict()
        {
            int size = VillageDatas.Count;
            for(int i = 0; i < size; i++)
            {
                _villageDicts[VillageDatas[i].Name] = VillageDatas[i];
            }
        }
    }
}