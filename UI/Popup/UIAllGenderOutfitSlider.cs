using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAllGenderOutfitSlider : UIOutfitSliderBase
{
    enum Sliders
    {
        HeadCoveringBase,
        HeadCoveringMask,
        HeadCoveringNoHair,
        AllGenderHeadAttachment,

        AllGenderBackAttachment,
        AllGenderShoulderRight,
        AllGenderShoulderLeft,
        AllGenderElbowRight,

        AllGenderElbowLeft,
        AllGenderHips,
        AllGenderKneeRight,
        AllGenderKneeLeft,
    }

    public override void Init()
    {
        Bind<Slider>(typeof(Sliders));

        MaxValueChange();

        AddListener();
    }

    //public override void SetCharacter(HumanOutfit outfit, CharacterOutfit character)
    //{
    //    base.SetCharacter(outfit, character);
    //}

    protected override void AddListener()
    {
        Get<Slider>((int)Sliders.HeadCoveringBase).onValueChanged.AddListener(HeadCoveringBaseListener);
        Get<Slider>((int)Sliders.HeadCoveringMask).onValueChanged.AddListener(HeadCoveringMaskListener);
        Get<Slider>((int)Sliders.HeadCoveringNoHair).onValueChanged.AddListener(HeadCoveringNoHairListener);
        Get<Slider>((int)Sliders.AllGenderHeadAttachment).onValueChanged.AddListener(AllGenderHeadAttachmentListener);

        Get<Slider>((int)Sliders.AllGenderBackAttachment).onValueChanged.AddListener(AllGenderBackAttachmentListener);
        Get<Slider>((int)Sliders.AllGenderShoulderRight).onValueChanged.AddListener(AllGenderShoulderRightListener);
        Get<Slider>((int)Sliders.AllGenderShoulderLeft).onValueChanged.AddListener(AllGenderShoulderLeftListener);
        Get<Slider>((int)Sliders.AllGenderElbowRight).onValueChanged.AddListener(AllGenderElbowRightListener);

        Get<Slider>((int)Sliders.AllGenderElbowLeft).onValueChanged.AddListener(AllGenderElbowLeftListener);
        Get<Slider>((int)Sliders.AllGenderHips).onValueChanged.AddListener(AllGenderHipsListener);
        Get<Slider>((int)Sliders.AllGenderKneeRight).onValueChanged.AddListener(AllGenderKneeRightListener);
        Get<Slider>((int)Sliders.AllGenderKneeLeft).onValueChanged.AddListener(AllGenderKneeLeftListener);
    }

    protected override void MaxValueChange()
    {
        //value
        Get<Slider>((int)Sliders.HeadCoveringBase).value = _outfit.GetAllGenderData(Define.HumanOutfitAllGender.HeadCoveringBase);
        Get<Slider>((int)Sliders.HeadCoveringMask).value = _outfit.GetAllGenderData(Define.HumanOutfitAllGender.HeadCoveringMask);
        Get<Slider>((int)Sliders.HeadCoveringNoHair).value = _outfit.GetAllGenderData(Define.HumanOutfitAllGender.HeadCoveringNoHair);
        Get<Slider>((int)Sliders.AllGenderHeadAttachment).value = _outfit.GetAllGenderData(Define.HumanOutfitAllGender.AllGenderHeadAttachment);

        Get<Slider>((int)Sliders.AllGenderBackAttachment).value = _outfit.GetAllGenderData(Define.HumanOutfitAllGender.AllGenderBackAttachment);
        Get<Slider>((int)Sliders.AllGenderShoulderRight).value = _outfit.GetAllGenderData(Define.HumanOutfitAllGender.AllGenderShoulderRight);
        Get<Slider>((int)Sliders.AllGenderShoulderLeft).value = _outfit.GetAllGenderData(Define.HumanOutfitAllGender.AllGenderShoulderLeft);
        Get<Slider>((int)Sliders.AllGenderElbowRight).value = _outfit.GetAllGenderData(Define.HumanOutfitAllGender.AllGenderElbowRight);

        Get<Slider>((int)Sliders.AllGenderElbowLeft).value = _outfit.GetAllGenderData(Define.HumanOutfitAllGender.AllGenderElbowLeft);
        Get<Slider>((int)Sliders.AllGenderHips).value = _outfit.GetAllGenderData(Define.HumanOutfitAllGender.AllGenderHips);
        Get<Slider>((int)Sliders.AllGenderKneeRight).value = _outfit.GetAllGenderData(Define.HumanOutfitAllGender.AllGenderKneeRight);
        Get<Slider>((int)Sliders.AllGenderKneeLeft).value = _outfit.GetAllGenderData(Define.HumanOutfitAllGender.AllGenderKneeLeft);

        //minvalue
        for (int i = 0; i <= (int)Sliders.AllGenderKneeLeft; i++)
            Get<Slider>(i).minValue = -1;
        //Get<Slider>((int)Sliders.HeadCoveringBase).maxValue = -1;
        //Get<Slider>((int)Sliders.HeadCoveringMask).maxValue = -1;
        //Get<Slider>((int)Sliders.HeadCoveringNoHair).maxValue = -1;
        //Get<Slider>((int)Sliders.AllGenderHeadAttachment).maxValue = _charOutfit.HeadAttachCount();
        //
        //Get<Slider>((int)Sliders.AllGenderBackAttachment).maxValue = _charOutfit.BackAttachCount();
        //Get<Slider>((int)Sliders.AllGenderShoulderRight).maxValue = _charOutfit.ShoulderRightCount();
        //Get<Slider>((int)Sliders.AllGenderShoulderLeft).maxValue = _charOutfit.ShoulderLeftCount();
        //Get<Slider>((int)Sliders.AllGenderElbowRight).maxValue = _charOutfit.ElbowRightCount();
        //
        //Get<Slider>((int)Sliders.AllGenderElbowLeft).maxValue = _charOutfit.ElbowLeftCount();
        //Get<Slider>((int)Sliders.AllGenderHips).maxValue = _charOutfit.AllHipCount();
        //Get<Slider>((int)Sliders.AllGenderKneeRight).maxValue = _charOutfit.KneeRightCount();
        //Get<Slider>((int)Sliders.AllGenderKneeLeft).maxValue = _charOutfit.KneeLeftCount();

        //maxvalue
        Get<Slider>((int)Sliders.HeadCoveringBase).maxValue = _charOutfit.HeadCoverBaseSizeCount()-1;
        Get<Slider>((int)Sliders.HeadCoveringMask).maxValue = _charOutfit.HeadCoverMaskSizeCount()-1;
        Get<Slider>((int)Sliders.HeadCoveringNoHair).maxValue = _charOutfit.HeadCoverNoHairCount()-1;
        Get<Slider>((int)Sliders.AllGenderHeadAttachment).maxValue = _charOutfit.HeadAttachCount()-1;

        Get<Slider>((int)Sliders.AllGenderBackAttachment).maxValue = _charOutfit.BackAttachCount()-1;
        Get<Slider>((int)Sliders.AllGenderShoulderRight).maxValue = _charOutfit.ShoulderRightCount()-1;
        Get<Slider>((int)Sliders.AllGenderShoulderLeft).maxValue = _charOutfit.ShoulderLeftCount()-1;
        Get<Slider>((int)Sliders.AllGenderElbowRight).maxValue = _charOutfit.ElbowRightCount()-1;

        Get<Slider>((int)Sliders.AllGenderElbowLeft).maxValue = _charOutfit.ElbowLeftCount()-1;
        Get<Slider>((int)Sliders.AllGenderHips).maxValue = _charOutfit.AllHipCount()-1;
        Get<Slider>((int)Sliders.AllGenderKneeRight).maxValue = _charOutfit.KneeRightCount()-1;
        Get<Slider>((int)Sliders.AllGenderKneeLeft).maxValue = _charOutfit.KneeLeftCount()-1;

        
    }

    #region Add Listener Function

     private void HeadCoveringBaseListener(float value)
    {
        SliderValueChange(Sliders.HeadCoveringBase, value);
    }
    private void HeadCoveringMaskListener(float value)
    {

        SliderValueChange(Sliders.HeadCoveringMask, value);
    }
    private void HeadCoveringNoHairListener(float value)
    {

        SliderValueChange(Sliders.HeadCoveringNoHair, value);
    }
    private void AllGenderHeadAttachmentListener(float value)
    {
        SliderValueChange(Sliders.AllGenderHeadAttachment, value);
    }
    private void AllGenderBackAttachmentListener(float value)
    {
        SliderValueChange(Sliders.AllGenderBackAttachment, value);
    }
    private void AllGenderShoulderRightListener(float value)
    {
        SliderValueChange(Sliders.AllGenderShoulderRight, value);
    }
    private void AllGenderShoulderLeftListener(float value)
    {
        SliderValueChange(Sliders.AllGenderShoulderLeft, value);
    }
    private void AllGenderElbowRightListener(float value)
    {
        SliderValueChange(Sliders.AllGenderElbowRight, value);
    }
    private void AllGenderElbowLeftListener(float value)
    {
        SliderValueChange(Sliders.AllGenderElbowLeft, value);
    }
    private void AllGenderHipsListener(float value)
    {
        SliderValueChange(Sliders.AllGenderHips, value);
    }
    private void AllGenderKneeRightListener(float value)
    {
        SliderValueChange(Sliders.AllGenderKneeRight, value);
    }
    private void AllGenderKneeLeftListener(float value)
    {
        SliderValueChange(Sliders.AllGenderKneeLeft, value);
    }
    #endregion

    private void SliderValueChange(Sliders type, float value)
    {
        int val = Mathf.RoundToInt(Get<Slider>((int)type).value);
        Get<Slider>((int)type).value = value;

        _outfit.SetAllGenderData(type.ToString(), val);
        _charOutfit.ChangeAllGenderOutfit(type.ToString(), _outfit.GetAllGenderData(type.ToString()));
    }
}
