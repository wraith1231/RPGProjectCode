using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleCharacterData
{
    public float MaxHealthPoint { get; set; }
    public float CurrentHealthPoint { get; set; }
    public float CurrentHPPercent { get { return CurrentHealthPoint / MaxHealthPoint; } }
    public float MaxStaminaPoint { get; set; }
    public float CurrentStaminaPoint { get; set; }
    public float CurrentSPPercent { get { return CurrentStaminaPoint / MaxStaminaPoint; } }
    public float HealthRecovery { get; set; }
    public float StaminaRecovery { get; set; }

    public int Power { get; set; }
    public float PowerAdvantage { get; set; }
    public int Agility { get; set; }
    public int Defense { get; set; }
    public float DefenseAdvantage { get; set; }

    public float WeaponPower { get; set; }
    public float WeaponDefense { get; set; }

    public float FinalPower { get { return (Power * WeaponPower) * PowerAdvantage; } }
    public float FinalDefense { get { return (Defense * WeaponDefense) * DefenseAdvantage; } }
    public float FinalAgility { get { return Agility / (Agility + 100f); } }

    public float CharacterPower { get { return MaxHealthPoint + MaxStaminaPoint + Power + Agility + Defense; } }
    public float CurrentPower { get { return CurrentHealthPoint + CurrentStaminaPoint + Power + Agility + Defense; } }

    public BattleCharacterData(GlobalCharacterData data = null)
    {
        SetData(data);
    }

    public void SetData(GlobalCharacterData data = null)
    {
        WeaponPower = 1.0f;
        WeaponDefense = 1.0f;
        PowerAdvantage = 1.0f;
        DefenseAdvantage = 1.0f;
        if (data != null)
        {
            CurrentHealthPoint = data.CurrentHealthPoint;
            CurrentStaminaPoint = data.CurrentStaminaPoint;
            MaxHealthPoint = data.HealthPoint;
            MaxStaminaPoint = data.StaminaPoint;
            HealthRecovery = data.HealthRecovery;
            StaminaRecovery = data.StaminaRecovery;
            Power = data.Power;
            Agility = data.Agility;
            Defense = data.Defense;
        }
        else
        {
            Debug.Log("global character data not found");
            MaxHealthPoint = CurrentHealthPoint = 100;
            MaxStaminaPoint = CurrentStaminaPoint = 100;
            HealthRecovery = 10;
            StaminaRecovery = 50;
            Power = 10;
            Agility = 5;
            Defense = 5;
        }
    }

    public void BattlePhaseCharacter(BattleCharacterData data)
    {

    }

    public void BattlePhaseVillage(GlobalVillageData data)
    {

    }

    public void InnRest()
    {
        CurrentHealthPoint = MaxHealthPoint;
        CurrentStaminaPoint = MaxStaminaPoint;
    }
}
