using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCharacterData
{
    //outfit 관련은 CharacterOutfit으로
    public float HealthPoint { get; set; }
    public float StaminaPoint { get; set; }
    public float HealthRecovery { get; set; }
    public float StaminaRecovery { get; set; }
    public int Power { get; set; }
    public int Agility { get; set; }
    public int Defense { get; set; }

    public Vector3 Position { get; set; }
    public string CharacterName { get; set; }

    public void SetStatData(Data.StatData data)
    {
        HealthPoint = data.HealthPoint;
        StaminaPoint = data.StaminaPoint;
        HealthRecovery = data.HealthRecovery;
        StaminaRecovery = data.StaminaRecovery;
        Power = data.Power;
        Agility = data.Agility;
        Defense = data.Defense;
    }

    public void SetNPCBaseData()
    {
        HealthPoint = Random.Range(Managers.Data.NPCMinimumStat.HealthPoint, Managers.Data.NPCMaximumStat.HealthPoint);
        StaminaPoint = Random.Range(Managers.Data.NPCMinimumStat.StaminaPoint, Managers.Data.NPCMaximumStat.StaminaPoint);
        HealthRecovery = Random.Range(Managers.Data.NPCMinimumStat.HealthRecovery, Managers.Data.NPCMaximumStat.HealthRecovery);
        StaminaRecovery = Random.Range(Managers.Data.NPCMinimumStat.StaminaRecovery, Managers.Data.NPCMaximumStat.StaminaRecovery);
        Power = Random.Range(Managers.Data.NPCMinimumStat.Power, Managers.Data.NPCMaximumStat.Power);
        Agility = Random.Range(Managers.Data.NPCMinimumStat.Agility, Managers.Data.NPCMaximumStat.Agility);
        Defense = Random.Range(Managers.Data.NPCMinimumStat.Defense, Managers.Data.NPCMaximumStat.Defense);
    }
}
