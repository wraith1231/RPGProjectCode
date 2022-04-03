using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCharacterData
{
    //outfit ������ CharacterOutfit����
    //battle scene������ ���ϰ�
    public float HealthPoint { get; set; }
    public float StaminaPoint { get; set; }
    public float HealthRecovery { get; set; }
    public float StaminaRecovery { get; set; }
    public int Power { get; set; }
    public int Dexterity { get; set; }
    public float Defense { get; set; }

    //area scene������ ���ϰ�
    public int Money { get; set; }
    public Vector3 Position { get; set; }
    public string CharacterName { get; set; }
}
