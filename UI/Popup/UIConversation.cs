using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIConversation : UIPopup
{
    enum Texts
    {
        ConversationText,
        NameText
    }

    enum GameObjects
    {
        Panel,
    }

    public override void Init()
    {
        base.Init();

        Managers.Context.CurrentConversation = this;

        GetComponent<Canvas>().worldCamera = Managers.Map.UICam;
        //GetComponent<Canvas>().planeDistance = GetComponent<Canvas>().sortingOrder;

        Bind<TMP_Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        ChangeContext();

        Get<GameObject>((int)GameObjects.Panel).BindUIEvent(CheckNextConversation);
        //Managers.Input.LeftMouseAction -= CheckNextConversation;
        //Managers.Input.LeftMouseAction += CheckNextConversation;
    }

    public void ChangeContext()
    {
        string name = Managers.Context.CurrentTalker;
        Debug.Log($"talke = {name}, context = {Managers.Context.CurrentContext}");
        string context = Managers.Context.GetCurrentContextString();

        Get<TMP_Text>((int)Texts.NameText).text = name;
        Get<TMP_Text>((int)Texts.ConversationText).text = context;
    }

    private void CheckNextConversation(PointerEventData eventData)
    {
        Define.InteractionEvent next = Managers.Context.CheckNextContext();
        if (next == Define.InteractionEvent.End)
        {
            Managers.UI.ClosePopupUI(this);
            return;
        }

        switch (next)
        {
            case Define.InteractionEvent.Context:
                ChangeContext();
                break;
            case Define.InteractionEvent.Question:
                Managers.UI.MakePopupUI<UIChoiceInterface>();
                break;
            case Define.InteractionEvent.Reward:
                break;
            case Define.InteractionEvent.Quest:
                break;
            case Define.InteractionEvent.Unknown:
                break;
            case Define.InteractionEvent.Outfit:
                Managers.UI.MakePopupUI<UIChangeOutfitInterface>();
                Managers.UI.ClosePopupUI(this);
                break;
            case Define.InteractionEvent.Blacksmith:
                break;
            case Define.InteractionEvent.Enchant:
                break;
            case Define.InteractionEvent.Rest:
                break;
        }
    }

    private void OnDestroy()
    {
        Managers.Context.CurrentConversation = null;
    }
}
