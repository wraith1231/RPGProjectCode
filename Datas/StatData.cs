using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class StatData
    {
        //status
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

        public float WeaponSkill;

        public bool Gender;

        public List<int> AllGenderOutfit = new List<int>();
        public List<int> OneGenderOutfit = new List<int>();

        public void MonsterDataInput(Data.MonsterData data)
        {
            HealthPoint = data.HealthPoint;
            StaminaPoint = data.StaminaPoint;
            HealthRecovery = data.HealthRecovery;
            StaminaRecovery = data.StaminaRecovery;

            Power = data.Power;
            Agility = data.Agility;
            Defense = data.Defense;

            AttackAdvantage = data.AttackAdvantage;
            DefenseAdvantage = data.DefenseAdvantage;
            AgilityAdvantage = data.AgilityAdvantage;
        }
    }

    [System.Serializable]
    public class StatList : ILoader<int, StatData>
    {
        public StatData PlayerStartStat;
        public StatData NPCMinimumStat;
        public StatData NPCMaximumStat;

        public void MakeDict()
        {

        }

        public void GetStatList(out StatData startStat, out StatData minimumStat, out StatData maximumStat)
        {
            startStat = PlayerStartStat;
            minimumStat = NPCMinimumStat;
            maximumStat = NPCMaximumStat;
        }
    }

}