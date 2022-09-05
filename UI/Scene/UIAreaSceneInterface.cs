using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAreaSceneInterface : UIScene
{
    public enum Images
    {
        DayPanel,
        StatusPanel,
    }

    public enum Buttons
    {
        Character,
        Party,
        Inventory,
        Quest,
        Setting,
    }

    private TMP_Text _dayText = null;
    private TMP_Text _moneyText = null;
    private TMP_Text _foodText = null;

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));
        _dayText = Get<Image>((int)Images.DayPanel).GetComponentInChildren<TMP_Text>();
        DayTextChange(Managers.Map.Day);

        Bind<Button>(typeof(Buttons));

        Managers.Map.DayChangeUpdate += OnDayChange;
        GetComponent<Canvas>().worldCamera = Managers.Map.UICam;

        TMP_Text[] status = Get<Image>((int)Images.StatusPanel).GetComponentsInChildren<TMP_Text>();
        _moneyText = status[0];
        _foodText = status[1];

        Managers.General.GlobalGroups[0].CallMoneyChange += MoneyChanged;
        Managers.General.GlobalGroups[0].CallFoodChange += FoodChanged;

        MoneyChanged(Managers.General.GlobalGroups[0].Gold);
        FoodChanged(Managers.General.GlobalGroups[0].Foods);
    }

    public void MoneyChanged(int value)
    {
        _moneyText.text = $"Money : {value}";
    }
    public void FoodChanged(float value)
    {
        _foodText.text = $"Food : {value}";
    }

    public void OnDayChange(int day)
    {
        DayTextChange(day);
    }

    private void DayTextChange(int day)
    {
        if (_dayText == null) return;

        _dayText.text = $"Day {day}";
    }

    private void OnDestroy()
    {
        Managers.General.GlobalGroups[0].CallMoneyChange -= MoneyChanged;
        Managers.General.GlobalGroups[0].CallFoodChange -= FoodChanged;
    }
}
