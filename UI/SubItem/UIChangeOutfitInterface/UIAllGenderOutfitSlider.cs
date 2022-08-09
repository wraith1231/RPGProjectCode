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

        AllGenderHair,
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

        ValueInit();

        AddListener();
    }


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
        Get<Slider>((int)Sliders.AllGenderHair).onValueChanged.AddListener(AllGenderHairListener);
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
        Get<Slider>((int)Sliders.AllGenderHair).value = _outfit.GetAllGenderData(Define.HumanOutfitAllGender.AllGenderHair);

        //minvalue
        for (int i = 0; i <= (int)Sliders.AllGenderHair; i++)
            Get<Slider>(i).minValue = -1;

        //maxvalue
        Get<Slider>((int)Sliders.HeadCoveringBase).maxValue = _charOutfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.HeadCoveringBase)-1;
        Get<Slider>((int)Sliders.HeadCoveringMask).maxValue = _charOutfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.HeadCoveringMask) - 1;
        Get<Slider>((int)Sliders.HeadCoveringNoHair).maxValue = _charOutfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.HeadCoveringNoHair) - 1;
        Get<Slider>((int)Sliders.AllGenderHeadAttachment).maxValue = _charOutfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.AllGenderHeadAttachment) - 1;

        Get<Slider>((int)Sliders.AllGenderBackAttachment).maxValue = _charOutfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.AllGenderBackAttachment) - 1;
        Get<Slider>((int)Sliders.AllGenderShoulderRight).maxValue = _charOutfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.AllGenderShoulderRight) - 1;
        Get<Slider>((int)Sliders.AllGenderShoulderLeft).maxValue = _charOutfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.AllGenderShoulderLeft) - 1;
        Get<Slider>((int)Sliders.AllGenderElbowRight).maxValue = _charOutfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.AllGenderElbowRight) - 1;

        Get<Slider>((int)Sliders.AllGenderElbowLeft).maxValue = _charOutfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.AllGenderElbowLeft) - 1;
        Get<Slider>((int)Sliders.AllGenderHips).maxValue = _charOutfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.AllGenderHips) - 1;
        Get<Slider>((int)Sliders.AllGenderKneeRight).maxValue = _charOutfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.AllGenderKneeRight) - 1;
        Get<Slider>((int)Sliders.AllGenderKneeLeft).maxValue = _charOutfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.AllGenderKneeLeft) - 1;
        Get<Slider>((int)Sliders.AllGenderHair).maxValue = _charOutfit.GetAllGenderPartCount(Define.HumanOutfitAllGender.AllGenderHair) - 1;
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
    private void AllGenderHairListener(float value)
    {
        SliderValueChange(Sliders.AllGenderHair, value);
    }
    #endregion

    private void SliderValueChange(Sliders type, float value)
    {
        int val = Mathf.RoundToInt(Get<Slider>((int)type).value);
        Get<Slider>((int)type).value = val;
        
        _outfit.SetAllGenderData(type.ToString(), val);
        _charOutfit.ChangeAllGenderOutfit(type.ToString(), _outfit.GetAllGenderData(type.ToString()));

        _parentUI.RefreshNeedString();
    }

    public void ValueInit()
    {
        int size = (int)Sliders.AllGenderHair;
        for(int i=0; i <= size; i++)
        {
            string type = ((Sliders)i).ToString();
            _outfit.SetAllGenderData(type, _baseOutfit.GetAllGenderData(type));
            Get<Slider>(i).value = _outfit.GetAllGenderData(type);
        }
    }

    public int CompareDeference()
    {
        int size = (int)Sliders.AllGenderHair;
        int ret = 0;
        for (int i = 0; i <= size; i++)
        {
            string type = ((Sliders)i).ToString();
            if (_outfit.CompareDeferenceAllGender(type, _baseOutfit.GetAllGenderData(type)) == false)
                ret++;
        }

        return ret;
    }
}
