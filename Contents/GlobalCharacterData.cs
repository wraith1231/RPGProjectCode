using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCharacterData
{
    //outfit 관련은 CharacterOutfit으로
    public float HealthPoint { get; set; }
    public float CurrentHealthPoint { get; set; }
    public float StaminaPoint { get; set; }
    public float CurrentStaminaPoint { get; set; }

    public float HealthRecovery { get; set; }
    public float StaminaRecovery { get; set; }

    public int Power { get; set; }
    public int Agility { get; set; }
    public int Defense { get; set; }

    public Vector3 Position { get; set; }
    public string CharacterName { get; set; }

    public List<float> WeaponSkills = new List<float>();
    public void SetStatData(Data.StatData data)
    {
        HealthPoint = data.HealthPoint;
        CurrentHealthPoint = HealthPoint;
        StaminaPoint = data.StaminaPoint;
        CurrentStaminaPoint = StaminaPoint;
        HealthRecovery = data.HealthRecovery;
        StaminaRecovery = data.StaminaRecovery;
        Power = data.Power;
        Agility = data.Agility;
        Defense = data.Defense;

        int size = (int)Define.WeaponType.Unknown;
        for (int i = 0; i < size; i++)
            WeaponSkills.Add(1.0f);
    }

    public void SetNPCBaseData()
    {
        HealthPoint = Random.Range(Managers.Data.NPCMinimumStat.HealthPoint, Managers.Data.NPCMaximumStat.HealthPoint);
        StaminaPoint = Random.Range(Managers.Data.NPCMinimumStat.StaminaPoint, Managers.Data.NPCMaximumStat.StaminaPoint);
        CurrentHealthPoint = HealthPoint;
        CurrentStaminaPoint = StaminaPoint;
        HealthRecovery = Random.Range(Managers.Data.NPCMinimumStat.HealthRecovery, Managers.Data.NPCMaximumStat.HealthRecovery);
        StaminaRecovery = Random.Range(Managers.Data.NPCMinimumStat.StaminaRecovery, Managers.Data.NPCMaximumStat.StaminaRecovery);
        Power = Random.Range(Managers.Data.NPCMinimumStat.Power, Managers.Data.NPCMaximumStat.Power);
        Agility = Random.Range(Managers.Data.NPCMinimumStat.Agility, Managers.Data.NPCMaximumStat.Agility);
        Defense = Random.Range(Managers.Data.NPCMinimumStat.Defense, Managers.Data.NPCMaximumStat.Defense);

        int size = (int)Define.WeaponType.Unknown;
        for (int i = 0; i < size; i++)
            WeaponSkills[i] = Random.Range(Managers.Data.NPCMinimumStat.WeaponSkill, Managers.Data.NPCMaximumStat.WeaponSkill);
    }

    public void InnRest()
    {
        CurrentHealthPoint = HealthPoint;
        CurrentStaminaPoint = StaminaPoint;
    }
}
