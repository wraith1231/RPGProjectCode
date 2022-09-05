using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonGroup : UIBase
{
    enum UIButtons
    {
        EnchantHP,
        EnchantSP,
        EnchantPower,
        EnchantDefense,
        EnchantAgility,
        EnchantRandom,
    }

    private UITextGroup _texts;
    private GlobalCharacterData _data;
    private int _playerMoney;
    private int _enchantMoney;

    public override void Init()
    {

    }
    public void CharacterSetting(GlobalCharacterData data, UITextGroup texts, int enchantMoney)
    {
        _data = data;
        _texts = texts;
        _enchantMoney = enchantMoney;

        Bind<Button>(typeof(UIButtons));
        _playerMoney = Managers.General.GlobalGroups[0].Gold;

        Managers.General.GlobalGroups[0].CallMoneyChange -= MoneyRefresh;
        Managers.General.GlobalGroups[0].CallMoneyChange += MoneyRefresh;

        Get<Button>((int)UIButtons.EnchantHP).gameObject.BindUIEvent(EnchantHealthPoint);
        Get<Button>((int)UIButtons.EnchantSP).gameObject.BindUIEvent(EnchantStaminaPoint);
        Get<Button>((int)UIButtons.EnchantPower).gameObject.BindUIEvent(EnchantPower);
        Get<Button>((int)UIButtons.EnchantDefense).gameObject.BindUIEvent(EnchantDefense);
        Get<Button>((int)UIButtons.EnchantAgility).gameObject.BindUIEvent(EnchantAgility);
        Get<Button>((int)UIButtons.EnchantRandom).gameObject.BindUIEvent(EnchantRandom);
    }

    private void OnDestroy()
    {
        Managers.General.GlobalGroups[0].CallMoneyChange -= MoneyRefresh;
        Managers.General.GlobalGroups[0].CallMoneyChange -= MoneyRefresh;
    }

    private void MoneyRefresh(int money)
    {
        _playerMoney = money;
    }

    private void EnchantHealthPoint(PointerEventData data)
    {
        if(_playerMoney >= _enchantMoney)
        {
            Managers.General.GlobalGroups[0].Gold -= _enchantMoney;
            EnchantFunc(UIButtons.EnchantHP, 10);
        }
    }
    private void EnchantStaminaPoint(PointerEventData data)
    {
        if (_playerMoney >= _enchantMoney)
        {
            Managers.General.GlobalGroups[0].Gold -= _enchantMoney;
            EnchantFunc(UIButtons.EnchantSP, 10);
        }
    }
    private void EnchantPower(PointerEventData data)
    {
        if (_playerMoney >= _enchantMoney)
        {
            Managers.General.GlobalGroups[0].Gold -= _enchantMoney;
            EnchantFunc(UIButtons.EnchantPower, 5);
        }
    }
    private void EnchantDefense(PointerEventData data)
    {
        if (_playerMoney >= _enchantMoney)
        {
            Managers.General.GlobalGroups[0].Gold -= _enchantMoney;
            EnchantFunc(UIButtons.EnchantDefense, 5);
        }
    }
    private void EnchantAgility(PointerEventData data)
    {
        if (_playerMoney >= _enchantMoney)
        {
            Managers.General.GlobalGroups[0].Gold -= _enchantMoney;
            EnchantFunc(UIButtons.EnchantAgility, 5);
        }
    }

    private void EnchantRandom(PointerEventData data)
    {
        if (_playerMoney < _enchantMoney * 2)
            return;
        Managers.General.GlobalGroups[0].Gold -= _enchantMoney * 2;

        float num = RandomFloat(5000);

        if(num < 1000)
        {
            EnchantFunc(UIButtons.EnchantHP, 30);
        }
        else if(num < 2000)
        {
            EnchantFunc(UIButtons.EnchantSP, 30);
        }
        else if(num < 3000)
        {
            EnchantFunc(UIButtons.EnchantPower, 15);
        }
        else if (num < 4000)
        {

            EnchantFunc(UIButtons.EnchantDefense, 15);
        }
        else
        {
            EnchantFunc(UIButtons.EnchantAgility, 15);
        }
    }

    private void EnchantFunc(UIButtons type, float max)
    {
        switch (type)
        {
            case UIButtons.EnchantHP:
                float hp = RandomFloat(max);
                _data.HealthPoint += hp;
                _data.HealthRecovery += hp * 0.1f;
                _texts.RefreshHP();
                _texts.RefreshLastResult(Define.EnchantResult.HealthPoint, hp);
                break;

            case UIButtons.EnchantSP:
                Managers.General.GlobalGroups[0].Gold -= 10;
                float sp = RandomFloat(max);
                _data.StaminaPoint += sp;
                _data.StaminaRecovery += sp * 0.1f;
                _texts.RefreshSP();
                _texts.RefreshLastResult(Define.EnchantResult.StaminaPoint, sp);
                break;

            case UIButtons.EnchantPower:
                int power = RandomInt((int)max);
                _data.Power += power;
                _texts.RefreshPower();
                _texts.RefreshLastResult(Define.EnchantResult.Power, power);
                break;

            case UIButtons.EnchantDefense:
                int defense = RandomInt((int)max);
                _data.Defense += defense;
                _texts.RefreshDefense();
                _texts.RefreshLastResult(Define.EnchantResult.Defense, defense);
                break;

            case UIButtons.EnchantAgility:
                int agility = RandomInt((int)max);
                _data.Agility += agility;
                _texts.RefreshAgility();
                _texts.RefreshLastResult(Define.EnchantResult.Agility, agility);
                break;
        }
    }

    private float RandomFloat(float max)
    {
        return Random.Range(0.1f, max);
    }
    private int RandomInt(int max)
    {
        return Random.Range(1, max);
    }
}
