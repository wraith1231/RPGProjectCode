using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEnchantWindow : UIPopup
{
    enum UIObjects
    {
        TextGroup,
        ButtonGroup,
    }

    enum UIButtons
    {
        CloseButton,
    }

    private UITextGroup _texts;
    private UIButtonGroup _buttons;
    private int _enchantMoney;

    public override void Init()
    {
        base.Init();
        Time.timeScale = 0;
        GetComponent<Canvas>().worldCamera = Managers.Map.UICam;
        _enchantMoney = 50;

        Bind<GameObject>(typeof(UIObjects));
        Bind<Button>(typeof(UIButtons));

        _texts = Get<GameObject>((int)UIObjects.TextGroup).GetComponent<UITextGroup>();
        _buttons = Get<GameObject>((int)UIObjects.ButtonGroup).GetComponent<UIButtonGroup>();

        _texts.CharacterSetting(Managers.General.GlobalPlayer.GlobalData);
        _buttons.CharacterSetting(Managers.General.GlobalPlayer.GlobalData, _texts, _enchantMoney);

        Get<Button>((int)UIButtons.CloseButton).gameObject.BindUIEvent(CloseButton);
    }

    private void CloseButton(PointerEventData data)
    {
        Time.timeScale = 1;
        Managers.UI.ClosePopupUI(this);
    }
}
