using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIVillageInterface : UIPopup
{
    enum GameObjects
    {
        SubMenuPanel,
        CharacterPanel,
    }

    enum Buttons
    {
        Gate,
        Market,
        Workshop,
        Square,
        Guild,
        Mayor,
        Inn,
        Exit,
    }

    enum Texts
    {
        VillageName,
    }

    GlobalVillageData _currentVillage;

    // Start is called before the first frame update
    void Start()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));

        GameObject panel = Get<GameObject>((int)GameObjects.SubMenuPanel);
        foreach (Transform child in panel.transform)
            Destroy(child.gameObject);
        panel = Get<GameObject>((int)GameObjects.CharacterPanel);
        foreach (Transform child in panel.transform)
            Destroy(child.gameObject);

        _currentVillage = Managers.General.GlobalVillages[Managers.General.GlobalGroups[0].CurrentVillageNumber].Data;
        Get<TMP_Text>((int)Texts.VillageName).text = _currentVillage.VillageName;

        int size = (int)Define.Facilities.Unknown;
        for(int i = 0; i < size; i++)
        {
            if(_currentVillage.Facility.ContainsKey((Define.Facilities)i) == false)
            {
                Color color = Get<Button>((int)i).image.color;
                color.a = 0.5f;
                Get<Button>((int)i).image.color = color;
                Get<Button>((int)i).enabled = false;
            }
            else
            {
                //바인딩을 해줘야되는데 이거 일일이 binduievent해줘야됨??
                switch ((Buttons)i)
                {
                    case Buttons.Gate:
                        Get<Button>((int)i).gameObject.BindUIEvent(OnGateButton);
                        break;
                    case Buttons.Market:
                        Get<Button>((int)i).gameObject.BindUIEvent(OnMarketButton);
                        break;
                    case Buttons.Workshop:
                        Get<Button>((int)i).gameObject.BindUIEvent(OnWorkshopButton);
                        break;
                    case Buttons.Square:
                        Get<Button>((int)i).gameObject.BindUIEvent(OnSquareButton);
                        break;
                    case Buttons.Guild:
                        Get<Button>((int)i).gameObject.BindUIEvent(OnGuildButton);
                        break;
                    case Buttons.Mayor:
                        Get<Button>((int)i).gameObject.BindUIEvent(OnMayorButton);
                        break;
                    case Buttons.Inn:
                        Get<Button>((int)i).gameObject.BindUIEvent(OnInnButton);
                        break;
                }
            }
        }

        Get<Button>((int)Buttons.Exit).gameObject.BindUIEvent(CloseVillageInterface);

    }

    void CloseVillageInterface(PointerEventData eventData)
    {
        Debug.Log("Close Popup!!");
        Managers.UI.ClosePopupUI(this);
    }

    private void DeleteAllSubItem()
    {
        DeleteMenuSubItem();
        DeleteCharacterSubItem();
    }

    private void DeleteMenuSubItem()
    {
        GameObject panel = Get<GameObject>((int)GameObjects.SubMenuPanel);
        foreach (Transform child in panel.transform)
            Destroy(child.gameObject);
    }
    private void DeleteCharacterSubItem()
    {
        GameObject panel = Get<GameObject>((int)GameObjects.CharacterPanel);
        foreach (Transform child in panel.transform)
            Destroy(child.gameObject);
    }

    private void MakeCharacters(Transform transform, Define.Facilities facilities)
    {
        List<int> members = _currentVillage.Facility[facilities];
        int size = members.Count;
        for (int i = 0; i < size; i++)
        {
            Managers.UI.MakeSubItem<UICharacterButton>(transform, members[i].ToString());
        }
    }

    void OnGateButton(PointerEventData eventData)
    {
        DeleteAllSubItem();

        GameObject panel = Get<GameObject>((int)GameObjects.SubMenuPanel);
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Talk Guard");

        panel = Get<GameObject>((int)GameObjects.CharacterPanel);
        MakeCharacters(panel.transform, Define.Facilities.Gate);
    }
    void OnMarketButton(PointerEventData eventData)
    {
        DeleteAllSubItem();

        GameObject panel = Get<GameObject>((int)GameObjects.SubMenuPanel);
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Talk Marchant");
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Buy Something");
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Sell Something");
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Change looks");

        panel = Get<GameObject>((int)GameObjects.CharacterPanel);
        MakeCharacters(panel.transform, Define.Facilities.Market);

    }
    void OnWorkshopButton(PointerEventData eventData)
    {
        DeleteAllSubItem();

        GameObject panel = Get<GameObject>((int)GameObjects.SubMenuPanel);
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Talk Blacksmith");
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Change Weapon");

        panel = Get<GameObject>((int)GameObjects.CharacterPanel);
        MakeCharacters(panel.transform, Define.Facilities.Workshop);
    }
    void OnSquareButton(PointerEventData eventData)
    {
        DeleteAllSubItem();

        GameObject panel = Get<GameObject>((int)GameObjects.SubMenuPanel);
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Talk Person");

        panel = Get<GameObject>((int)GameObjects.CharacterPanel);
        MakeCharacters(panel.transform, Define.Facilities.Square);
    }
    void OnGuildButton(PointerEventData eventData)
    {
        DeleteAllSubItem();

        GameObject panel = Get<GameObject>((int)GameObjects.SubMenuPanel);
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Listen to Sound Arounds");
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Accept Quest");
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Check Quest");

        panel = Get<GameObject>((int)GameObjects.CharacterPanel);
        MakeCharacters(panel.transform, Define.Facilities.Guild);
    }
    void OnMayorButton(PointerEventData eventData)
    {
        DeleteAllSubItem();

        GameObject panel = Get<GameObject>((int)GameObjects.SubMenuPanel);
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Talk Mayor");
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Enchant");

        panel = Get<GameObject>((int)GameObjects.CharacterPanel);
        MakeCharacters(panel.transform, Define.Facilities.Mayor);
    }

    void OnInnButton(PointerEventData eventData)
    {
        DeleteAllSubItem();

        GameObject panel = Get<GameObject>((int)GameObjects.SubMenuPanel);
        Managers.UI.MakeSubItem<UIVillageSubButton>(panel.transform, "Talk Owner");

        panel = Get<GameObject>((int)GameObjects.CharacterPanel);
        MakeCharacters(panel.transform, Define.Facilities.Inn);
    }
}
