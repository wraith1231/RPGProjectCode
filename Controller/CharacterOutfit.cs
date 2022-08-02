using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOutfit : MonoBehaviour
{
    private bool _isInit = false;
    private Define.HumanGender _gender = Define.HumanGender.Unknown;
    public Define.HumanGender Gender { get { return _gender; } set { ChangeGender(value); } }
    private List<int> _currentAllGenderOutfit = new List<int>();
    private List<int> _currentGenderOutfit = new List<int>();

    private int _prevAllGenderHeadBase = -1;
    private int _prevAllGenderMask = -1;
    private int _prevAllGenderNoHair = -1;
    private int _prevAllGenderHair = -1;
    private int _prevExtra = -1;

    private int _prevHeadGear = -1;
    private int _prevHead = -1;
    private int _prevFacial = -1;

    [SerializeField] private List<GameObject> _genderParts;


    #region All Gender
    [SerializeField] private List<GameObject> _headCoveringsBase;
    public int HeadCoverBaseSizeCount() { return _headCoveringsBase.Count; }

    [SerializeField] private List<GameObject> _headCoveringsMask;
    public int HeadCoverMaskSizeCount() { return _headCoveringsMask.Count; }

    [SerializeField] private List<GameObject> _headCoveringsNoHair;
    public int HeadCoverNoHairCount() { return _headCoveringsNoHair.Count; }

    [SerializeField] private List<GameObject> _allGenderHair;
    public int HairCount() { return _allGenderHair.Count; }

    [SerializeField] private List<GameObject> _allGenderHeadAttachment;
    public int HeadAttachCount() { return _allGenderHeadAttachment.Count; }

    [SerializeField] private List<GameObject> _allGenderBackAttachment;
    public int BackAttachCount() { return _allGenderBackAttachment.Count; }

    [SerializeField] private List<GameObject> _allGenderShoulderRight;
    public int ShoulderRightCount() { return _allGenderShoulderRight.Count; }

    [SerializeField] private List<GameObject> _allGenderShoulderLeft;
    public int ShoulderLeftCount() { return _allGenderShoulderLeft.Count; }

    [SerializeField] private List<GameObject> _allGenderElbowRight;
    public int ElbowRightCount() { return _allGenderElbowRight.Count; }

    [SerializeField] private List<GameObject> _allGenderElbowLeft;
    public int ElbowLeftCount() { return _allGenderElbowLeft.Count; }

    [SerializeField] private List<GameObject> _allGenderHips;
    public int AllHipCount() { return _allGenderHips.Count; }

    [SerializeField] private List<GameObject> _allGenderKneeRight;
    public int KneeRightCount() { return _allGenderKneeRight.Count; }

    [SerializeField] private List<GameObject> _allGenderKneeLeft;
    public int KneeLeftCount() { return _allGenderKneeLeft.Count; }

    [SerializeField] private List<GameObject> _allGenderExtra;
    public int ExtraCount() { return _allGenderExtra.Count; }
    #endregion

    #region Female
    [SerializeField] private List<GameObject> _femaleHead;
    [SerializeField] private List<GameObject> _femaleHeadGear;
    [SerializeField] private List<GameObject> _femaleEyebrows;
    [SerializeField] private List<GameObject> _femaleTorso;
    [SerializeField] private List<GameObject> _femaleArmUpperRight;
    [SerializeField] private List<GameObject> _femaleArmUpperLeft;
    [SerializeField] private List<GameObject> _femaleArmLowerRight;
    [SerializeField] private List<GameObject> _femaleArmLowerLeft;
    [SerializeField] private List<GameObject> _femaleHandRight;
    [SerializeField] private List<GameObject> _femaleHandLeft;
    [SerializeField] private List<GameObject> _femaleHips;
    [SerializeField] private List<GameObject> _femaleLegRight;
    [SerializeField] private List<GameObject> _femaleLegLeft;

    public int FemaleHeadCount() { return _femaleHead.Count; }
    public int FemaleHeadGearCount() { return _femaleHeadGear.Count; }
    public int FemaleEyebrowsCount() { return _femaleEyebrows.Count; }
    public int FemaleTorsoCount() { return _femaleTorso.Count; }
    public int FemaleArmUpperRightCount() { return _femaleArmUpperRight.Count; }
    public int FemaleArmUpperLeftCount() { return _femaleArmUpperLeft.Count; }
    public int FemaleArmLowerRightCount() { return _femaleArmLowerRight.Count; }
    public int FemaleArmLowerLeftCount() { return _femaleArmLowerLeft.Count; }
    public int FemaleHandRightCount() { return _femaleHandRight.Count; }
    public int FemaleHandLeftCount() { return _femaleHandLeft.Count; }
    public int FemaleHipsCount() { return _femaleHips.Count; }
    public int FemaleLegRightCount() { return _femaleLegRight.Count; }
    public int FemaleLegLeftCount() { return _femaleLegLeft.Count; }
    #endregion

    #region Male
    [SerializeField] private List<GameObject> _maleHead;
    [SerializeField] private List<GameObject> _maleHeadGear;
    [SerializeField] private List<GameObject> _maleEyebrows;
    [SerializeField] private List<GameObject> _maleFacial;
    [SerializeField] private List<GameObject> _maleTorso;
    [SerializeField] private List<GameObject> _maleArmUpperRight;
    [SerializeField] private List<GameObject> _maleArmUpperLeft;
    [SerializeField] private List<GameObject> _maleArmLowerRight;
    [SerializeField] private List<GameObject> _maleArmLowerLeft;
    [SerializeField] private List<GameObject> _maleHandRight;
    [SerializeField] private List<GameObject> _maleHandLeft;
    [SerializeField] private List<GameObject> _maleHips;
    [SerializeField] private List<GameObject> _maleLegRight;
    [SerializeField] private List<GameObject> _maleLegLeft;

    public int MaleHeadCount() { return _maleHead.Count; }
    public int MaleHeadGearCount() { return _maleHeadGear.Count; }
    public int MaleEyebrowsCount() { return _maleEyebrows.Count; }
    public int MaleFaciallCount() { return _maleFacial.Count; }
    public int MaleTorsoCount() { return _maleTorso.Count; }
    public int MaleArmUpperRightCount() { return _maleArmUpperRight.Count; }
    public int MaleArmUpperLeftCount() { return _maleArmUpperLeft.Count; }
    public int MaleArmLowerRightCount() { return _maleArmLowerRight.Count; }
    public int MaleArmLowerLeftCount() { return _maleArmLowerLeft.Count; }
    public int MaleHandRightCount() { return _maleHandRight.Count; }
    public int MaleHandLeftCount() { return _maleHandLeft.Count; }
    public int MaleHipsCount() { return _maleHips.Count; }
    public int MaleLegRightCount() { return _maleLegRight.Count; }
    public int MaleLegLeftCount() { return _maleLegLeft.Count; }
    #endregion

    public CharacterOutfit()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit == true)
            return;

        int size = (int)Define.HumanOutfitAllGender.Unknown;
        for (int i = 0; i < size; i++)
            _currentAllGenderOutfit.Add(-1);

        size = (int)Define.HumanOutfitOneGender.Unknown;
        for (int i = 0; i < size; i++)
        {
            if (i == (int)Define.HumanOutfitOneGender.HeadGear)
                _currentGenderOutfit.Add(-1);
            else
                _currentGenderOutfit.Add(0);
        }
        _isInit = true;
    }

    public void ChangeGender(Define.HumanGender gender)
    {
        if(gender == Define.HumanGender.Unknown)
        {
            Debug.Log("Unknown Gender invalid!!!");
            return;
        }

        if(gender == _gender)
        {
            Debug.Log("Same Gender");
            return;
        }

        //현재 젠더의 모든 의상을 false함
        int size = (int)Define.HumanOutfitOneGender.Unknown;
        if (_gender != Define.HumanGender.Unknown)
        {
            for (int i = 0; i < size; i++)
            {
                if (_gender == Define.HumanGender.Male)
                    SetActiveMaleParts((Define.HumanOutfitOneGender)i, _currentGenderOutfit[i], false);
                else
                    SetActiveFemaleParts((Define.HumanOutfitOneGender)i, _currentGenderOutfit[i], false);
            }
            for (int i = 0; i < (int)Define.HumanGender.Unknown; i++)
                _genderParts[i].SetActive(false);
        }

        //성별 변경 후 다시 active함
        _gender = gender;
        for(int i = 0; i < size; i++)
        {
            if (_gender == Define.HumanGender.Male)
                SetActiveMaleParts((Define.HumanOutfitOneGender)i, _currentGenderOutfit[i], true);
            else
                SetActiveFemaleParts((Define.HumanOutfitOneGender)i, _currentGenderOutfit[i], true);
        }
        _genderParts[(int)_gender].SetActive(true);
    }


    public void SetOutfit(HumanOutfit outfit = null)
    {
        if (outfit == null)
            return;

        if (_isInit == false)
            Init();

        Gender = outfit.Gender;

        int size = (int)Define.HumanOutfitAllGender.Unknown;
        for(int i = 0; i < size; i++)
        {
            ChangeAllGenderOutfit((Define.HumanOutfitAllGender)i, outfit.AllGender[i]);
        }
        size = (int)Define.HumanOutfitOneGender.Unknown;
        for(int i = 0; i < size; i++)
        {
            ChangeGenderOutfit((Define.HumanOutfitOneGender)i, outfit.OneGender[i]);
        }
    }

    #region AllGenderOutfit
    public void ChangeAllGenderOutfit(Define.HumanOutfitAllGender part, int num)
    {
        //현재 착용한거 false
        SetActiveAllGenderParts(part, _currentAllGenderOutfit[(int)part], false);

        //바꿀거 true
        SetActiveAllGenderParts(part, num, true);
    }

    public void ChangeAllGenderOutfit(string type, int num)
    {
        Define.HumanOutfitAllGender part = Define.HumanOutfitAllGender.Unknown;
        int size = (int)part;
        for(int i = 0; i < size; i++)
        {
            if(type == ((Define.HumanOutfitAllGender)i).ToString())
            {
                part = (Define.HumanOutfitAllGender)i;
                break;
            }
        }

        ChangeAllGenderOutfit(part, num);
    }

    private void SetActiveFalse()
    {
        _prevAllGenderHeadBase = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringBase];
        if (_prevAllGenderHeadBase >= 0)
            _headCoveringsBase[_prevAllGenderHeadBase].SetActive(false);

        _prevAllGenderMask = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringMask];
        if (_prevAllGenderMask >= 0)
            _headCoveringsMask[_prevAllGenderMask].SetActive(false);

        _prevAllGenderNoHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringNoHair];
        if (_prevAllGenderNoHair >= 0)
            _headCoveringsNoHair[_prevAllGenderNoHair].SetActive(false);

        _prevAllGenderHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderHair];
        if (_prevAllGenderHair >= 0)
            _allGenderHair[_prevAllGenderHair].SetActive(false);

        _prevExtra = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderExtra];
        if (_prevExtra >= 0)
            _allGenderExtra[_prevExtra].SetActive(false);

        _prevHeadGear = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.HeadGear];
        if (_prevHeadGear >= 0)
            ChangeGenderOutfit(Define.HumanOutfitOneGender.HeadGear, -1);

        _prevHead = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.Head];
        if (_prevHead >= 0)
            ChangeGenderOutfit(Define.HumanOutfitOneGender.Head, -1);

        if(_gender == Define.HumanGender.Male)
        {
            _prevFacial = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.Facial];
            if (_prevFacial >= 0)
                ChangeGenderOutfit(Define.HumanOutfitOneGender.Facial, -1);
        }
            //_maleHead[_prevHead].SetActive(false);
    }

    private void ActiveDefaultGenderHeads(params Define.HumanOutfitOneGender[] ones)
    {
        int size = ones.Length;
        for(int i = 0; i < size; i++)
        {
            if (ones[i] == Define.HumanOutfitOneGender.Head)
                ChangeGenderOutfit(Define.HumanOutfitOneGender.Head, _prevHead);
            if (ones[i] == Define.HumanOutfitOneGender.Facial)
                ChangeGenderOutfit(Define.HumanOutfitOneGender.Facial, _prevFacial);
        }
    }
    private void ActiveDefaultAllHeads(params Define.HumanOutfitAllGender[] alls)
    {
        int size = alls.Length;
        for(int i = 0; i < size; i++)
        {
            if (alls[i] == Define.HumanOutfitAllGender.AllGenderHair)
                ChangeAllGenderOutfit(Define.HumanOutfitAllGender.AllGenderHair, _prevAllGenderHair);
            if (alls[i] == Define.HumanOutfitAllGender.AllGenderExtra)
                ChangeAllGenderOutfit(Define.HumanOutfitAllGender.AllGenderExtra, _prevExtra);
        }
    }

    private void SetActiveAllGenderParts(Define.HumanOutfitAllGender part, int num, bool active)
    {
        if (num >= 0)
        {
            switch (part)
            {
                case Define.HumanOutfitAllGender.HeadCoveringBase:
                    if (active == true)
                        SetActiveFalse();

                    _headCoveringsBase[num].SetActive(active);

                    ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderExtra);
                    ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Head, Define.HumanOutfitOneGender.Facial);

                    if (active == false)
                    {
                        ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair);
                    }
                    break;
                case Define.HumanOutfitAllGender.HeadCoveringMask:
                    if (active == true)
                        SetActiveFalse();

                    _headCoveringsMask[num].SetActive(active);

                    ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair, Define.HumanOutfitAllGender.AllGenderExtra);
                    ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Head);
                    if (active == false)
                    {
                        ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Facial);
                    }
                    break;
                case Define.HumanOutfitAllGender.HeadCoveringNoHair:
                    if (active == true)
                        SetActiveFalse();

                    _headCoveringsNoHair[num].SetActive(active);

                    ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Head, Define.HumanOutfitOneGender.Facial);
                    if (active == false)
                    {
                        ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair, Define.HumanOutfitAllGender.AllGenderExtra);
                    }
                    break;
                case Define.HumanOutfitAllGender.AllGenderHair:
                    if (active == true)
                        SetActiveFalse();

                    _allGenderHair[num].SetActive(active);

                    ActiveDefaultAllHeads( Define.HumanOutfitAllGender.AllGenderExtra);
                    ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Head, Define.HumanOutfitOneGender.Facial);
                    break;
                case Define.HumanOutfitAllGender.AllGenderHeadAttachment:
                    _allGenderHeadAttachment[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderBackAttachment:
                    _allGenderBackAttachment[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderShoulderRight:
                    _allGenderShoulderRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderShoulderLeft:
                    _allGenderShoulderLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderElbowRight:
                    _allGenderElbowRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderElbowLeft:
                    _allGenderElbowLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderHips:
                    _allGenderHips[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderKneeRight:
                    _allGenderKneeRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderKneeLeft:
                    _allGenderKneeLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderExtra:
                    if (active == true)
                        SetActiveFalse();

                    _allGenderExtra[num].SetActive(active);

                    ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair);
                    ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Head, Define.HumanOutfitOneGender.Facial);
                    break;
                case Define.HumanOutfitAllGender.Unknown:
                    break;
            }
        }

        if(active == true)
            _currentAllGenderOutfit[(int)part] = num;
    }
    #endregion

    #region Gender
    public void ChangeGenderOutfit(Define.HumanOutfitOneGender part, int num)
    {
        if (_gender == Define.HumanGender.Male)
        {
            //현재 착용한거 false
            SetActiveMaleParts(part, _currentGenderOutfit[(int)part], false);

            //바꿀거 true
            SetActiveMaleParts(part, num, true);
        }
        else
        {
            //현재 착용한거 false
            SetActiveFemaleParts(part, _currentGenderOutfit[(int)part], false);

            //바꿀거 true
            SetActiveFemaleParts(part, num, true);
        }
    }

    public void ChangeGenderOutfit(string type, int num)
    {
        Define.HumanOutfitOneGender part = Define.HumanOutfitOneGender.Unknown;
        int size = (int)Define.HumanOutfitOneGender.Unknown;
        for(int i = 0; i < size; i++)
        {
            if(type == ((Define.HumanOutfitOneGender)i).ToString())
            {
                part = (Define.HumanOutfitOneGender)i;
                break;
            }
        }
        //Debug.Log(part);
        ChangeGenderOutfit(part, num);
    }

    private void SetActiveMaleParts(Define.HumanOutfitOneGender part, int num, bool active)
    {
        if(num >= 0)
        {
            switch (part)
            {
                case Define.HumanOutfitOneGender.Head:
                    if (active == true)
                        SetActiveFalse();

                    _maleHead[num].SetActive(active);

                    ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair, Define.HumanOutfitAllGender.AllGenderExtra);
                    ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Facial);
                    break;
                case Define.HumanOutfitOneGender.HeadGear:
                    if (active == true)
                        SetActiveFalse();

                    _maleHeadGear[num].SetActive(active);

                    if (active == false)
                    {
                        ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair, Define.HumanOutfitAllGender.AllGenderExtra);
                        ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Head, Define.HumanOutfitOneGender.Facial);
                    }
                    break;
                case Define.HumanOutfitOneGender.Eyebrows:
                    _maleEyebrows[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Facial:
                    if (active == true)
                        SetActiveFalse();

                    _maleFacial[num].SetActive(active);


                    break;
                case Define.HumanOutfitOneGender.Torso:
                    _maleTorso[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmUpperRight:
                    _maleArmUpperRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmUpperLeft:
                    _maleArmUpperLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmLowerRight:
                    _maleArmLowerRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmLowerLeft:
                    _maleArmLowerLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.HandRight:
                    _maleHandRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.HandLeft:
                    _maleHandLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Hips:
                    _maleHips[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.LegRight:
                    _maleLegRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.LegLeft:
                    _maleLegLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Unknown:
                    break;
            }
        }

        if (active == true)
        {
            _currentGenderOutfit[(int)part] = num;
        }
    }
    private void SetActiveFemaleParts(Define.HumanOutfitOneGender part, int num, bool active)
    {
        if (num >= 0)
        {
            switch (part)
            {
                case Define.HumanOutfitOneGender.Head:
                    if (active == true)
                        SetActiveFalse();

                    _femaleHead[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.HeadGear:
                    if (active == true)
                        SetActiveFalse();

                    _femaleHeadGear[num].SetActive(active);

                    if (active == false)
                    {
                        ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair, Define.HumanOutfitAllGender.AllGenderExtra);
                        ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Head, Define.HumanOutfitOneGender.Facial);
                    }
                    break;
                case Define.HumanOutfitOneGender.Eyebrows:
                    if (num >= _femaleEyebrows.Count)
                        Debug.Log("Eyebrows Over");
                    _femaleEyebrows[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Facial:
                        Debug.Log("Facial Over");
                    //_femaleFacial[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Torso:
                    if (num >= _femaleTorso.Count)
                        Debug.Log("Torso Over");
                    _femaleTorso[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmUpperRight:
                    if (num >= _femaleArmUpperRight.Count)
                        Debug.Log("ArmUpperRight Over");
                    _femaleArmUpperRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmUpperLeft:
                    if (num >= _femaleArmUpperLeft.Count)
                        Debug.Log("ArmUpperLeft Over");
                    _femaleArmUpperLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmLowerRight:
                    if (num >= _femaleArmLowerRight.Count)
                        Debug.Log("ArmLowerRight Over");
                    _femaleArmLowerRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmLowerLeft:
                    if (num >= _femaleArmLowerLeft.Count)
                        Debug.Log("ArmLowerLeft Over");
                    _femaleArmLowerLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.HandRight:
                    if (num >= _femaleHandRight.Count)
                        Debug.Log("HandRight Over");
                    _femaleHandRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.HandLeft:
                    if (num >= _femaleHandLeft.Count)
                        Debug.Log("HandLeft Over");
                    _femaleHandLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Hips:
                    if (num >= _femaleHips.Count)
                        Debug.Log("Hips Over");
                    _femaleHips[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.LegRight:
                    if (num >= _femaleLegRight.Count)
                        Debug.Log("LegRight Over");
                    _femaleLegRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.LegLeft:
                    if (num >= _femaleLegLeft.Count)
                        Debug.Log("LegLeft Over");
                    _femaleLegLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Unknown:
                    break;
            }
        }

        if (active == true)
            _currentGenderOutfit[(int)part] = num;
    }

    #endregion
}
