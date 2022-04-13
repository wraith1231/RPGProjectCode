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
                    _prevAllGenderNoHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringNoHair];
                    SetActiveAllGenderParts(Define.HumanOutfitAllGender.HeadCoveringNoHair, _prevAllGenderNoHair, false);

                    _prevAllGenderMask = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringMask];
                    SetActiveAllGenderParts(Define.HumanOutfitAllGender.HeadCoveringMask, _prevAllGenderMask, false);

                    _prevAllGenderHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderHair];
                    SetActiveAllGenderParts(Define.HumanOutfitAllGender.AllGenderHair, _prevAllGenderHair, false);

                    _headCoveringsBase[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.HeadCoveringMask:
                    _prevAllGenderHeadBase = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringBase];
                    SetActiveAllGenderParts(Define.HumanOutfitAllGender.HeadCoveringBase, _prevAllGenderHeadBase, false);

                    _prevAllGenderNoHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringNoHair];
                    SetActiveAllGenderParts(Define.HumanOutfitAllGender.HeadCoveringNoHair, _prevAllGenderNoHair, false);

                    _prevAllGenderHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderHair];
                    SetActiveAllGenderParts(Define.HumanOutfitAllGender.AllGenderHair, _prevAllGenderHair, false);

                    _headCoveringsMask[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.HeadCoveringNoHair:
                    _prevAllGenderHeadBase = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringBase];
                    SetActiveAllGenderParts(Define.HumanOutfitAllGender.HeadCoveringBase, _prevAllGenderHeadBase, false);

                    _prevAllGenderMask = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringMask];
                    SetActiveAllGenderParts(Define.HumanOutfitAllGender.HeadCoveringMask, _prevAllGenderMask, false);

                    _prevAllGenderHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderHair];
                    SetActiveAllGenderParts(Define.HumanOutfitAllGender.AllGenderHair, _prevAllGenderHair, false);

                    _headCoveringsNoHair[num].SetActive(active);
                    break;
                case Define.HumanOutfitAllGender.AllGenderHair:
                    _prevAllGenderHeadBase = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringBase];
                    SetActiveAllGenderParts(Define.HumanOutfitAllGender.HeadCoveringBase, _prevAllGenderHeadBase, false);

                    _prevAllGenderHeadBase = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringBase];
                    SetActiveAllGenderParts(Define.HumanOutfitAllGender.HeadCoveringBase, _prevAllGenderHeadBase, false);

                    _prevAllGenderNoHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringNoHair];
                    SetActiveAllGenderParts(Define.HumanOutfitAllGender.HeadCoveringNoHair, _prevAllGenderNoHair, false);

                    _allGenderHair[num].SetActive(active);
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
                    _allGenderElbowRight[num].SetActive(active);
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
                    SetActiveMaleParts(Define.HumanOutfitOneGender.HeadGear, _prevHeadGear, false);

                    _maleHead[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.HeadGear:
                    _prevHead = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.Head];
                    SetActiveMaleParts(Define.HumanOutfitOneGender.Head, _prevHead, false);

                    _maleHeadGear[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Eyebrows:
                    _maleEyebrows[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Facial:
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
            _currentGenderOutfit[(int)part] = num;
    }
    private void SetActiveFemaleParts(Define.HumanOutfitOneGender part, int num, bool active)
    {
        if (num >= 0)
        {
            switch (part)
            {
                case Define.HumanOutfitOneGender.Head:
                    _prevHeadGear = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.HeadGear];
                    SetActiveFemaleParts(Define.HumanOutfitOneGender.HeadGear, _prevHeadGear, false);

                    _femaleHead[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.HeadGear:
                    _prevHead = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.Head];
                    SetActiveFemaleParts(Define.HumanOutfitOneGender.Head, _prevHead, false);

                    _femaleHeadGear[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Eyebrows:
                    _femaleEyebrows[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Facial:
                    //_femaleFacial[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Torso:
                    _femaleTorso[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmUpperRight:
                    _femaleArmUpperRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmUpperLeft:
                    _femaleArmUpperLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmLowerRight:
                    _femaleArmLowerRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.ArmLowerLeft:
                    _femaleArmLowerLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.HandRight:
                    _femaleHandRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.HandLeft:
                    _femaleHandLeft[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.Hips:
                    _femaleHips[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.LegRight:
                    _femaleLegRight[num].SetActive(active);
                    break;
                case Define.HumanOutfitOneGender.LegLeft:
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
