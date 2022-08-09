using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSlider : UIBase
{
    public enum Sliders
    {
        GenderSlider,
        Head,
        AllGenderHair,
        Eyebrows,
        AllGenderExtra,
        Facial,
    }

    public enum Texts
    {
        GenderText,
        HeadText,
        HairText,
        EyeBrowText,
        ExtraText,
        FacialText,
    }

    private HumanOutfit _baseOutfit = null;
    private GameObject _char = null;
    private CharacterOutfit _outfit = null;
    public void SetCharacter(HumanOutfit outfit, GameObject go)
    {
        _baseOutfit = outfit;
        _char = go;
        _outfit = _char.GetComponent<CharacterOutfit>();
    }

    private bool _isInit = false;
    public override void Init()
    {
        if (_isInit == true)
            return;
        
        Bind<Slider>(typeof(Sliders));
        Bind<TMP_Text>(typeof(Texts));

        _isInit = true;
    }
    public void SliderInit()
    {
        if (_isInit == false)
            Init();

        MaxValueChange();

        AddListner();
    }

    private void MaxValueChange()
    {
        Get<Slider>((int)Sliders.GenderSlider).maxValue = 1;
        Get<Slider>((int)Sliders.GenderSlider).value = 1;

        Get<Slider>((int)Sliders.Head).maxValue = _outfit.GetGenderPartCount(Define.HumanOutfitOneGender.Head)-1;

        Get<Slider>((int)Sliders.AllGenderHair).minValue = -1;
        Get<Slider>((int)Sliders.AllGenderHair).maxValue = _outfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.AllGenderHair)-1;

        Get<Slider>((int)Sliders.Eyebrows).maxValue = _outfit.GetGenderPartCount( Define.HumanOutfitOneGender.Eyebrows)-1;

        Get<Slider>((int)Sliders.AllGenderExtra).minValue = -1;
        Get<Slider>((int)Sliders.AllGenderExtra).maxValue = _outfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.AllGenderExtra)-1;

        Get<Slider>((int)Sliders.Facial).minValue = -1;
        Get<Slider>((int)Sliders.Facial).maxValue = _outfit.GetGenderPartCount(Define.HumanOutfitOneGender.Facial)-1;
    }

    private void AddListner()
    {
        Get<Slider>((int)Sliders.GenderSlider).onValueChanged.AddListener(GenderSliderChange);
        Get<Slider>((int)Sliders.Head).onValueChanged.AddListener(HeadSliderChange);
        Get<Slider>((int)Sliders.AllGenderHair).onValueChanged.AddListener(HairSliderChange);
        Get<Slider>((int)Sliders.Eyebrows).onValueChanged.AddListener(EyebrowSliderChange);
        Get<Slider>((int)Sliders.AllGenderExtra).onValueChanged.AddListener(ExtraSliderChange);
        Get<Slider>((int)Sliders.Facial).onValueChanged.AddListener(FacialSliderChange);
    }

    #region Value Change Function
    private void GenderSliderChange(float value)
    {
        SliderValueChange(Sliders.GenderSlider, value);
    }
    private void HeadSliderChange(float value)
    {
        SliderValueChange(Sliders.Head, value);
    }
    private void HairSliderChange(float value)
    {
        SliderValueChange(Sliders.AllGenderHair, value);
    }
    private void EyebrowSliderChange(float value)
    {
        SliderValueChange(Sliders.Eyebrows, value);
    }
    private void ExtraSliderChange(float value)
    {
        SliderValueChange(Sliders.AllGenderExtra, value);
    }
    private void FacialSliderChange(float value)
    {
        SliderValueChange(Sliders.Facial, value);
    }
    #endregion

    private void SliderValueChange(Sliders type, float value)
    {
        int val = Mathf.RoundToInt(Get<Slider>((int)type).value);
        Get<Slider>((int)type).value = val;

        switch(type)
        {
            case Sliders.GenderSlider:
                _baseOutfit.Gender = (Define.HumanGender)val;

                ActiveChange(_baseOutfit.Gender == Define.HumanGender.Male ? true : false);
                _outfit.ChangeGender(_baseOutfit.Gender);

                break;

            case Sliders.Facial:
            case Sliders.Eyebrows:
            case Sliders.Head:
                _baseOutfit.SetOneGenderData(type.ToString(), val);
                _outfit.ChangeGenderOutfit(type.ToString(), _baseOutfit.GetOneGenderData(type.ToString()));
                break;

            default:
                _baseOutfit.SetAllGenderData(type.ToString(), val);
                _outfit.ChangeAllGenderOutfit(type.ToString(), _baseOutfit.GetAllGenderData(type.ToString()));
                break;
        }
    }

    private void ActiveChange(bool val)
    {
        Get<Slider>((int)Sliders.Facial).gameObject.SetActive(val);
        Get<TMP_Text>((int)Texts.FacialText).gameObject.SetActive(val);

        if (val == true)
        {
            Get<Slider>((int)Sliders.Eyebrows).maxValue = _outfit.GetGenderPartCount(Define.HumanOutfitOneGender.Eyebrows)-1;
        }
        else
        {
            int eyebrowCount = _outfit.GetGenderPartCount(Define.HumanGender.Female, Define.HumanOutfitOneGender.Eyebrows);
            Get<Slider>((int)Sliders.Eyebrows).maxValue = eyebrowCount;

            if (_baseOutfit.OneGender[(int)Define.HumanOutfitOneGender.Eyebrows] >= _outfit.GetGenderPartCount(Define.HumanGender.Female, Define.HumanOutfitOneGender.Eyebrows))
            {
                int value = eyebrowCount - 1;
                Debug.Log(value);
                _baseOutfit.SetOneGenderData(Define.HumanOutfitOneGender.Eyebrows, value);
                _outfit.ChangeGenderOutfit(Define.HumanOutfitOneGender.Eyebrows, _baseOutfit.GetOneGenderData(Define.HumanOutfitOneGender.Eyebrows));
            }
        }
    }

}
