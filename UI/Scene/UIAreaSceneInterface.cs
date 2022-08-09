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

    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));
        _dayText = Get<Image>((int)Images.DayPanel).GetComponentInChildren<TMP_Text>();
        DayTextChange(Managers.Map.Day);

        Bind<Button>(typeof(Buttons));

        Managers.Map.DayChangeUpdate += OnDayChange;
        GetComponent<Canvas>().worldCamera = Managers.Map.UICam;
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
}
