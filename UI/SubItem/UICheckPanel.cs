using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICheckPanel : UIBase
{
    public enum UITexts
    {
        CheckText,
    }

    public enum UIButtons
    {
        Accept,
        Decline,
    }

    public bool AcceptClicekd = false;
    public delegate void ButtonClickedFunc(bool val);
    public ButtonClickedFunc WaitFunctions;
    public string PanelTexts;

    public override void Init()
    {
        Bind<TMP_Text>(typeof(UITexts));
        Bind<Button>(typeof(UIButtons));

        Get<Button>((int)UIButtons.Accept).gameObject.BindUIEvent(AcceptButtonClicekd);
        Get<Button>((int)UIButtons.Decline).gameObject.BindUIEvent(DeclineButtonClicked);
    }

    public void ActiveTrue()
    {
        Get<TMP_Text>((int)UITexts.CheckText).text = PanelTexts;
    }
    
    private void AcceptButtonClicekd(PointerEventData data)
    {
        AcceptClicekd = true;
        WaitFunctions(AcceptClicekd);
    }

    private void DeclineButtonClicked(PointerEventData data)
    {
        AcceptClicekd = false;
        WaitFunctions(AcceptClicekd);
    }
}
