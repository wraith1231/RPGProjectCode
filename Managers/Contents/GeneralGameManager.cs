using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameManager
{
    private GlobalPlayerController _globalPlayer;
    public GlobalPlayerController GlobalPlayer { get { return _globalPlayer; } }

    public void Init()
    {
        _globalPlayer = new GlobalPlayerController();

        //_globalPlayer.SetRightWeapon(Define.WeaponCategory.TwoHand, "Mace1");
        //_globalPlayer.SetRightWeapon(Define.WeaponCategory.OneHand, "Sword4");
        //_globalPlayer.SetLeftWeapon(Define.WeaponCategory.OneHand, "Axe1");
        //_globalPlayer.SetLeftWeapon(Define.WeaponCategory.Shield, "Buckler1");
        //_globalPlayer.SetRightWeapon(Define.WeaponCategory.Shield, "KiteShield1");
        //_globalPlayer.SetRightWeapon(Define.WeaponCategory.TwoHand, "Spear1");
    }
}
