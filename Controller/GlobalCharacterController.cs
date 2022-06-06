using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCharacterController
{
    protected CharacterData _data = new CharacterData();
    public CharacterData Data { get { return _data; } set { _data = value; } }

    protected GlobalCharacterData _globalData = new GlobalCharacterData();
    public GlobalCharacterData GlobalData { get { return _globalData; } set { _globalData = value; } }

    //0�̸� Normal��, +�� ������ -�� ���۰�
    private Dictionary<int, float> _relations = new Dictionary<int, float>();

    public GlobalCharacterController()
    {
        _data.Player = false;
        _data.Outfit.Gender = Define.HumanGender.Male;
    }

    public void SetRightWeapon(Define.WeaponCategory category, string file)
    {
        _data.Right.ChangeWeapon(category, file);
    }
    public void SetLeftWeapon(Define.WeaponCategory category, string file)
    {
        _data.Left.ChangeWeapon(category, file);
    }
}
