using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOneGenderOutfitSlider : UIOutfitSliderBase
{
    enum Sliders
    {
        Facial,
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
        Bind<Slider>(typeof(Sliders));

        MaxValueChange();

        if (_outfit.Gender == Define.HumanGender.Female)
        {
            Get<Slider>((int)Sliders.Facial).gameObject.SetActive(false);
        }

        AddListener();
    }

    //public override void SetCharacter(HumanOutfit outfit, CharacterOutfit character)
    //{
    //    base.SetCharacter(outfit, character);
    //}


    protected override void AddListener()
    {
        Get<Slider>((int)Sliders.HeadGear).onValueChanged.AddListener(HeadgearChanged);
        Get<Slider>((int)Sliders.Facial).onValueChanged.AddListener(FacialChanged);
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
        Get<Slider>((int)Sliders.HeadGear).maxValue = _charOutfit.MaleHeadGearCount()-1;

        Get<Slider>((int)Sliders.Facial).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.Facial);
        Get<Slider>((int)Sliders.Facial).minValue = -1;
        Get<Slider>((int)Sliders.Facial).maxValue = _charOutfit.MaleFaciallCount()-1;

        Get<Slider>((int)Sliders.Torso).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.Torso);
        //Get<Slider>((int)Sliders.Torso).minValue = -1;
        Get<Slider>((int)Sliders.Torso).maxValue = _charOutfit.MaleTorsoCount()-1;

        Get<Slider>((int)Sliders.ArmUpperRight).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.ArmUpperRight);
        //Get<Slider>((int)Sliders.ArmUpperRight).minValue = -1;
        Get<Slider>((int)Sliders.ArmUpperRight).maxValue = _charOutfit.MaleArmUpperRightCount()-1;

        Get<Slider>((int)Sliders.ArmUpperLeft).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.ArmUpperLeft);
        //Get<Slider>((int)Sliders.ArmUpperLeft).minValue = -1;
        Get<Slider>((int)Sliders.ArmUpperLeft).maxValue = _charOutfit.MaleArmUpperLeftCount()-1;

        Get<Slider>((int)Sliders.ArmLowerRight).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.ArmLowerRight);
        //Get<Slider>((int)Sliders.ArmLowerRight).minValue = -1;
        Get<Slider>((int)Sliders.ArmLowerRight).maxValue = _charOutfit.MaleArmLowerRightCount()-1;

        Get<Slider>((int)Sliders.ArmLowerLeft).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.ArmLowerLeft);
        //Get<Slider>((int)Sliders.ArmLowerLeft).minValue = -1;
        Get<Slider>((int)Sliders.ArmLowerLeft).maxValue = _charOutfit.MaleArmLowerLeftCount()-1;

        Get<Slider>((int)Sliders.HandRight).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.HandRight);
        //Get<Slider>((int)Sliders.HandRight).minValue = -1;
        Get<Slider>((int)Sliders.HandRight).maxValue = _charOutfit.MaleHandRightCount()-1;

        Get<Slider>((int)Sliders.HandLeft).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.HandLeft);
        //Get<Slider>((int)Sliders.HandLeft).minValue = -1;
        Get<Slider>((int)Sliders.HandLeft).maxValue = _charOutfit.MaleHandLeftCount()-1;

        Get<Slider>((int)Sliders.Hips).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.Hips);
        //Get<Slider>((int)Sliders.Hips).minValue = -1;
        Get<Slider>((int)Sliders.Hips).maxValue = _charOutfit.MaleHipsCount()-1;

        Get<Slider>((int)Sliders.LegRight).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.LegRight);
        //Get<Slider>((int)Sliders.LegRight).minValue = -1;
        Get<Slider>((int)Sliders.LegRight).maxValue = _charOutfit.MaleLegRightCount()-1;

        Get<Slider>((int)Sliders.LegLeft).value = _outfit.GetOneGenderData(Define.HumanOutfitOneGender.LegLeft);
        //Get<Slider>((int)Sliders.LegLeft).minValue = -1;
        Get<Slider>((int)Sliders.LegLeft).maxValue = _charOutfit.MaleLegLeftCount()-1;
    }

    #region slider value change listner
    private void HeadgearChanged(float value)
    {
        SliderValueChange(Sliders.HeadGear, value);
    }

    private void FacialChanged(float value)
    {
        SliderValueChange(Sliders.Facial, value);
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
        Get<Slider>((int)type).value = value;

        _outfit.SetOneGenderData(type.ToString(), val);
        _charOutfit.ChangeGenderOutfit(type.ToString(), _outfit.GetOneGenderData(type.ToString()));
    }
}
