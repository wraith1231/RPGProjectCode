using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOneGenderOutfitSlider : UIOutfitSliderBase
{
    enum Sliders
    {
        HeadGear,
        Torso,
        ArmUpperRight,

        ArmUpperLeft,
        ArmLowerRight,
        ArmLowerLeft,
        HandRight,

        HandLeft,
        Hips,
        LegRight,
        LegLeft,
    }

    public override void Init()
    {
        if (_isInit == true)
            return;

        Bind<Slider>(typeof(Sliders));

        _isInit = true;
    }

    public void SliderInit()
    {
        Init();

        MaxValueChange();

        AddListener();
    }

    protected override void AddListener()
    {
        Get<Slider>((int)Sliders.HeadGear).onValueChanged.AddListener(HeadgearChanged);
        Get<Slider>((int)Sliders.Torso).onValueChanged.AddListener(TorsoChanged);
        Get<Slider>((int)Sliders.ArmUpperRight).onValueChanged.AddListener(ArmUpperRightChanged);

        Get<Slider>((int)Sliders.ArmUpperLeft).onValueChanged.AddListener(ArmUpperLeftChanged);
        Get<Slider>((int)Sliders.ArmLowerRight).onValueChanged.AddListener(ArmLowerRightChanged);
        Get<Slider>((int)Sliders.ArmLowerLeft).onValueChanged.AddListener(ArmLowerLeftChanged);
        Get<Slider>((int)Sliders.HandRight).onValueChanged.AddListener(HandRightChanged);

        Get<Slider>((int)Sliders.HandLeft).onValueChanged.AddListener(HandLeftChanged);
        Get<Slider>((int)Sliders.Hips).onValueChanged.AddListener(HipsChanged);
        Get<Slider>((int)Sliders.LegRight).onValueChanged.AddListener(LegRightChanged);
        Get<Slider>((int)Sliders.LegLeft).onValueChanged.AddListener(LegLeftChanged);
    }

    protected override void MaxValueChange()
    {
        Get<Slider>((int)Sliders.HeadGear).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.HeadGear);
        Get<Slider>((int)Sliders.HeadGear).minValue = -1;
        Get<Slider>((int)Sliders.HeadGear).maxValue = _charOutfit.GetGenderPartCount(Define.HumanOutfitOneGender.HeadGear)-1;

        Get<Slider>((int)Sliders.Torso).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.Torso);
        //Get<Slider>((int)Sliders.Torso).minValue = -1;
        Get<Slider>((int)Sliders.Torso).maxValue = _charOutfit.GetGenderPartCount(Define.HumanOutfitOneGender.Torso) - 1;

        Get<Slider>((int)Sliders.ArmUpperRight).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.ArmUpperRight);
        //Get<Slider>((int)Sliders.ArmUpperRight).minValue = -1;
        Get<Slider>((int)Sliders.ArmUpperRight).maxValue = _charOutfit.GetGenderPartCount(Define.HumanOutfitOneGender.ArmUpperRight) - 1;

        Get<Slider>((int)Sliders.ArmUpperLeft).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.ArmUpperLeft);
        //Get<Slider>((int)Sliders.ArmUpperLeft).minValue = -1;
        Get<Slider>((int)Sliders.ArmUpperLeft).maxValue = _charOutfit.GetGenderPartCount(Define.HumanOutfitOneGender.ArmUpperLeft) - 1;

        Get<Slider>((int)Sliders.ArmLowerRight).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.ArmLowerRight);
        //Get<Slider>((int)Sliders.ArmLowerRight).minValue = -1;
        Get<Slider>((int)Sliders.ArmLowerRight).maxValue = _charOutfit.GetGenderPartCount(Define.HumanOutfitOneGender.ArmLowerRight) - 1;

        Get<Slider>((int)Sliders.ArmLowerLeft).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.ArmLowerLeft);
        //Get<Slider>((int)Sliders.ArmLowerLeft).minValue = -1;
        Get<Slider>((int)Sliders.ArmLowerLeft).maxValue = _charOutfit.GetGenderPartCount(Define.HumanOutfitOneGender.ArmLowerLeft) - 1;

        Get<Slider>((int)Sliders.HandRight).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.HandRight);
        //Get<Slider>((int)Sliders.HandRight).minValue = -1;
        Get<Slider>((int)Sliders.HandRight).maxValue = _charOutfit.GetGenderPartCount(Define.HumanOutfitOneGender.HandRight) - 1;

        Get<Slider>((int)Sliders.HandLeft).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.HandLeft);
        //Get<Slider>((int)Sliders.HandLeft).minValue = -1;
        Get<Slider>((int)Sliders.HandLeft).maxValue = _charOutfit.GetGenderPartCount(Define.HumanOutfitOneGender.HandLeft) - 1;

        Get<Slider>((int)Sliders.Hips).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.Hips);
        //Get<Slider>((int)Sliders.Hips).minValue = -1;
        Get<Slider>((int)Sliders.Hips).maxValue = _charOutfit.GetGenderPartCount(Define.HumanOutfitOneGender.Hips) - 1;

        Get<Slider>((int)Sliders.LegRight).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.LegRight);
        //Get<Slider>((int)Sliders.LegRight).minValue = -1;
        Get<Slider>((int)Sliders.LegRight).maxValue = _charOutfit.GetGenderPartCount(Define.HumanOutfitOneGender.LegRight) - 1;

        Get<Slider>((int)Sliders.LegLeft).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.LegLeft);
        //Get<Slider>((int)Sliders.LegLeft).minValue = -1;
        Get<Slider>((int)Sliders.LegLeft).maxValue = _charOutfit.GetGenderPartCount(Define.HumanOutfitOneGender.LegLeft) - 1;
    }

    #region slider value change listner
    private void HeadgearChanged(float value)
    {
        SliderValueChange(Sliders.HeadGear, value);
    }

    private void TorsoChanged(float value)
    {
        SliderValueChange(Sliders.Torso, value);
    }

    private void ArmUpperRightChanged(float value)
    {
        SliderValueChange(Sliders.ArmUpperRight, value);
    }

    private void ArmUpperLeftChanged(float value)
    {
        SliderValueChange(Sliders.ArmUpperLeft, value);
    }

    private void ArmLowerRightChanged(float value)
    {
        SliderValueChange(Sliders.ArmLowerRight, value);
    }

    private void ArmLowerLeftChanged(float value)
    {
        SliderValueChange(Sliders.ArmLowerLeft, value);
    }

    private void HandRightChanged(float value)
    {
        SliderValueChange(Sliders.HandRight, value);
    }

    private void HandLeftChanged(float value)
    {
        SliderValueChange(Sliders.HandLeft, value);
    }

    private void HipsChanged(float value)
    {
        SliderValueChange(Sliders.Hips, value);
    }

    private void LegRightChanged(float value)
    {
        SliderValueChange(Sliders.LegRight, value);
    }

    private void LegLeftChanged(float value)
    {
        SliderValueChange(Sliders.LegLeft, value);
    }
    #endregion

    private void SliderValueChange(Sliders type, float value)
    {
        int val = Mathf.RoundToInt(Get<Slider>((int)type).value);
        Get<Slider>((int)type).value = val;

        _outfit.SetOneGenderData(type.ToString(), val);
        _charOutfit.ChangeGenderOutfit(type.ToString(), _outfit.GetOneGenderData(type.ToString()));

        _parentUI.RefreshNeedString();
    }

    public void ValueInit()
    {
        int size = (int)Sliders.LegLeft;
        for (int i = 0; i <= size; i++)
        {
            string type = ((Sliders)i).ToString();
            _outfit.SetOneGenderData(type, _baseOutfit.GetOneGenderData(type));
            Get<Slider>(i).value = _outfit.GetOneGenderData(type);
        }
    }
    public int CompareDeference()
    {
        int size = (int)Sliders.LegLeft;
        int ret = 0;
        for (int i = 0; i <= size; i++)
        {
            string type = ((Sliders)i).ToString();
            if (_outfit.CompareDeferenceGender(type, _baseOutfit.GetOneGenderData(type)) == false)
                ret++;
        }

        return ret;
    }
}
