using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBlacksmithWeapon : UIBase
{
    enum UIObjects
    {
        Onehand,
        Twohand,
        Shield,
    }

    private List<GameObject> _onehand = new List<GameObject>();
    private List<GameObject> _twohand = new List<GameObject>();
    private List<GameObject> _shield = new List<GameObject>();

    private bool _init = false;

    private GameObject _currentDisplay = null;

    public override void Init()
    {
        Bind<GameObject>(typeof(UIObjects));

        //OnehandInit();
    }
    private void Update()
    {
        if(_init == false)
        {
            if (Managers.Data.OnehandList.Count == 0)
                return;

            OnehandInit();
            TwohandInit();
            ShieldInit();

            _init = true;
        }
    }

    public void DisplayWeaponChanged(int category, int type, int value)
    {
        if (category < 0 || type < 0 || value < 0)
            return;
        if (_currentDisplay != null)
            _currentDisplay.SetActive(false);

        Data.WeaponData weapon = null;
        if(category == 2)
        {
            weapon = Managers.Data.TwohandList[type][value];

            int size = _twohand.Count;
            for(int i = 0; i < size; i++)
            {
                if(_twohand[i].name == weapon.File)
                {
                    _twohand[i].SetActive(true);
                    _currentDisplay = _twohand[i];
                    return;
                }
            }
        }
        else if(category < 2)
        {
            weapon = Managers.Data.OnehandList[type][value];

            int size = _onehand.Count;
            for (int i = 0; i < size; i++)
            {
                if (_onehand[i].name == weapon.File)
                {
                    _onehand[i].SetActive(true);
                    _currentDisplay = _onehand[i];
                    return;
                }
            }

        }
        else
        {
            weapon = Managers.Data.ShieldList[(int)Define.WeaponType.Shield][value];

            int size = _shield.Count;
            for (int i = 0; i < size; i++)
            {
                if (_shield[i].name == weapon.File)
                {
                    _shield[i].SetActive(true);
                    _currentDisplay = _shield[i];
                    return;
                }
            }
        }

        Debug.LogError($"weapon not found : {category}, {type}, {value}, {weapon.File}");
    }

    private void OnehandInit()
    {
        List<List<Data.WeaponData>> weapons = Managers.Data.OnehandList;

        int size = weapons.Count;
        int dataSize = 0;
        for(int i = 0; i < size; i++)
        {
            dataSize = weapons[i].Count;
            for(int j = 0; j < dataSize; j++)
            {
                Managers.Resource.Instantiate("Onehand/" + weapons[i][j].File, OnehandInstantiated, Get<GameObject>((int)UIObjects.Onehand).transform);
            }
        }
    }

    private void OnehandInstantiated(GameObject go)
    {
        _onehand.Add(go);
        string[] temp = go.name.Split('/');
        go.name = temp[1];
        go.layer = gameObject.layer;
        go.SetActive(false);
    }

    private void TwohandInit()
    {
        List<List<Data.WeaponData>> weapons = Managers.Data.TwohandList;

        int size = weapons.Count;
        int dataSize = 0;
        for (int i = 0; i < size; i++)
        {
            dataSize = weapons[i].Count;
            for (int j = 0; j < dataSize; j++)
            {
                Managers.Resource.Instantiate("Twohand/" + weapons[i][j].File, TwohandInstantiated, Get<GameObject>((int)UIObjects.Twohand).transform);
            }
        }
    }
    private void TwohandInstantiated(GameObject go)
    {
        _twohand.Add(go);
        string[] temp = go.name.Split('/');
        go.name = temp[1];
        go.layer = gameObject.layer;
        go.SetActive(false);
    }

    private void ShieldInit()
    {
        List<List<Data.WeaponData>> weapons = Managers.Data.ShieldList;

        int size = weapons.Count;
        int dataSize = 0;
        for (int i = 0; i < size; i++)
        {
            dataSize = weapons[i].Count;
            for (int j = 0; j < dataSize; j++)
            {
                Managers.Resource.Instantiate("Shield/" + weapons[i][j].File, ShieldInstantiated, Get<GameObject>((int)UIObjects.Shield).transform);
            }
        }
    }
    private void ShieldInstantiated(GameObject go)
    {
        _shield.Add(go);
        string[] temp = go.name.Split('/');
        go.name = temp[1];
        go.layer = gameObject.layer;
        go.SetActive(false);
    }
}
