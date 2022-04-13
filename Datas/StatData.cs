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

        public int Money;

        public bool Gender;

        public List<int> AllGenderOutfit = new List<int>();
        public List<int> OneGenderOutfit = new List<int>();

        //public int HeadBase;
        //public int HeadMask;
        //public int HeadNoHair;
        //public int Hair;
        //public int HeadAttachment;
        //public int BackAttachment;
        //public int ShoulderRight;
        //public int ShoulderLeft;
        //public int ElbowRight;
        //public int ElbowLeft;
        //public int Hips;
        //public int KneeRight;
        //public int KneeLeft;
        //public int Extra;
        //
        //public int Head;
        //public int HeadGear;
        //public int Eyebrows;
        //public int Facial;
        //public int Torso;
        //public int ArmUpperRight;
        //public int ArmUpperLeft;
        //public int ArmLowerRight;
        //public int ArmLowerLeft;
        //public int HandRight;
        //public int HandLeft;
        //public int OneHips;
        //public int LegRight;
        //public int LegLeft;
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