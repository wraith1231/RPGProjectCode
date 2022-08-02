using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIVillageSubButton : UIScene
{
    enum Texts
    {
        Text,
    }

    private Define.VillageSubButtonType _type = Define.VillageSubButtonType.Unknown;
    public Define.VillageSubButtonType ButtonType { get { return _type; } set { _type = value; } }

    // Start is called before the first frame update
    void Start()
    {
        Bind<TMP_Text>(typeof(Texts));

        Get<TMP_Text>((int)Texts.Text).text = transform.name;

        BindUIEvent(gameObject, OnClickButton);

    }

    void OnClickButton(PointerEventData eventData)
    {
        switch (_type)
        {
            case Define.VillageSubButtonType.TalkPerson:
                Managers.Context.CurrentTalker = "Civilian";

                Managers.Context.SetContextValue("Cilvilian", $"{Managers.General.GlobalPlayer.Data.CharName}");

                Managers.Context.ChangeToRandomTalkSurroundContext();
                Managers.UI.MakePopupUI<UIConversation>();
                break;
            case Define.VillageSubButtonType.TalkMaster:
                Managers.Context.CurrentTalker = "Master";

                Managers.Context.SetContextValue("Master", $"{Managers.General.GlobalPlayer.Data.CharName}");

                Managers.Context.ChangeToRandomTalkSurroundContext();
                Managers.UI.MakePopupUI<UIConversation>();
                break;
            case Define.VillageSubButtonType.TalkMayer:
                Managers.Context.CurrentTalker = "Mayer";

                Managers.Context.SetContextValue("Mayer", $"{Managers.General.GlobalPlayer.Data.CharName}");

                Managers.Context.ChangeToRandomTalkSurroundContext();
                Managers.UI.MakePopupUI<UIConversation>();
                break;
            case Define.VillageSubButtonType.TalkGuard:
                Managers.Context.CurrentTalker = "Guard";

                Managers.Context.SetContextValue("Guard", $"{Managers.General.GlobalPlayer.Data.CharName}");

                Managers.Context.ChangeToRandomTalkSurroundContext();
                Managers.UI.MakePopupUI<UIConversation>();
                break;
            case Define.VillageSubButtonType.TalkGuild:
                Managers.Context.CurrentTalker = "Guild Clerk";

                Managers.Context.SetContextValue("Guild Clerk", $"{Managers.General.GlobalPlayer.Data.CharName}");

                Managers.Context.ChangeToRandomTalkSurroundContext();
                Managers.UI.MakePopupUI<UIConversation>();
                break;
            case Define.VillageSubButtonType.TalkMerchant:
                Managers.Context.CurrentTalker = "Merchant";

                Managers.Context.SetContextValue("Merchant", $"{Managers.General.GlobalPlayer.Data.CharName}");

                Managers.Context.ChangeToRandomTalkSurroundContext();
                Managers.UI.MakePopupUI<UIConversation>();
                break;
            case Define.VillageSubButtonType.OpenChangeOutfit:
                Managers.Context.CurrentTalker = "Merchant";

                Managers.Context.SetContextValue("Merchant", $"{Managers.General.GlobalPlayer.Data.CharName}");

                Managers.Context.OpenChangeOutfitSelected();
                Managers.UI.MakePopupUI<UIConversation>();
                break;
            case Define.VillageSubButtonType.OpenBlacksmith:
                break;
            case Define.VillageSubButtonType.OpenEnchant:
                break;
            case Define.VillageSubButtonType.OpenQuest:
                break;
            case Define.VillageSubButtonType.CheckQuest:
                break;
            case Define.VillageSubButtonType.OpenRest:
                break;
            case Define.VillageSubButtonType.Unknown:
                break;
        }
    }
}
