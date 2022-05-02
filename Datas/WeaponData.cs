using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class WeaponData
    {
        public Define.WeaponType Type;
        public string File;

        public float RPosX;
        public float RPosY;
        public float RPosZ;

        public float RRotX;
        public float RRotY;
        public float RRotZ;

        public float LPosX;
        public float LPosY;
        public float LPosZ;

        public float LRotX;
        public float LRotY;
        public float LRotZ;

        public float SizeX;
        public float SizeY;
        public float SizeZ;
    }

    [System.Serializable]
    public class WeaponList : ILoader<string, WeaponData>
    {
        public List<WeaponData> onehand = new List<WeaponData>();
        public List<WeaponData> twohand = new List<WeaponData>();
        public List<WeaponData> shield = new List<WeaponData>();

        public Dictionary<string, WeaponData> onehandDict = new Dictionary<string, WeaponData>();
        public Dictionary<string, WeaponData> twohandDict = new Dictionary<string, WeaponData>();
        public Dictionary<string, WeaponData> shieldDict = new Dictionary<string, WeaponData>();

        public void MakeDict()
        {
            for (int i = 0; i < onehand.Count; i++)
            {
                onehandDict.Add(onehand[i].File, onehand[i]);
            }
            for (int i = 0; i < twohand.Count; i++)
            {
                twohandDict.Add(twohand[i].File, twohand[i]);
            }
            for (int i = 0; i < shield.Count; i++)
            {
                shieldDict.Add(shield[i].File, shield[i]);
            }
        }

        public void GetWeaponDict(out Dictionary<string, WeaponData> one, out Dictionary<string, WeaponData> two, out Dictionary<string, WeaponData> shie)
        {
            one = onehandDict;
            two = twohandDict;
            shie = shieldDict;
        }

        public void GetOnehandList(out List<List<WeaponData>> list)
        {
            list = new List<List<WeaponData>>();
            
            int size = (int)Define.WeaponType.Unknown;
            for (int i = 0; i < size; i++)
            {
                List<WeaponData> temp;
                GetDictionary(onehandDict, (Define.WeaponType)i, out temp);
                list.Insert(i, temp);
            }
        }

        public void GetTwohandList(out List<List<WeaponData>> list)
        {
            list = new List<List<WeaponData>>();
            int size = (int)Define.WeaponType.Unknown;
            for (int i = 0; i < size; i++)
            {
                List<WeaponData> temp;
                GetDictionary(twohandDict, (Define.WeaponType)i, out temp);
                list.Insert(i, temp);
            }
        }

        public void GetShieldList(out List<List<WeaponData>> list)
        {
            list = new List<List<WeaponData>>();
            int size = (int)Define.WeaponType.Unknown;
            for (int i = 0; i < size; i++)
            {
                List<WeaponData> temp;
                GetDictionary(shieldDict, (Define.WeaponType)i, out temp);
                list.Insert(i, temp);
            }
        }

        private void GetDictionary(Dictionary<string, WeaponData> data, Define.WeaponType type , out List<WeaponData> list)
        {
            list = new List<WeaponData>();
            foreach(KeyValuePair<string, WeaponData> weapon in data)
            {
                if (weapon.Value.Type == type)
                    list.Add(weapon.Value);
            }
        }
    }
}