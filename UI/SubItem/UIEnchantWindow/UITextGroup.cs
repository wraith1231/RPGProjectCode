using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextGroup : UIBase
{
    enum UITexts
    {
        CurrentHealthPoint,
        CurrentStaminaPoint,
        CurrentPower,
        CurrentDefense,
        CurrentAgility, 
        LastResult,
        CurrentMoney,
    }

    public override void Init()
    {
        
    }

    private GlobalCharacterData _data;
    public void CharacterSetting(GlobalCharacterData data)
    {
        _data = data;

        Bind<TMP_Text>(typeof(UITexts));

        RefreshHP();
        RefreshSP();
        RefreshPower();
        RefreshDefense();
        RefreshAgility();
        RefreshMoney(Managers.General.GlobalGroups[0].Gold);
        RefreshLastResult(Define.EnchantResult.Unknown, 0);

        Managers.General.GlobalGroups[0].CallMoneyChange -= RefreshMoney;
        Managers.General.GlobalGroups[0].CallMoneyChange += RefreshMoney;
    }

    private void OnDestroy()
    {
        Managers.General.GlobalGroups[0].CallMoneyChange -= RefreshMoney;
    }

    public void RefreshHP()
    {
        Get<TMP_Text>((int)UITexts.CurrentHealthPoint).text = $"Current HP : {_data.HealthPoint}";
    }
    public void RefreshSP()
    {
        Get<TMP_Text>((int)UITexts.CurrentStaminaPoint).text = $"Current SP : {_data.StaminaPoint}";
    }
    public void RefreshPower()
    {
        Get<TMP_Text>((int)UITexts.CurrentPower).text = $"Current Power : {_data.Power}";
    }
    public void RefreshDefense()
    {
        Get<TMP_Text>((int)UITexts.CurrentDefense).text = $"Current Defense : {_data.Defense}";
    }
    public void RefreshAgility()
    {
        Get<TMP_Text>((int)UITexts.CurrentAgility).text = $"Current Agility : {_data.Agility}";
    }

    public void RefreshMoney(int money)
    {
        Get<TMP_Text>((int)UITexts.CurrentMoney).text = $"Current Money : {money}";
    }

    public void RefreshLastResult(Define.EnchantResult result, float value)
    {
        switch (result)
        {
            case Define.EnchantResult.HealthPoint:
                Get<TMP_Text>((int)UITexts.LastResult).text = $"Health Point + {value}";
                break;
            case Define.EnchantResult.StaminaPoint:
                Get<TMP_Text>((int)UITexts.LastResult).text = $"Stamina Point + {value}";
                break;
            case Define.EnchantResult.Power:
                Get<TMP_Text>((int)UITexts.LastResult).text = $"Power + {value}";
                break;
            case Define.EnchantResult.Defense:
                Get<TMP_Text>((int)UITexts.LastResult).text = $"Defense + {value}";
                break;
            case Define.EnchantResult.Agility:
                Get<TMP_Text>((int)UITexts.LastResult).text = $"Agility + {value}";
                break;
            case Define.EnchantResult.Unknown:
                Get<TMP_Text>((int)UITexts.LastResult).text = $"Enchant Result";
                break;
        }
    }
}
