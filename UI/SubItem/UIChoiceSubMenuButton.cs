using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIChoiceSubMenuButton : UIScene
{
    public int ChoiceNumber = -1;

    enum Texts
    {
        Text,
    }

    private bool _binded = false;
    // Start is called before the first frame update
    void Start()
    {
        BindCheck();

        BindUIEvent(gameObject, ClickEvent);
    }

    private void BindCheck()
    {
        if(_binded == false)
        {
            _binded = true;
            Bind<TMP_Text>(typeof(Texts));
        }
    }

    public void ChangeText(string context)
    {
        BindCheck();
        Get<TMP_Text>((int)Texts.Text).text = context;
    }

    private void ClickEvent(PointerEventData eventData)
    {
        Define.InteractionEvent next = Managers.Context.ChoiceSomething(ChoiceNumber);

        switch (next)
        {
            case Define.InteractionEvent.End:
                Managers.UI.ClosePopupUI(Managers.Context.CurrentChoiceInterface);
                Managers.UI.ClosePopupUI(Managers.Context.CurrentConversation);
                break;
            case Define.InteractionEvent.Context:
                Managers.UI.ClosePopupUI(Managers.Context.CurrentChoiceInterface);
                if (Managers.Context.CurrentConversation == null)
                {
                    Managers.UI.MakePopupUI<UIConversation>();
                    Managers.UI.ClosePopupUI(Managers.Context.CurrentChoiceInterface);
                }
                else
                {
                    Managers.Context.CurrentConversation.ChangeContext();
                    Managers.UI.ClosePopupUI(Managers.Context.CurrentChoiceInterface);
                }
                break;
            case Define.InteractionEvent.Question:
                Managers.Context.CurrentChoiceInterface.ResetChoices();
                break;
            case Define.InteractionEvent.Reward:
                //reward Áà¾ßµÊ

                Managers.UI.ClosePopupUI(Managers.Context.CurrentChoiceInterface);
                Managers.UI.ClosePopupUI(Managers.Context.CurrentConversation);
                break;
            case Define.InteractionEvent.Quest:
                //Äù½ºÆ® Áà¾ßµÊ

                Managers.UI.ClosePopupUI(Managers.Context.CurrentChoiceInterface);
                Managers.UI.ClosePopupUI(Managers.Context.CurrentConversation);
                break;
            case Define.InteractionEvent.Unknown:
                break;
        }
    }
}
