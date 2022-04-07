using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameManager
{
    private GlobalPlayerController _globalPlayer;
    public GlobalPlayerController GlobalPlayer { get { return _globalPlayer; } }

    private List<GlobalNPCController> _globalCharacters;
    public List<GlobalNPCController> GlobalCharacters { get { return _globalCharacters; } }
    public void Init()
    {
        _globalPlayer = new GlobalPlayerController();
        _globalCharacters = new List<GlobalNPCController>();
        //_globalPlayer.SetRightWeapon(Define.WeaponCategory.TwoHand, "Mace1");
        //_globalPlayer.SetRightWeapon(Define.WeaponCategory.OneHand, "Sword4");
        //_globalPlayer.SetLeftWeapon(Define.WeaponCategory.OneHand, "Axe1");
        //_globalPlayer.SetLeftWeapon(Define.WeaponCategory.Shield, "Buckler1");
        //_globalPlayer.SetRightWeapon(Define.WeaponCategory.Shield, "KiteShield1");
        //_globalPlayer.SetRightWeapon(Define.WeaponCategory.TwoHand, "Spear1");
    }
}
