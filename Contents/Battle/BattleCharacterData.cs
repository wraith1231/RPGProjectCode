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

    public float FinalPower { get { return Power * PowerAdvantage; } }
    public float FinalDefense { get { return Defense * DefenseAdvantage; } }
    public float FinalAgility { get { return Agility / (Agility + 100f); } }

    public BattleCharacterData(GlobalCharacterData data = null)
    {
        if (data != null)
        {
            MaxHealthPoint = CurrentHealthPoint = data.HealthPoint;
            MaxStaminaPoint = CurrentStaminaPoint = data.StaminaPoint;
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
        PowerAdvantage = 1.0f;
        DefenseAdvantage = 1.0f;
    }
}
