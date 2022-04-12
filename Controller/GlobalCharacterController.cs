using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCharacterController
{
    protected CharacterData _data = new CharacterData(Vector3.zero);
    public CharacterData Data { get { return _data; } set { _data = value; } }

    private Dictionary<int, Define.CharacterRelationship> _relations = new Dictionary<int, Define.CharacterRelationship>();

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
