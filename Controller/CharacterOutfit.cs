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

    private Define.LastOutfitChange _lastChanged = Define.LastOutfitChange.AllGenderHair;
    private int _prevAllGenderHeadBase = -1;
    private int _prevAllGenderMask = -1;
    private int _prevAllGenderNoHair = -1;
    private int _prevAllGenderHair = -1;
    private int _prevExtra = -1;

    private int _prevHeadGear = -1;
    private int _prevHead = -1;
    private int _prevFacial = -1;

    [SerializeField] private List<GameObject> _genderParts;

    private Dictionary<Define.HumanOutfitAllGender, List<GameObject>> _allGenderParts = new Dictionary<Define.HumanOutfitAllGender, List<GameObject>>();
    private Dictionary<Define.HumanOutfitOneGender, List<GameObject>> _femaleParts = new Dictionary<Define.HumanOutfitOneGender, List<GameObject>>();
    private Dictionary<Define.HumanOutfitOneGender, List<GameObject>> _maleParts = new Dictionary<Define.HumanOutfitOneGender, List<GameObject>>();

    public delegate void InitEndFunc();
    public InitEndFunc InitEnded;
    protected HumanOutfit _tempOutfit = null;

    public int GetAllGenderPartCount(Define.HumanOutfitAllGender part)
    {
        return _allGenderParts[part].Count;
    }
    public int GetGenderPartCount(Define.HumanOutfitOneGender part)
    {
        return _maleParts[part].Count;
    }
    public int GetGenderPartCount(Define.HumanGender gender, Define.HumanOutfitOneGender part)
    {
        if (gender == Define.HumanGender.Female)
            return _femaleParts[part].Count;
        else if (gender == Define.HumanGender.Male)
            return _maleParts[part].Count;
        else
            return -1;
    }

    private int _allOutfitCheckerCount = 0;
    private int _allOutfitCheckerInitCount = 0;
    private bool _allOutfitEnd = false;

    private int _maleOutfitCheckerCount = 0;
    private int _maleOutfitCheckerInitCount = 0;
    private bool _maleOutfitEnd = false;

    private int _femaleOutfitCheckerCount = 0;
    private int _femaleOutfitCheckerInitCount = 0;
    private bool _femaleOutfitEnd = false;

    private void Start()
    {
        AllGenderOutfitChecker[] alls = gameObject.GetComponentsInChildren<AllGenderOutfitChecker>();
        _allOutfitCheckerCount = alls.Length;

        FemaleOutfitChecker[] females = gameObject.GetComponentsInChildren<FemaleOutfitChecker>();
        _femaleOutfitCheckerCount = females.Length;

        MaleOutfitChecker[] males = gameObject.GetComponentsInChildren<MaleOutfitChecker>();
        _maleOutfitCheckerCount = males.Length;

        for (int i = 0; i < _allOutfitCheckerCount; i++)
            alls[i].GetChildEnd += AllOutfitCheckerInited;

        for (int i = 0; i < _maleOutfitCheckerCount; i++)
            males[i].GetChildEnd += MaleOutfitCheckerInited;

        for (int i = 0; i < _femaleOutfitCheckerCount; i++)
            females[i].GetChildEnd += FemaleOutfitCheckerInited;

        //Init();
    }

    private void AllOutfitCheckerInited()
    {
        _allOutfitCheckerInitCount++;

        if(_allOutfitCheckerInitCount == _allOutfitCheckerCount)
        {
            _allOutfitEnd = true;

            if (_maleOutfitEnd && _femaleOutfitEnd)
                Init();
        }
    }
    private void MaleOutfitCheckerInited()
    {
        _maleOutfitCheckerInitCount++;

        if (_maleOutfitCheckerInitCount == _maleOutfitCheckerCount)
        {
            _maleOutfitEnd = true;

            if (_allOutfitEnd && _femaleOutfitEnd)
                Init();
        }
    }

    private void FemaleOutfitCheckerInited()
    {
        _femaleOutfitCheckerInitCount++;

        if (_femaleOutfitCheckerInitCount == _femaleOutfitCheckerCount)
        {
            _femaleOutfitEnd = true;

            if (_maleOutfitEnd && _allOutfitEnd)
                Init();
        }
    }

    private void Init()
    {
        if (_isInit == true)
        {
            InitEndPlayer();
        }

        int size = (int)Define.HumanOutfitAllGender.Unknown;

        AllGenderOutfitChecker[] alls = gameObject.GetComponentsInChildren<AllGenderOutfitChecker>();
        FemaleOutfitChecker[] females = gameObject.GetComponentsInChildren<FemaleOutfitChecker>();
        MaleOutfitChecker[] males = gameObject.GetComponentsInChildren<MaleOutfitChecker>();

        size = alls.Length;
        for(int i = 0; i < size; i++)
        {
            Define.HumanOutfitAllGender all = (Define.HumanOutfitAllGender)alls[i].GetOutfitType();
            _allGenderParts[all] = alls[i].GetChildren();
        }

        size = males.Length;
        for(int i =0; i < size; i++)
        {
            Define.HumanOutfitOneGender male = (Define.HumanOutfitOneGender)males[i].GetOutfitType();
            _maleParts[male] = males[i].GetChildren();
        }

        size = females.Length;
        for (int i = 0; i < size; i++)
        {
            Define.HumanOutfitOneGender female = (Define.HumanOutfitOneGender)females[i].GetOutfitType();
            _femaleParts[female] = females[i].GetChildren();
        }

        size = (int)Define.HumanOutfitAllGender.Unknown;
        for(int i = 0; i < size; i++)
        {
            if (i == (int)Define.HumanOutfitAllGender.AllGenderHair)
                _currentAllGenderOutfit.Add(0);
            else
                _currentAllGenderOutfit.Add(-1);
            
        }

        size = (int)Define.HumanOutfitOneGender.Unknown;
        for(int i =0; i < size; i++)
        {
            if (i == (int)Define.HumanOutfitOneGender.HeadGear || i == (int)Define.HumanOutfitOneGender.Facial)
                _currentGenderOutfit.Add(-1);
            else
                _currentGenderOutfit.Add(0);
        }

        _isInit = true;
        InitEndPlayer();
    }

    private void InitEndPlayer()
    {
        if(InitEnded != null)
            InitEnded();
        InitEnded = null;
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


    public void SetOutfit(HumanOutfit outfit)
    {
        if (outfit == null)
            return;

        if (_isInit == false)
        {
            _tempOutfit = outfit;
            InitEnded += SetOutfit;
            return;
        }

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

        switch (outfit.LastChange)
        {
            case Define.LastOutfitChange.HeadCoveringBase:
                ChangeAllGenderOutfit(Define.HumanOutfitAllGender.HeadCoveringBase, outfit.AllGender[(int)Define.HumanOutfitAllGender.HeadCoveringBase]);
                break;
            case Define.LastOutfitChange.HeadCoveringMask:
                ChangeAllGenderOutfit(Define.HumanOutfitAllGender.HeadCoveringMask, outfit.AllGender[(int)Define.HumanOutfitAllGender.HeadCoveringMask]);
                break;
            case Define.LastOutfitChange.HeadCoveringNoHair:
                ChangeAllGenderOutfit(Define.HumanOutfitAllGender.HeadCoveringNoHair, outfit.AllGender[(int)Define.HumanOutfitAllGender.HeadCoveringNoHair]);
                break;
            case Define.LastOutfitChange.AllGenderHair:
                ChangeAllGenderOutfit(Define.HumanOutfitAllGender.AllGenderHair, outfit.AllGender[(int)Define.HumanOutfitAllGender.AllGenderHair]);
                break;
            case Define.LastOutfitChange.AllGenderExtra:
                ChangeAllGenderOutfit(Define.HumanOutfitAllGender.AllGenderExtra, outfit.AllGender[(int)Define.HumanOutfitAllGender.AllGenderExtra]);
                break;
            case Define.LastOutfitChange.Head:
                ChangeGenderOutfit(Define.HumanOutfitOneGender.Head, outfit.OneGender[(int)Define.HumanOutfitOneGender.Head]);
                break;
            case Define.LastOutfitChange.HeadGear:
                ChangeGenderOutfit(Define.HumanOutfitOneGender.HeadGear, outfit.OneGender[(int)Define.HumanOutfitOneGender.HeadGear]);
                break;
            case Define.LastOutfitChange.Facial:
                ChangeGenderOutfit(Define.HumanOutfitOneGender.Facial, outfit.OneGender[(int)Define.HumanOutfitOneGender.Facial]);
                break;
        }
    }
    public void SetOutfit()
    {
        if (_tempOutfit == null)
            return;

        SetOutfit(_tempOutfit);
    }

    public void SaveOutfitData(HumanOutfit outfit)
    {
        if (outfit == null) return;

        outfit.Gender = _gender;
        outfit.LastChange = _lastChanged;

        int size = (int)Define.HumanOutfitAllGender.Unknown;
        for (int i = 0; i < size; i++)
            outfit.AllGender[i] = _currentAllGenderOutfit[i];

        size = (int)Define.HumanOutfitOneGender.Unknown;
        for (int i = 0; i < size; i++)
            outfit.OneGender[i] = _currentGenderOutfit[i];
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
            _allGenderParts[Define.HumanOutfitAllGender.HeadCoveringBase][_prevAllGenderHeadBase].SetActive(false);

        _prevAllGenderMask = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringMask];
        if (_prevAllGenderMask >= 0)
            _allGenderParts[Define.HumanOutfitAllGender.HeadCoveringMask][_prevAllGenderMask].SetActive(false);

        _prevAllGenderNoHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.HeadCoveringNoHair];
        if (_prevAllGenderNoHair >= 0)
            _allGenderParts[Define.HumanOutfitAllGender.HeadCoveringNoHair][_prevAllGenderNoHair].SetActive(false);

        _prevAllGenderHair = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderHair];
        if (_prevAllGenderHair >= 0)
            _allGenderParts[Define.HumanOutfitAllGender.AllGenderHair][_prevAllGenderHair].SetActive(false);

        _prevExtra = _currentAllGenderOutfit[(int)Define.HumanOutfitAllGender.AllGenderExtra];
        if (_prevExtra >= 0)
            _allGenderParts[Define.HumanOutfitAllGender.AllGenderExtra][_prevExtra].SetActive(false);

        _prevHeadGear = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.HeadGear];
        _prevHead = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.Head];
        _prevFacial = _currentGenderOutfit[(int)Define.HumanOutfitOneGender.Facial];
        
        if(_gender == Define.HumanGender.Male)
        {
            if (_prevHeadGear >= 0)
                _maleParts[Define.HumanOutfitOneGender.HeadGear][_prevHeadGear].SetActive(false);

            if (_prevHead >= 0)
                _maleParts[Define.HumanOutfitOneGender.Head][_prevHead].SetActive(false);

            if (_prevFacial >= 0)
                _maleParts[Define.HumanOutfitOneGender.Facial][_prevFacial].SetActive(false);
        }
        else
        {
            if (_prevHeadGear >= 0)
                _femaleParts[Define.HumanOutfitOneGender.HeadGear][_prevHeadGear].SetActive(false);

            if (_prevHead >= 0)
                _femaleParts[Define.HumanOutfitOneGender.Head][_prevHead].SetActive(false);

            //if (_prevFacial >= 0)
            //    _femaleParts[Define.HumanOutfitOneGender.Facial][_prevFacial].SetActive(false);
        }
    }

    private void ActiveDefaultGenderHeads(params Define.HumanOutfitOneGender[] ones)
    {
        int size = ones.Length;
        if(_gender == Define.HumanGender.Female)
        {
            for (int i = 0; i < size; i++)
            {
                if (ones[i] == Define.HumanOutfitOneGender.Head)
                {
                    _currentGenderOutfit[(int)ones[i]] = _prevHead;
                    if (_prevHead < 0) continue;
                    _femaleParts[ones[i]][_prevHead].SetActive(true);
                }
                if (ones[i] == Define.HumanOutfitOneGender.Facial)
                {
                    _currentGenderOutfit[(int)ones[i]] = _prevFacial;
                    if (_prevFacial < 0) continue;
                }
            }
        }
        else
        {
            for (int i = 0; i < size; i++)
            {
                if (ones[i] == Define.HumanOutfitOneGender.Head)
                {
                    _currentGenderOutfit[(int)ones[i]] = _prevHead;
                    if (_prevHead < 0) continue;
                    _maleParts[ones[i]][_prevHead].SetActive(true);
                }
                if (ones[i] == Define.HumanOutfitOneGender.Facial)
                {
                    _currentGenderOutfit[(int)ones[i]] = _prevFacial;
                    if (_prevFacial < 0) continue;
                    _maleParts[ones[i]][_prevFacial].SetActive(true);
                }
            }
        }
    }
    private void ActiveDefaultAllHeads(params Define.HumanOutfitAllGender[] alls)
    {
        int size = alls.Length;
        for(int i = 0; i < size; i++)
        {
            if (alls[i] == Define.HumanOutfitAllGender.AllGenderHair)
            {
                _currentAllGenderOutfit[(int)alls[i]] = _prevAllGenderHair;
                if (_prevAllGenderHair < 0) continue;
                _allGenderParts[alls[i]][_prevAllGenderHair].SetActive(true);
            }
            if (alls[i] == Define.HumanOutfitAllGender.AllGenderExtra)
            {
                _currentAllGenderOutfit[(int)alls[i]] = _prevExtra;
                if (_prevExtra < 0) continue;
                _allGenderParts[alls[i]][_prevExtra].SetActive(true);
            }
        }
    }

    private void SetActiveAllGenderParts(Define.HumanOutfitAllGender part, int num, bool active)
    {
        if(num >= 0)
        {
            switch(part)
            {
                case Define.HumanOutfitAllGender.HeadCoveringBase:
                    if (active == true)
                        SetActiveFalse();

                    _allGenderParts[part][num].SetActive(active);
                    _lastChanged = Define.LastOutfitChange.HeadCoveringBase;

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

                    _allGenderParts[part][num].SetActive(active);
                    _lastChanged = Define.LastOutfitChange.HeadCoveringMask;

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

                    _allGenderParts[part][num].SetActive(active);
                    _lastChanged = Define.LastOutfitChange.HeadCoveringNoHair;

                    ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Head, Define.HumanOutfitOneGender.Facial);
                    if (active == false)
                    {
                        ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair, Define.HumanOutfitAllGender.AllGenderExtra);
                    }
                    break;
                case Define.HumanOutfitAllGender.AllGenderHair:
                    if (active == true)
                        SetActiveFalse();

                    _allGenderParts[part][num].SetActive(active);
                    _lastChanged = Define.LastOutfitChange.AllGenderHair;

                    ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderExtra);
                    ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Head, Define.HumanOutfitOneGender.Facial);

                    break;
                case Define.HumanOutfitAllGender.AllGenderExtra:
                    if (active == true)
                        SetActiveFalse();

                    _allGenderParts[part][num].SetActive(active);
                    _lastChanged = Define.LastOutfitChange.AllGenderExtra;

                    ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair);
                    ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Head, Define.HumanOutfitOneGender.Facial);
                    break;
                default:
                    _allGenderParts[part][num].SetActive(active);

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
            switch(part)
            {
                case Define.HumanOutfitOneGender.Head:
                    if (active == true)
                        SetActiveFalse();

                    _maleParts[part][num].SetActive(active);
                    _lastChanged = Define.LastOutfitChange.Head;

                    ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair, Define.HumanOutfitAllGender.AllGenderExtra);
                    ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Facial);
                    break;
                case Define.HumanOutfitOneGender.HeadGear:
                    if (active == true)
                        SetActiveFalse();

                    _maleParts[part][num].SetActive(active);
                    _lastChanged = Define.LastOutfitChange.HeadGear;

                    if (active == false)
                    {
                        ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair, Define.HumanOutfitAllGender.AllGenderExtra);
                        ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Head, Define.HumanOutfitOneGender.Facial);
                    }
                    break;
                case Define.HumanOutfitOneGender.Facial:
                    if (active == true)
                        SetActiveFalse();

                    _maleParts[part][num].SetActive(active);
                    _lastChanged = Define.LastOutfitChange.Facial;

                    ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair, Define.HumanOutfitAllGender.AllGenderExtra);
                    ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Head);
                    break;
                default:
                    _maleParts[part][num].SetActive(active);
                    break;
            }
        }

        if (active == true)
        {
            if (part == Define.HumanOutfitOneGender.Eyebrows)
                Debug.Log(num);
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

                    _femaleParts[part][num].SetActive(active);
                    _lastChanged = Define.LastOutfitChange.Head;

                    ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair, Define.HumanOutfitAllGender.AllGenderExtra);
                    ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Facial);
                    break;
                case Define.HumanOutfitOneGender.HeadGear:
                    if (active == true)
                        SetActiveFalse();

                    _femaleParts[part][num].SetActive(active);
                    _lastChanged = Define.LastOutfitChange.HeadGear;

                    if (active == false)
                    {
                        ActiveDefaultAllHeads(Define.HumanOutfitAllGender.AllGenderHair, Define.HumanOutfitAllGender.AllGenderExtra);
                        ActiveDefaultGenderHeads(Define.HumanOutfitOneGender.Head, Define.HumanOutfitOneGender.Facial);
                    }
                    break;
                case Define.HumanOutfitOneGender.Facial:


                    break;
                default:
                    _femaleParts[part][num].SetActive(active);
                    break;
            }
        }

        if (active == true)
        {
            if (part == Define.HumanOutfitOneGender.Eyebrows)
                Debug.Log(num);
            _currentGenderOutfit[(int)part] = num;
        }
    }

    #endregion
}
