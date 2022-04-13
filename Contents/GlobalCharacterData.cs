using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCharacterData
{
    //outfit ������ CharacterOutfit����
    public float HealthPoint { get; set; }
    public float StaminaPoint { get; set; }
    public float HealthRecovery { get; set; }
    public float StaminaRecovery { get; set; }
    public int Power { get; set; }
    public int Agility { get; set; }
    public int Defense { get; set; }

    public int Money { get; set; }
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
        Money = data.Money;
    }
}
