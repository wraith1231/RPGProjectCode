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

    [SerializeField] private List<GameObject> _genderParts;

    #region All Gender
    [SerializeField] private List<GameObject> _headCoveringsBase;
    [SerializeField] private List<GameObject> _headCoveringsMask;
    [SerializeField] private List<GameObject> _headCoveringsNoHair;

    [SerializeField] private List<GameObject> _allGenderHair;
    [SerializeField] private List<GameObject> _allGenderHeadAttachment;
    [SerializeField] private List<GameObject> _allGenderBackAttachment;
    [SerializeField] private List<GameObject> _allGenderShoulderRight;
    [SerializeField] private List<GameObject> _allGenderShoulderLeft;
    [SerializeField] private List<GameObject> _allGenderElbowRight;
    [SerializeField] private List<GameObject> _allGenderElbowLeft;
    [SerializeField] private List<GameObject> _allGenderHips;
    [SerializeField] private List<GameObject> _allGenderKneeRight;
    [SerializeField] private List<GameObject> _allGenderKneeLeft;
    [SerializeField] private List<GameObject> _allGenderExtra;
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
    private void ChangeAllGenderOutfit(Define.HumanOutfitAllGender part, int num)
    {
        //현재 착용한거 false
        SetActiveAllGenderParts(part, _currentAllGenderOutfit[(int)part], false);

        //바꿀거 true
        SetActiveAllGenderParts(part, num, true);
    }

    private void SetActiveAllGenderParts(Define.HumanOutfitAllGender part, int num, bool active)
    {
        if (num >= 0)
        {
            switch (part)
            {
                case Define.HumanOutfitAllGender.HeadCoveringBase:
                    //mask, nohair, Nohair, headgear false
                    _prevAllGenderMask = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringMask];
                    if(_prevAllGenderMask >= 0)
                        _headCoveringsMask[_prevAllGenderMask].SetActive(false);

                    _prevAllGenderNoHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringNoHair];
                    if (_prevAllGenderNoHair >= 0)
                        _headCoveringsNoHair[_prevAllGenderNoHair].SetActive(false);

                    _prevAllGenderHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderHair];
                    if (_prevAllGenderHair >= 0)
                        _allGenderHair[_prevAllGenderHair].SetActive(false);

                    _prevHeadGear = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.HeadGear];
                    if (_prevHeadGear >= 0)
                        ChangeGenderOutfit(Define.HumanOutfitOneGender.HeadGear, -1);

                    if (num >= _headCoveringsBase.Count)
                        Debug.Log("HeadCoveringBase Over");
                    _headCoveringsBase[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.HeadCoveringMask:
                    //base, nohair, headgear false
                    _prevAllGenderHeadBase = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringBase];
                    if (_prevAllGenderHeadBase >= 0)
                        _headCoveringsBase[_prevAllGenderHeadBase].SetActive(false);

                    _prevAllGenderNoHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringNoHair];
                    if (_prevAllGenderNoHair >= 0)
                        _headCoveringsNoHair[_prevAllGenderNoHair].SetActive(false);

                    _prevHeadGear = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.HeadGear];
                    if (_prevHeadGear >= 0)
                        ChangeGenderOutfit(Define.HumanOutfitOneGender.HeadGear, -1);

                    if (num >= _headCoveringsMask.Count)
                        Debug.Log("_headCoveringsMask Over");
                    _headCoveringsMask[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.HeadCoveringNoHair:
                    //base, mask, hair, extra, headgear false
                    _prevAllGenderHeadBase = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringBase];
                    if (_prevAllGenderHeadBase >= 0)
                        _headCoveringsBase[_prevAllGenderHeadBase].SetActive(false);

                    _prevAllGenderMask = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringMask];
                    if (_prevAllGenderMask >= 0)
                        _headCoveringsMask[_prevAllGenderMask].SetActive(false);

                    _prevAllGenderHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderHair];
                    if (_prevAllGenderHair >= 0)
                        _allGenderHair[_prevAllGenderHair].SetActive(false);

                    _prevExtra = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderExtra];
                    if (_prevExtra >= 0)
                        _allGenderExtra[_prevExtra].SetActive(false);

                    _prevHeadGear = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.HeadGear];
                    if (_prevHeadGear >= 0)
                        ChangeGenderOutfit(Define.HumanOutfitOneGender.HeadGear, -1);

                    if (num >= _headCoveringsNoHair.Count)
                        Debug.Log("_headCoveringsNoHair Over");
                    _headCoveringsNoHair[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderHair:
                    //base, mask, nohair, headgear false
                    _prevAllGenderHeadBase = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringBase];
                    if (_prevAllGenderHeadBase >= 0)
                        _headCoveringsBase[_prevAllGenderHeadBase].SetActive(false);

                    _prevAllGenderMask = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringMask];
                    if (_prevAllGenderMask >= 0)
                        _headCoveringsMask[_prevAllGenderMask].SetActive(false);

                    _prevAllGenderNoHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringNoHair];
                    if (_prevAllGenderNoHair >= 0)
                        _headCoveringsNoHair[_prevAllGenderNoHair].SetActive(false);

                    _prevHeadGear = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.HeadGear];
                    if (_prevHeadGear >= 0)
                        ChangeGenderOutfit(Define.HumanOutfitOneGender.HeadGear, -1);

                    if (num >= _allGenderHair.Count)
                        Debug.Log("_allGenderHair Over");
                    _allGenderHair[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderHeadAttachment:
                    if (num >= _allGenderHeadAttachment.Count)
                        Debug.Log("_allGenderHeadAttachment Over");
                    _allGenderHeadAttachment[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderBackAttachment:
                    if (num >= _allGenderBackAttachment.Count)
                        Debug.Log("_allGenderBackAttachment Over");
                    _allGenderBackAttachment[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderShoulderRight:
                    if (num >= _allGenderShoulderRight.Count)
                        Debug.Log("_allGenderShoulderRight Over");
                    _allGenderShoulderRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderShoulderLeft:
                    if (num >= _allGenderShoulderLeft.Count)
                        Debug.Log("_allGenderShoulderLeft Over");
                    _allGenderShoulderLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderElbowRight:
                    if (num >= _allGenderElbowRight.Count)
                        Debug.Log("_allGenderElbowRight Over");
                    _allGenderElbowRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderElbowLeft:
                    if (num >= _allGenderElbowRight.Count)
                        Debug.Log("_allGenderElbowRight Over");
                    _allGenderElbowRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderHips:
                    if (num >= _allGenderHips.Count)
                        Debug.Log("_allGenderHips Over");
                    _allGenderHips[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderKneeRight:
                    if (num >= _allGenderKneeRight.Count)
                        Debug.Log("_allGenderKneeRight Over");
                    _allGenderKneeRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderKneeLeft:
                    if (num >= _allGenderKneeLeft.Count)
                        Debug.Log("_allGenderKneeLeft Over");
                    _allGenderKneeLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderExtra:
                    if (num >= _allGenderExtra.Count)
                        Debug.Log("_allGenderExtra Over");
                    _allGenderExtra[num].SetActive(active);
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
    private void ChangeGenderOutfit(Define.HumanOutfitOneGender part, int num)
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

    private void SetActiveMaleParts(Define.HumanOutfitOneGender part, int num, bool active)
    {
        if(num >= 0)
        {
            switch (part)
            {
                case Define.HumanOutfitOneGender.Head:
                    _prevHeadGear = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.HeadGear];
                    if (_prevHeadGear >= 0)
                        _maleHeadGear[_prevHeadGear].SetActive(false);

                    if (num >= _maleHead.Count)
                        Debug.Log("Head Over");

                    _maleHead[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.HeadGear:
                    _prevHead = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.Head];
                    if (_prevHead >= 0)
                        _maleHead[_prevHead].SetActive(false);
                    _prevAllGenderHeadBase = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringBase];
                    if (_prevAllGenderHeadBase >= 0)
                        _headCoveringsBase[_prevAllGenderHeadBase].SetActive(false);

                    _prevAllGenderMask = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringMask];
                    if (_prevAllGenderMask >= 0)
                        _headCoveringsMask[_prevAllGenderMask].SetActive(false);

                    _prevAllGenderHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderHair];
                    if (_prevAllGenderHair >= 0)
                        _allGenderHair[_prevAllGenderHair].SetActive(false);

                    _prevExtra = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderExtra];
                    if (_prevExtra >= 0)
                        _allGenderExtra[_prevExtra].SetActive(false);

                    _prevAllGenderNoHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringNoHair];
                    if (_prevAllGenderNoHair >= 0)
                        _headCoveringsNoHair[_prevAllGenderNoHair].SetActive(false);

                    if (num >= _maleHeadGear.Count)
                        Debug.Log("HeadGear Over");
                    _maleHeadGear[num].SetActive(active);

                    if (_prevHead >= 0 && active == false)
                        _maleHead[_prevHead].SetActive(true);
                    break;
                case Define.HumanOutfitOneGender.Eyebrows:
                    if (num >= _maleEyebrows.Count)
                        Debug.Log("Eyebrows Over");
                    _maleEyebrows[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Facial:
                    if (num >= _maleFacial.Count)
                        Debug.Log("Facial Over");
                    _maleFacial[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Torso:
                    if (num >= _maleTorso.Count)
                        Debug.Log("Torso Over");
                    _maleTorso[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmUpperRight:
                    if (num >= _maleArmUpperRight.Count)
                        Debug.Log("ArmUpperRight Over");
                    _maleArmUpperRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmUpperLeft:
                    if (num >= _maleArmUpperLeft.Count)
                        Debug.Log("ArmUpperLeft Over");
                    _maleArmUpperLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmLowerRight:
                    if (num >= _maleArmLowerRight.Count)
                        Debug.Log("ArmLowerRight Over");
                    _maleArmLowerRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmLowerLeft:
                    if (num >= _maleArmLowerLeft.Count)
                        Debug.Log("ArmLowerLeft Over");
                    _maleArmLowerLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.HandRight:
                    if (num >= _maleHandRight.Count)
                        Debug.Log("HandRight Over");
                    _maleHandRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.HandLeft:
                    if (num >= _maleHandLeft.Count)
                        Debug.Log("HandLeft Over");
                    _maleHandLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Hips:
                    if (num >= _maleHips.Count)
                        Debug.Log("Hips Over");
                    _maleHips[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.LegRight:
                    if (num >= _maleLegRight.Count)
                        Debug.Log("LegRight Over");
                    _maleLegRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.LegLeft:
                    if (num >= _maleLegLeft.Count)
                        Debug.Log("LegLeft Over");
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
                    _prevHeadGear = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.HeadGear];
                    if(_prevHeadGear >= 0)
                        _femaleHeadGear[_prevHeadGear].SetActive(false);

                    if (num >= _femaleHead.Count)
                        Debug.Log("Head Over");
                    _femaleHead[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.HeadGear:
                    _prevHead = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.Head];
                    if(_prevHead >= 0)
                        _femaleHead[_prevHead].SetActive(false);
                    _prevAllGenderHeadBase = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringBase];
                    if (_prevAllGenderHeadBase >= 0)
                        _headCoveringsBase[_prevAllGenderHeadBase].SetActive(false);

                    _prevAllGenderMask = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringMask];
                    if (_prevAllGenderMask >= 0)
                        _headCoveringsMask[_prevAllGenderMask].SetActive(false);

                    _prevAllGenderHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderHair];
                    if (_prevAllGenderHair >= 0)
                        _allGenderHair[_prevAllGenderHair].SetActive(false);

                    _prevExtra = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderExtra];
                    if (_prevExtra >= 0)
                        _allGenderExtra[_prevExtra].SetActive(false);

                    _prevAllGenderNoHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringNoHair];
                    if (_prevAllGenderNoHair >= 0)
                        _headCoveringsNoHair[_prevAllGenderNoHair].SetActive(false);

                    if (num >= _femaleHeadGear.Count)
                        Debug.Log("HeadGear Over");
                    _femaleHeadGear[num].SetActive(active);

                    if (_prevHead >= 0 && active == false)
                        _femaleHead[_prevHead].SetActive(true);
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
