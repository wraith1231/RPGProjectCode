using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBlacksmithWindow : UIPopup
{
    enum UIDropdown
    {
        Category,
        Type,
        WeaponLists,
    }

    enum UIObjects
    {
        Weapons,
        CheckPanel,
    }

    enum UITexts
    {
        LeftHandText,
        RightHandText,
    }
    
    enum UIButtons
    {
        LeftRotate,
        RightRotate,
        SubmitButton,
        CancelButton,
        LeftHandClearButton,
        RightHandClearButton,
        SelectButton,
    }

    private bool _rotateStart;
    private bool _leftRotate;

    private int _categoryValue;
    private int _typeValue;
    private int _weaponValue;
    private UIBlacksmithWeapon _weapon;
    private UICheckPanel _checkPanel;

    private string _leftHand = "Barehand";
    private string _rightHand = "Barehand";

    private Define.WeaponCategory _leftCategory = Define.WeaponCategory.Unknown;
    private Define.WeaponCategory _rightCategory = Define.WeaponCategory.Unknown;
    public override void Init()
    {
        base.Init();

        Time.timeScale = 0;
        GetComponent<Canvas>().worldCamera = Managers.Map.UICam;

        Bind<TMP_Dropdown>(typeof(UIDropdown));
        Bind<GameObject>(typeof(UIObjects));
        Bind<Button>(typeof(UIButtons));
        Bind<TMP_Text>(typeof(UITexts));

        Get<TMP_Dropdown>((int)UIDropdown.Category).onValueChanged.AddListener(CategoryValueChanged);
        Get<TMP_Dropdown>((int)UIDropdown.Type).onValueChanged.AddListener(TypeValueChanged);
        Get<TMP_Dropdown>((int)UIDropdown.WeaponLists).onValueChanged.AddListener(WeaponValueChanged);

        _weapon = Get<GameObject>((int)UIObjects.Weapons).GetComponent<UIBlacksmithWeapon>();

        Get<Button>((int)UIButtons.LeftRotate).onClick.AddListener(LeftRotateButton);
        Get<Button>((int)UIButtons.RightRotate).onClick.AddListener(RightRotateButton);

        Get<Button>((int)UIButtons.SubmitButton).gameObject.BindUIEvent(SubmitButtonClicked);
        Get<Button>((int)UIButtons.CancelButton).gameObject.BindUIEvent(CancelButton);

        Get<Button>((int)UIButtons.LeftHandClearButton).gameObject.BindUIEvent(LeftHandClear);
        Get<Button>((int)UIButtons.RightHandClearButton).gameObject.BindUIEvent(RightHandClear);

        Get<Button>((int)UIButtons.SelectButton).gameObject.BindUIEvent(SelectButtonClicked);

        _checkPanel = Get<GameObject>((int)UIObjects.CheckPanel).GetComponent<UICheckPanel>();
        _checkPanel.Init();
        _checkPanel.gameObject.SetActive(false);

        LeftHandTextChanged(Managers.General.GlobalPlayer.Data.Left.GetFileName());
        RightHandTextChanged(Managers.General.GlobalPlayer.Data.Right.GetFileName());

        _leftCategory = Managers.General.GlobalPlayer.Data.Left.GetCategory();
        _rightCategory = Managers.General.GlobalPlayer.Data.Right.GetCategory();
    }

    private void Update()
    {
        if (_rotateStart == true)
        {
            if (_leftRotate)
            {
                _weapon.transform.Rotate(Vector3.up, 0.1f);
            }
            else
            {
                _weapon.transform.Rotate(Vector3.up, -0.1f);
            }
        }
    }

    private void LeftRotateButton()
    {
        if (_rotateStart == true)
        {
            _rotateStart = false;
        }
        else
        {
            _rotateStart = true;
            _leftRotate = true;
        }
    }
    private void RightRotateButton()
    {
        if (_rotateStart == true)
            _rotateStart = false;
        else
        {
            _rotateStart = true;
            _leftRotate = false;
        }
    }


    private void CategoryValueChanged(int value)
    {
        _categoryValue = value;
        Get<TMP_Dropdown>((int)UIDropdown.Type).ClearOptions();
        Get<TMP_Dropdown>((int)UIDropdown.WeaponLists).ClearOptions();
        _typeValue = _weaponValue = -1;

        if(value == 2)
        {
            int size = (int)Define.WeaponType.Shield;

            List<TMP_Dropdown.OptionData> lists = new List<TMP_Dropdown.OptionData>();
            for(int i =0;i < size; i++)
            {
                TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
                data.text = ((Define.WeaponType)i).ToString();

                lists.Add(data);
            }

            Get<TMP_Dropdown>((int)UIDropdown.Type).AddOptions(lists);

            if (size > 0)
                TypeValueChanged(0);
        }
        else if(value < 2)
        {
            int size = (int)Define.WeaponType.Bow;

            List<TMP_Dropdown.OptionData> lists = new List<TMP_Dropdown.OptionData>();
            for (int i = 0; i < size; i++)
            {
                TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
                data.text = ((Define.WeaponType)i).ToString();

                lists.Add(data);
            }

            Get<TMP_Dropdown>((int)UIDropdown.Type).AddOptions(lists);

            if (size > 0)
                TypeValueChanged(0);
        }
        else
        {
            //shield
            _typeValue = (int)Define.WeaponType.Shield;
            List<Data.WeaponData> list = Managers.Data.ShieldList[(int)Define.WeaponType.Shield];
            int size = list.Count;

            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            for(int i = 0; i < size;i ++)
            {
                TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
                data.text = list[i].File;

                options.Add(data);
            }

            Get<TMP_Dropdown>((int)UIDropdown.WeaponLists).AddOptions(options);

            if (options.Count > 0)
                WeaponValueChanged(0);
        }
    }

    private void TypeValueChanged(int value)
    {
        _typeValue = value;
        Get<TMP_Dropdown>((int)UIDropdown.WeaponLists).ClearOptions();
        _weaponValue = -1;

        if(_categoryValue == 2)
        {
            List<Data.WeaponData> datas = Managers.Data.TwohandList[value];
            int size = datas.Count;

            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            for(int i = 0; i < size; i++)
            {
                TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
                data.text = datas[i].File;

                options.Add(data);
            }
            Get<TMP_Dropdown>((int)UIDropdown.WeaponLists).AddOptions(options);

            if (options.Count > 0)
                WeaponValueChanged(0);
        }
        else if(_categoryValue < 2)
        {
            List<Data.WeaponData> datas = Managers.Data.OnehandList[value];
            int size = datas.Count;

            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            for (int i = 0; i < size; i++)
            {
                TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
                data.text = datas[i].File;

                options.Add(data);
            }
            Get<TMP_Dropdown>((int)UIDropdown.WeaponLists).AddOptions(options);

            if (options.Count > 0)
                WeaponValueChanged(0);
        }
    }


    private void WeaponValueChanged(int value)
    {
        if (value < 0)
            return;

        _weaponValue = value;

    }

    private void SelectButtonClicked(PointerEventData data)
    {
        if (_categoryValue < 0 || _typeValue < 0 || _weaponValue < 0)
            return;

        _weapon.DisplayWeaponChanged(_categoryValue, _typeValue, _weaponValue);

        switch (_categoryValue)
        {
            case 0:
                if (_rightCategory == Define.WeaponCategory.TwoHand)
                {
                    _rightCategory = Define.WeaponCategory.Unknown;
                    _rightHand = "Barehand";
                    RightHandTextChanged(_rightHand);
                }
                _leftCategory = Define.WeaponCategory.OneHand;
                LeftHandTextChanged(Managers.Data.OnehandList[_typeValue][_weaponValue].File);
                break;
            case 1:
                _rightCategory = Define.WeaponCategory.OneHand;
                RightHandTextChanged(Managers.Data.OnehandList[_typeValue][_weaponValue].File);
                break;
            case 2:
                _leftCategory = Define.WeaponCategory.Unknown;
                _rightCategory = Define.WeaponCategory.TwoHand;
                RightHandTextChanged(Managers.Data.TwohandList[_typeValue][_weaponValue].File);
                break;
            case 3:
                if (_rightCategory == Define.WeaponCategory.TwoHand)
                {
                    _rightCategory = Define.WeaponCategory.Unknown;
                    _rightHand = "Barehand";
                }
                _leftCategory = Define.WeaponCategory.Shield;
                LeftHandTextChanged(Managers.Data.ShieldList[_typeValue][_weaponValue].File);
                break;
            case 4:
                _rightCategory = Define.WeaponCategory.Shield;
                RightHandTextChanged(Managers.Data.ShieldList[_typeValue][_weaponValue].File);
                break;
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }

    private void PanelOn()
    {
        _checkPanel.gameObject.SetActive(true);
        _checkPanel.ActiveTrue();
    }
    private void CancelButton(PointerEventData data)
    {
        _checkPanel.WaitFunctions += CancelPanelChoiced;
        _checkPanel.PanelTexts = "Cancel?";
        PanelOn();
    }
    private void CancelPanelChoiced(bool data)
    {
        if (data == true)
            ClosePopupUI();
        else
            _checkPanel.gameObject.SetActive(false);
    }
    private void SubmitButtonClicked(PointerEventData data)
    {
        //if (Managers.General.GlobalGroups[0].Gold < _changeValue)
        //    return;
        if (_categoryValue < 0 || _typeValue < 0 || _weaponValue < 0)
            return;

        _checkPanel.WaitFunctions += SubmitPanelChoiced;
        _checkPanel.PanelTexts = "Submit?";
        PanelOn();
    }

    private void SubmitPanelChoiced(bool data)
    {
        if (data == true)
        {
            Data.WeaponData current = null;
            if (_categoryValue == 2)
            {
                current = Managers.Data.TwohandList[_typeValue][_weaponValue];
            }
            else if(_categoryValue < 2)
            {
                current = Managers.Data.OnehandList[_typeValue][_weaponValue];
            }
            else
            {
                current = Managers.Data.ShieldList[_typeValue][_weaponValue];
            }

            if (current == null)
                return;

            Managers.General.GlobalPlayer.SetLeftWeapon(_leftCategory, _leftHand);
            Managers.General.GlobalPlayer.SetRightWeapon(_rightCategory, _rightHand);

            ClosePopupUI();
        }
        else
        {
            _checkPanel.gameObject.SetActive(false);
        }
    }

    private void LeftHandClear(PointerEventData data)
    {
        if (_leftHand == "None")
            return;

        _leftCategory = Define.WeaponCategory.Unknown;
        LeftHandTextChanged("Barehand");
    }
    private void RightHandClear(PointerEventData data)
    {
        _rightCategory = Define.WeaponCategory.Unknown;
        RightHandTextChanged("Barehand");
    }

    private void LeftHandTextChanged(string change)
    {
        if (change == "" || change == "Unknown")
            _leftHand = "Barehand";
        else
            _leftHand = change;

        Get<TMP_Text>((int)UITexts.LeftHandText).text = $"Left Hand : {_leftHand}";
    }
    private void RightHandTextChanged(string change)
    {
        if (change == "" || change == "Unknown")
            _rightHand = "Barehand";
        else
            _rightHand = change;

        if(_categoryValue != 2)
            Get<TMP_Text>((int)UITexts.RightHandText).text = $"Right Hand : {_rightHand}";
        else
        {
            Get<TMP_Text>((int)UITexts.RightHandText).text = $"Twohand : {_rightHand}";
            _leftHand = "None";
            Get<TMP_Text>((int)UITexts.LeftHandText).text = $"None";
        }

    }
}
