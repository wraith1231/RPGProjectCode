using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIChangeOutfitInterface : UIPopup
{
    enum UIObjects
    {
        Character,
        AllGenderSlider,
        OneGenderSlider,
        CheckPanel,
    }

    enum Buttons
    {
        SubmitButton,
        LeftRotate,
        RightRotate,
        ResetButton,
        CancelButton,
    }

    enum UITexts
    {
        PlayerGold,
        ChangeGold,
    }

    private CharacterOutfit _outfit;

    private HumanOutfit _outfitData;
    private HumanOutfit _baseOutfitData;

    private bool _rotateStart = false;
    private bool _leftClick = false;

    private int _changeValue = 0;
    private int _changePrice = 5;

    private UIAllGenderOutfitSlider _allGender;
    private UIOneGenderOutfitSlider _oneGender;
    private UICheckPanel _checkPanel;

    public override void Init()
    {
        Time.timeScale = 0;
        base.Init();

        Bind<GameObject>(typeof(UIObjects));
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(UITexts));

        Get<TMP_Text>((int)UITexts.PlayerGold).text = $"Gold : {Managers.General.GlobalGroups[0].Gold}";
        Get<TMP_Text>((int)UITexts.ChangeGold).text = $"Need : 0";

        GetComponent<Canvas>().worldCamera = Managers.Map.UICam;

        _outfit = Get<GameObject>((int)UIObjects.Character).GetComponent<CharacterOutfit>();
        _outfitData = new HumanOutfit();
        _outfitData.CopyHumanOutfit( Managers.General.GlobalPlayer.Data.Outfit);
        _baseOutfitData = new HumanOutfit();
        _baseOutfitData.CopyHumanOutfit(Managers.General.GlobalPlayer.Data.Outfit);

        _outfit.InitEnded += SetBaseDataToOutfit;

        _allGender = Get<GameObject>((int)UIObjects.AllGenderSlider).GetComponent<UIAllGenderOutfitSlider>();
        _oneGender = Get<GameObject>((int)UIObjects.OneGenderSlider).GetComponent<UIOneGenderOutfitSlider>();

        _allGender.SetCharacter(_outfitData, _baseOutfitData, _outfit);
        _oneGender.SetCharacter(_outfitData, _baseOutfitData, _outfit);

        _allGender.SetParentUI(this);
        _oneGender.SetParentUI(this);

        Get<Button>((int)Buttons.LeftRotate).onClick.AddListener(LeftRotateButton);
        Get<Button>((int)Buttons.RightRotate).onClick.AddListener(RightRotateButton);

        _outfit.InitEnded += _allGender.SliderInit;
        _outfit.InitEnded += _oneGender.SliderInit;

        Get<Button>((int)Buttons.SubmitButton).gameObject.BindUIEvent(SubmitButtonClicked);
        Get<Button>((int)Buttons.ResetButton).gameObject.BindUIEvent(ResetButton);
        Get<Button>((int)Buttons.CancelButton).gameObject.BindUIEvent(CancelButton);

        _changeValue = 0;

        _checkPanel = Get<GameObject>((int)UIObjects.CheckPanel).GetComponent<UICheckPanel>();
        _checkPanel.Init();
        _checkPanel.gameObject.SetActive(false);

    }
    private void SetBaseDataToOutfit()
    {
        _outfit.SetOutfit(_baseOutfitData);
    }

    private void ResetButton(PointerEventData data)
    {
        SetBaseDataToOutfit();

        Get<GameObject>((int)UIObjects.AllGenderSlider).GetComponent<UIAllGenderOutfitSlider>().ValueInit();
        Get<GameObject>((int)UIObjects.OneGenderSlider).GetComponent<UIOneGenderOutfitSlider>().ValueInit();

        _changeValue = 0;
        RefreshNeedString();
    }

    private void CancelButton(PointerEventData data)
    {
        _checkPanel.WaitFunctions += CancelPanelChoiced;
        _checkPanel.PanelTexts = "Cancel?";
        PanelOn();
    }
    private void CancelPanelChoiced(bool data)
    {
        if (data == true)
            ClosePopupUI();
        else
            _checkPanel.gameObject.SetActive(false);
    }

    public void RefreshNeedString()
    {
        int value = 0;

        value += _allGender.CompareDeference();
        value += _oneGender.CompareDeference();

        _changeValue = value * _changePrice;

        Get<TMP_Text>((int)UITexts.ChangeGold).text = $"Need : {_changeValue}";
    }

    private void Update()
    {
        if(_rotateStart == true)
        {
            if (_leftClick)
            {
                Get<GameObject>((int)UIObjects.Character).transform.Rotate(Vector3.up, 0.1f);
            }
            else
            {
                Get<GameObject>((int)UIObjects.Character).transform.Rotate(Vector3.up, -0.1f);
            }
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }

    private void PanelOn()
    {
        _checkPanel.gameObject.SetActive(true);
        _checkPanel.ActiveTrue();
    }

    private void SubmitButtonClicked(PointerEventData data)
    {
        if (Managers.General.GlobalGroups[0].Gold < _changeValue)
            return;

        _checkPanel.WaitFunctions += SubmitPanelChoiced;
        _checkPanel.PanelTexts = "Submit?";
        PanelOn();
    }

    private void SubmitPanelChoiced(bool data)
    {
        if(data == true)
        {
            _outfit.SaveOutfitData(Managers.General.GlobalPlayer.Data.Outfit);

            Managers.General.GlobalGroups[0].Gold -= _changeValue;
            ClosePopupUI();
        }
        else
        {
            _checkPanel.gameObject.SetActive(false);
        }
    }

    private void LeftRotateButton()
    {
        if (_rotateStart == true)
        {
            _rotateStart = false;
        }
        else
        {
            _rotateStart = true;
            _leftClick = true;
        }
    }
    private void RightRotateButton()
    {
        if (_rotateStart == true)
            _rotateStart = false;
        else
        {
            _rotateStart = true;
            _leftClick = false;
        }
    }
}
