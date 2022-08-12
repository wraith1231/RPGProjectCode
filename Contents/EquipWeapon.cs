using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon
{
    private Define.WeaponCategory Category;
    private Define.WeaponType Type;
    private string File;

    private Vector3 RPos;
    private Vector3 LPos;

    private Vector3 RRot;
    private Vector3 LRot;

    private Vector3 Size;

    public EquipWeapon()
    {
        SetDefault();
    }

    public EquipWeapon(Define.WeaponCategory category, string file)
    {
        SetDefault();
        ChangeWeapon(category, file);
    }

    public EquipWeapon(Define.WeaponCategory category, Define.WeaponType type, int num)
    {
        SetDefault();
        if (category == Define.WeaponCategory.Unknown)
            return;

        string weaponFile = "";
        switch (type)
        {
            case Define.WeaponType.Sword:
            case Define.WeaponType.Axe:
            case Define.WeaponType.Dagger:
            case Define.WeaponType.Mace:
            case Define.WeaponType.Spear:
                if(category == Define.WeaponCategory.OneHand)
                    weaponFile = Managers.Data.OnehandList[(int)type][num].File;
                else if(category == Define.WeaponCategory.TwoHand)
                    weaponFile = Managers.Data.TwohandList[(int)type][num].File;
                break;
            case Define.WeaponType.Bow:
            case Define.WeaponType.Gauntlet:
                weaponFile = Managers.Data.TwohandList[(int)type][num].File;
                break;
            case Define.WeaponType.Shield:
                weaponFile = Managers.Data.ShieldList[(int)type][num].File;
                break;
            case Define.WeaponType.Unknown:
                break;
        }
        ChangeWeapon(category, weaponFile);
    }

    private void SetDefault()
    {
        Category = Define.WeaponCategory.Unknown;
        Type = Define.WeaponType.Unknown;
        File = "";
        RPos = new Vector3();
        LPos = new Vector3();
        RRot = new Vector3();
        LRot = new Vector3();
        Size = new Vector3();
    }

    public void ChangeWeapon(Define.WeaponCategory category, string file)
    {
        Data.WeaponData data;
        switch (category)
        {
            case Define.WeaponCategory.OneHand:
                if (Managers.Data.OnehandDict.TryGetValue(file, out data) != false)
                {
                    Type = (Define.WeaponType)Enum.Parse(typeof(Define.WeaponType), data.Type);
                    File = data.File;
                    RPos = new Vector3(data.RPosX, data.RPosY, data.RPosZ);
                    RRot = new Vector3(data.RRotX, data.RRotY, data.RRotZ);
                    LPos = new Vector3(data.LPosX, data.LPosY, data.LPosZ);
                    LRot = new Vector3(data.LRotX, data.LRotY, data.LRotZ);
                    Size = new Vector3(data.SizeX, data.SizeY, data.SizeZ);
                }
                break;
            case Define.WeaponCategory.TwoHand:
                if (Managers.Data.TwohandDict.TryGetValue(file, out data) != false)
                {
                    Type = (Define.WeaponType)Enum.Parse(typeof(Define.WeaponType), data.Type);
                    File = data.File;
                    RPos = new Vector3(data.RPosX, data.RPosY, data.RPosZ);
                    RRot = new Vector3(data.RRotX, data.RRotY, data.RRotZ);
                    LPos = new Vector3(data.LPosX, data.LPosY, data.LPosZ);
                    LRot = new Vector3(data.LRotX, data.LRotY, data.LRotZ);
                    Size = new Vector3(data.SizeX, data.SizeY, data.SizeZ);
                }
                break;
            case Define.WeaponCategory.Shield:
                if (Managers.Data.SheildDict.TryGetValue(file, out data) != false)
                {
                    Type = (Define.WeaponType)Enum.Parse(typeof(Define.WeaponType), data.Type);
                    File = data.File;
                    RPos = new Vector3(data.RPosX, data.RPosY, data.RPosZ);
                    RRot = new Vector3(data.RRotX, data.RRotY, data.RRotZ);
                    LPos = new Vector3(data.LPosX, data.LPosY, data.LPosZ);
                    LRot = new Vector3(data.LRotX, data.LRotY, data.LRotZ);
                    Size = new Vector3(data.SizeX, data.SizeY, data.SizeZ);
                }
                break;
            case Define.WeaponCategory.Unknown:
                Category = Define.WeaponCategory.Unknown;
                Type = Define.WeaponType.Unknown;
                File = "";
                break;
        }
        Category = category;
    }

    public void ChangeWeapon(Define.WeaponCategory category, Define.WeaponType type, int num)
    {
        Category = category;
        Data.WeaponData data;
        switch (category)
        {
            case Define.WeaponCategory.OneHand:
                if(Managers.Data.OnehandList[(int)type].Count > num)
                {
                    data = Managers.Data.OnehandList[(int)type][num];
                    Type = (Define.WeaponType)Enum.Parse(typeof(Define.WeaponType), data.Type);
                    File = data.File;
                    RPos = new Vector3(data.RPosX, data.RPosY, data.RPosZ);
                    RRot = new Vector3(data.RRotX, data.RRotY, data.RRotZ);
                    LPos = new Vector3(data.LPosX, data.LPosY, data.LPosZ);
                    LRot = new Vector3(data.LRotX, data.LRotY, data.LRotZ);
                    Size = new Vector3(data.SizeX, data.SizeY, data.SizeZ);
                }
                break;
            case Define.WeaponCategory.TwoHand:
                if (Managers.Data.TwohandList[(int)type].Count > num)
                {
                    data = Managers.Data.TwohandList[(int)type][num];
                    Type = (Define.WeaponType)Enum.Parse(typeof(Define.WeaponType), data.Type);
                    File = data.File;
                    RPos = new Vector3(data.RPosX, data.RPosY, data.RPosZ);
                    RRot = new Vector3(data.RRotX, data.RRotY, data.RRotZ);
                    LPos = new Vector3(data.LPosX, data.LPosY, data.LPosZ);
                    LRot = new Vector3(data.LRotX, data.LRotY, data.LRotZ);
                    Size = new Vector3(data.SizeX, data.SizeY, data.SizeZ);
                }
                break;
            case Define.WeaponCategory.Shield:
                if (Managers.Data.ShieldList[(int)type].Count > num)
                {
                    data = Managers.Data.ShieldList[(int)type][num];
                    Type = (Define.WeaponType)Enum.Parse(typeof(Define.WeaponType), data.Type);
                    File = data.File;
                    RPos = new Vector3(data.RPosX, data.RPosY, data.RPosZ);
                    RRot = new Vector3(data.RRotX, data.RRotY, data.RRotZ);
                    LPos = new Vector3(data.LPosX, data.LPosY, data.LPosZ);
                    LRot = new Vector3(data.LRotX, data.LRotY, data.LRotZ);
                    Size = new Vector3(data.SizeX, data.SizeY, data.SizeZ);
                }
                break;
            case Define.WeaponCategory.Unknown:
                break;
        }
    }

    public Define.WeaponCategory GetCategory()
    {
        return Category;
    }

    public Define.WeaponType GetWeaponType() { return Type; }
    public string GetFileName() { return File; }
    public void SetFileName(string name) { File = name; }
    public Vector3 GetRightPosition() { return RPos; }
    public Vector3 GetRightRotation() { return RRot; }
    public Vector3 GetLeftPosition() { return LPos; }
    public Vector3 GetLeftRotation() { return LRot; }
    public Vector3 GetSize() { return Size; }
}
