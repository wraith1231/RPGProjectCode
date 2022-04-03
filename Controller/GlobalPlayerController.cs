using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPlayerController
{
    private CharacterData _data = new CharacterData(Vector3.zero);
    public CharacterData Data { get { return _data; } }

    public GlobalPlayerController()
    {
        _data.Player = true;
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
