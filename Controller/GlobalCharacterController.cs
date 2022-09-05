using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCharacterController
{
    protected CharacterData _data = new CharacterData();
    public CharacterData Data { get { return _data; } set { _data = value; } }

    protected GlobalCharacterData _globalData = new GlobalCharacterData();
    public GlobalCharacterData GlobalData { get { return _globalData; } set { _globalData = value; } }

    protected BattleCharacterData _battleData;
    public BattleCharacterData BattleData { get { return _battleData; } set { _battleData = value; } }

    protected int _currentGroup;
    public int CurrentGroup { get { return _currentGroup; } set { _currentGroup = value; } }

    //0이면 Normal로, +면 좋은거 -면 나쁜거
    private Dictionary<int, float> _relations = new Dictionary<int, float>();

    protected float _goodFame;
    public float GoodFame { get { return _goodFame; } set { _goodFame = value; } }
    protected float _badFame;
    public float BadFame { get { return _badFame; } set { _badFame = value; } }

    public GlobalCharacterController()
    {
        _data.Player = false;
        _data.Outfit.Gender = Define.HumanGender.Male;
        _goodFame = 0;
        _badFame = 0;
    }

    public void SetRightWeapon(Define.WeaponCategory category, string file)
    {
        _data.Right.ChangeWeapon(category, file);
    }
    public void SetLeftWeapon(Define.WeaponCategory category, string file)
    {
        _data.Left.ChangeWeapon(category, file);
    }

    public void InnRest()
    {
        _globalData.InnRest();
    }

    public void BattleDataUpdate()
    {
        _battleData.SetData(_globalData);

        if(_data.Right.GetCategory() == Define.WeaponCategory.TwoHand)
        {
            switch (_data.Right.GetWeaponType())
            {
                case Define.WeaponType.Sword:
                    _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[5];
                    _battleData.DefenseAdvantage = 0.7f;

                    _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                    _battleData.WeaponDefense = 1.0f;
                    break;
                case Define.WeaponType.Axe:
                    _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[6];
                    _battleData.DefenseAdvantage = 0.7f;

                    _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                    _battleData.WeaponDefense = 1.0f;
                    break;
                case Define.WeaponType.Dagger:
                    _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[7];
                    _battleData.DefenseAdvantage = 0.7f;

                    _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                    _battleData.WeaponDefense = 1.0f;
                    break;
                case Define.WeaponType.Mace:
                    _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[8];
                    _battleData.DefenseAdvantage = 0.7f;

                    _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                    _battleData.WeaponDefense = 1.0f;
                    break;
                case Define.WeaponType.Spear:
                    _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[9];
                    _battleData.DefenseAdvantage = 0.7f;

                    _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                    _battleData.WeaponDefense = 1.0f;
                    break;
                case Define.WeaponType.Bow:
                    _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[10];
                    _battleData.DefenseAdvantage = 0.7f;

                    _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                    _battleData.WeaponDefense = 1.0f;
                    break;
                case Define.WeaponType.Gauntlet:
                    _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[11];
                    _battleData.DefenseAdvantage = 0.7f;

                    _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                    _battleData.WeaponDefense = 1.0f;
                    break;
                case Define.WeaponType.Shield:
                    _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[12];
                    _battleData.DefenseAdvantage = 1.5f;

                    _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                    _battleData.WeaponDefense = _battleData.WeaponPower;
                    break;
            }
        }
        else
        {
            if(_data.Left.GetCategory() == Define.WeaponCategory.Unknown)
            {
                switch (_data.Right.GetWeaponType())
                {
                    case Define.WeaponType.Sword:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[0];
                        _battleData.DefenseAdvantage = 0.5f;

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Axe:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[1];
                        _battleData.DefenseAdvantage = 0.5f;

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Dagger:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[2];
                        _battleData.DefenseAdvantage = 0.5f;

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Mace:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[3];
                        _battleData.DefenseAdvantage = 0.5f;
                        break;
                    case Define.WeaponType.Spear:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[4];
                        _battleData.DefenseAdvantage = 0.5f;

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Shield:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[12];
                        _battleData.DefenseAdvantage = 1.5f;

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    default:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[11];

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)Define.WeaponType.Gauntlet];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                }
            }
            else if(_data.Right.GetCategory() == Define.WeaponCategory.Unknown)
            {
                switch (_data.Left.GetWeaponType())
                {
                    case Define.WeaponType.Sword:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[0];
                        _battleData.DefenseAdvantage = 0.5f;

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Axe:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[1];
                        _battleData.DefenseAdvantage = 0.5f;

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Dagger:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[2];
                        _battleData.DefenseAdvantage = 0.5f;

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Mace:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[3];
                        _battleData.DefenseAdvantage = 0.5f;

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Spear:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[4];
                        _battleData.DefenseAdvantage = 0.5f;

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Shield:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[12];
                        _battleData.DefenseAdvantage = 1.5f;

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    default:
                        _battleData.PowerAdvantage = Define.WeaponBaseAdvantage[11];

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)Define.WeaponType.Gauntlet];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                }
            }
            else
            {
                float left, right;
                left = right = 1.0f;
                bool shieldHere = false;
                switch (_data.Left.GetWeaponType())
                {
                    case Define.WeaponType.Sword:
                        left = Define.WeaponBaseAdvantage[0];

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Axe:
                        left = Define.WeaponBaseAdvantage[1];

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Dagger:
                        left = Define.WeaponBaseAdvantage[2];

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Mace:
                        left = Define.WeaponBaseAdvantage[3];

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Spear:
                        left = Define.WeaponBaseAdvantage[4];

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Shield:
                        left = Define.WeaponBaseAdvantage[12];
                        shieldHere = true;

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = _battleData.WeaponPower;
                        break;
                }
                switch (_data.Right.GetWeaponType())
                {
                    case Define.WeaponType.Sword:
                        right = Define.WeaponBaseAdvantage[0];

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Axe:
                        right = Define.WeaponBaseAdvantage[1];

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Dagger:
                        right = Define.WeaponBaseAdvantage[2];

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Mace:
                        right = Define.WeaponBaseAdvantage[3];

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Spear:
                        right = Define.WeaponBaseAdvantage[4];

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = 1.0f;
                        break;
                    case Define.WeaponType.Shield:
                        right = Define.WeaponBaseAdvantage[12];
                        shieldHere = true;

                        _battleData.WeaponPower = _globalData.WeaponSkills[(int)_data.Right.GetWeaponType()];
                        _battleData.WeaponDefense = _battleData.WeaponPower;
                        break;
                }

                _battleData.PowerAdvantage = left > right ? right : left;
                _battleData.DefenseAdvantage = shieldHere ? 1.5f : 0.6f;
            }
        }
    }
}
