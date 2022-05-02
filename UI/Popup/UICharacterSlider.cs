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
        HeadSlider,
        HairSlider,
        EyebrowSlider,
        ExtraSlider,
        FacialSlider,
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

    public override void Init()
    {
        Bind<Slider>(typeof(Sliders));
        Bind<TMP_Text>(typeof(Texts));

        MaxValueChange();
    }

    private void MaxValueChange()
    {
        Get<Slider>((int)Sliders.GenderSlider).value = 1;
        Get<Slider>((int)Sliders.GenderSlider).maxValue = 1;
        Get<Slider>((int)Sliders.HeadSlider).maxValue = _outfit.MaleHeadCount();
        Get<Slider>((int)Sliders.HairSlider).maxValue = _outfit.HairCount();
        Get<Slider>((int)Sliders.EyebrowSlider).maxValue = _outfit.MaleEyebrowsCount();
        Get<Slider>((int)Sliders.ExtraSlider).maxValue = _outfit.ExtraCount();
        Get<Slider>((int)Sliders.FacialSlider).maxValue = _outfit.MaleFaciallCount();
    }

    public void GenderChange()
    {
        if (_outfit == null)
            return;

        int value = Mathf.RoundToInt( Get<Slider>((int)Sliders.GenderSlider).value);
        Get<Slider>((int)Sliders.GenderSlider).value = value;

        _baseOutfit.Gender = (Define.HumanGender)value;
        _outfit.ChangeGender(_baseOutfit.Gender);

        ActiveChange(_baseOutfit.Gender == Define.HumanGender.Male ? true : false);
    }

    private void ActiveChange(bool val)
    {
        Get<Slider>((int)Sliders.FacialSlider).gameObject.SetActive(val);
        Get<TMP_Text>((int)Texts.FacialText).gameObject.SetActive(val);

        if (val == true)
        {
            Get<Slider>((int)Sliders.EyebrowSlider).maxValue = _outfit.MaleEyebrowsCount();
        }
        else
        {
            if (_baseOutfit.OneGender[(int)Define.HumanOutfitOneGender.Eyebrows] >= _outfit.FemaleEyebrowsCount())
            {
                int value = _outfit.FemaleEyebrowsCount() - 1;
                _baseOutfit.SetOneGenderData(Define.HumanOutfitOneGender.Eyebrows, value);
                _outfit.ChangeGenderOutfit(Define.HumanOutfitOneGender.Eyebrows, _baseOutfit.GetOneGenderData(Define.HumanOutfitOneGender.Eyebrows));
            }
            Get<Slider>((int)Sliders.EyebrowSlider).maxValue = _outfit.FemaleEyebrowsCount();
        }
    }

    public void HeadChange()
    {
        if (_outfit == null)
            return;

        int value = Mathf.RoundToInt(Get<Slider>((int)Sliders.HeadSlider).value);
        Get<Slider>((int)Sliders.HeadSlider).value = value;

        if (value >= _outfit.FemaleHeadCount())
            value = 0;

        _baseOutfit.SetOneGenderData(Define.HumanOutfitOneGender.Head, value);
        _outfit.ChangeGenderOutfit(Define.HumanOutfitOneGender.Head, _baseOutfit.GetOneGenderData(Define.HumanOutfitOneGender.Head));
    }

    public void HairChange()
    {
        if (_outfit == null)
            return;

        int value = Mathf.RoundToInt(Get<Slider>((int)Sliders.HairSlider).value);
        Get<Slider>((int)Sliders.HairSlider).value = value;

        if (value >= _outfit.HairCount())
            value = -1;

        _baseOutfit.SetAllGenderData(Define.HumanOutfitAllGender.AllGenderHair, value);
        _outfit.ChangeAllGenderOutfit(Define.HumanOutfitAllGender.AllGenderHair, _baseOutfit.GetAllGenderData(Define.HumanOutfitAllGender.AllGenderHair));
    }
    
    public void EyebrowChange()
    {
        if (_outfit == null)
            return;

        int value = Mathf.RoundToInt(Get<Slider>((int)Sliders.EyebrowSlider).value);
        Get<Slider>((int)Sliders.EyebrowSlider).value = value;

        if (_outfit.Gender == Define.HumanGender.Female)
        {
            if (value >= _outfit.FemaleEyebrowsCount())
                value = -1;
        }
        else
        {
            if (value >= _outfit.MaleEyebrowsCount())
                value = -1;
        }
        _baseOutfit.SetOneGenderData(Define.HumanOutfitOneGender.Eyebrows, value);
        _outfit.ChangeGenderOutfit(Define.HumanOutfitOneGender.Eyebrows, _baseOutfit.GetOneGenderData(Define.HumanOutfitOneGender.Eyebrows));
    }
    public void ExtraChange()
    {
        if (_outfit == null)
            return;

        int value = Mathf.RoundToInt(Get<Slider>((int)Sliders.ExtraSlider).value);
        Get<Slider>((int)Sliders.ExtraSlider).value = value;

        if (value >= _outfit.ExtraCount())
            value = -1;

        _baseOutfit.SetAllGenderData(Define.HumanOutfitAllGender.AllGenderExtra, value);
        _outfit.ChangeAllGenderOutfit(Define.HumanOutfitAllGender.AllGenderExtra, _baseOutfit.GetAllGenderData(Define.HumanOutfitAllGender.AllGenderExtra));
    }
    
    public void FacialChange()
    {
        if (_outfit == null)
            return;

        int value = Mathf.RoundToInt(Get<Slider>((int)Sliders.FacialSlider).value);
        Get<Slider>((int)Sliders.FacialSlider).value = value;

        if (value >= _outfit.MaleFaciallCount())
            value = -1;

        _baseOutfit.SetOneGenderData(Define.HumanOutfitOneGender.Facial, value);
        _outfit.ChangeGenderOutfit(Define.HumanOutfitOneGender.Facial, _baseOutfit.GetOneGenderData(Define.HumanOutfitOneGender.Facial));
    }
}
