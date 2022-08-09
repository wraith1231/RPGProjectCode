using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanOutfit
{
    public Define.HumanGender Gender;
    
    public List<int> AllGender = new List<int>();
    public List<int> OneGender = new List<int>();

    public Define.LastOutfitChange LastChange;

    public int CompareDeference(HumanOutfit outfit)
    {
        int ret = 0;
        int size = AllGender.Count;
        for(int i = 0; i < size; i++)
        {
            if (AllGender[i] != outfit.AllGender[i])
                ret++;
        }

        size = OneGender.Count;
        for(int i = 0; i < size; i++)
        {
            if (OneGender[i] != outfit.OneGender[i])
                ret++;
        }

        return ret;
    }
    public bool CompareDeferenceAllGender(Define.HumanOutfitAllGender type, int value)
    {
        if (value == AllGender[(int)type])
            return true;

        return false;
    }
    public bool CompareDeferenceAllGender(string type, int value)
    {
        int size = (int)Define.HumanOutfitAllGender.Unknown;
        for (int i = 0; i < size; i++)
        {
            if (type == ((Define.HumanOutfitAllGender)i).ToString())
            {
                if (value == AllGender[i])
                    return true;

                return false;
            }
        }
        return false;
    }
    public bool CompareDeferenceGender(Define.HumanOutfitOneGender type, int value)
    {
        if (value == OneGender[(int)type])
            return true;

        return false;
    }
    public bool CompareDeferenceGender(string type, int value)
    {
        int size = (int)Define.HumanOutfitOneGender.Unknown;
        for (int i = 0; i < size; i++)
        {
            if (type == ((Define.HumanOutfitOneGender)i).ToString())
            {
                if (value == OneGender[i])
                    return true;

                return false;
            }
        }
        return false;
    }

    public HumanOutfit()
    {
        int size = (int)Define.HumanOutfitAllGender.Unknown;
        for (int i = 0; i < size; i++)
        {
            AllGender.Add(-1);
        }
        size = (int)Define.HumanOutfitOneGender.Unknown;
        for (int i = 0; i < size; i++)
        {
            if (i == (int)Define.HumanOutfitOneGender.HeadGear || i == (int)Define.HumanOutfitOneGender.Facial)
                OneGender.Add(-1);
            else
                OneGender.Add(0);
        }
    }

    public void CopyHumanOutfit(HumanOutfit outfit)
    {
        if (outfit == null)
            return;
        Gender = outfit.Gender;

        int size = (int)Define.HumanOutfitAllGender.Unknown;
        for (int i = 0; i < size; i++)
        {
            AllGender[i] = outfit.AllGender[i];
        }
        size = (int)Define.HumanOutfitOneGender.Unknown;
        for (int i = 0; i < size; i++)
        {
            OneGender[i] = outfit.OneGender[i];
        }
    }

    public void SetStatData(Data.StatData data)
    {
        if (data.Gender == true)
            Gender = Define.HumanGender.Female;
        else
            Gender = Define.HumanGender.Male;

        int size = (int)Define.HumanOutfitAllGender.Unknown;
        for (int i = 0; i < size; i++)
        {
            SetAllGenderData((Define.HumanOutfitAllGender)i, data.AllGenderOutfit[i]);
        }

        size = (int)Define.HumanOutfitOneGender.Unknown;
        for (int i = 0; i < size; i++)
        {
            SetOneGenderData((Define.HumanOutfitOneGender)i, data.OneGenderOutfit[i]);
        }
    }

    public void SetNPCBaseData()
    {
        Data.StatData min = Managers.Data.NPCMinimumStat;
        Data.StatData max = Managers.Data.NPCMaximumStat;

        int gender = Random.Range(0, 2);
        if(gender == 0)
            Gender = Define.HumanGender.Male;
        else
            Gender = Define.HumanGender.Female;

        int size = (int)Define.HumanOutfitAllGender.Unknown;
        for (int i = 0; i < size; i++)
        {
            SetAllGenderData((Define.HumanOutfitAllGender)i, Random.Range(min.AllGenderOutfit[i], max.AllGenderOutfit[i]));
        }

        size = (int)Define.HumanOutfitOneGender.Unknown;
        for (int i = 0; i < size; i++)
        {
            SetOneGenderData((Define.HumanOutfitOneGender)i, Random.Range(min.OneGenderOutfit[i], max.OneGenderOutfit[i]));
        }
    }

    public int GetAllGenderData(Define.HumanOutfitAllGender allGender)
    {
        return AllGender[(int)allGender];
    }
    public int GetAllGenderData(string type)
    {
        int size = (int)Define.HumanOutfitAllGender.Unknown;
        for(int i = 0; i < size; i++)
        {
            if (((Define.HumanOutfitAllGender)i).ToString() == type)
                return GetAllGenderData((Define.HumanOutfitAllGender)i);
        }

        return -1;
    }

    public int GetOneGenderData(Define.HumanOutfitOneGender oneGender)
    {
        return OneGender[(int)oneGender];
    }
    public int GetOneGenderData(string type)
    {
        int size = (int)Define.HumanOutfitOneGender.Unknown;
        for (int i = 0; i < size; i++)
        {
            if (((Define.HumanOutfitOneGender)i).ToString() == type)
                return GetOneGenderData((Define.HumanOutfitOneGender)i);
        }

        return -1;
    }

    public void SetAllGenderData(Define.HumanOutfitAllGender allGender, int data)
    {
        AllGender[(int)allGender] = data;
    }
    public void SetOneGenderData(Define.HumanOutfitOneGender oneGender, int data)
    {
        OneGender[(int)oneGender] = data;
    }

    public void SetAllGenderData(string type, int data)
    {
        int size = (int)Define.HumanOutfitAllGender.Unknown;
        for(int i=0;i < size; i++)
        {
            if(type == ((Define.HumanOutfitAllGender)i).ToString())
            {
                AllGender[i] = data;
                break;
            }
        }
    }

    public void SetOneGenderData(string type, int data)
    {
        int size = (int)Define.HumanOutfitOneGender.Unknown;
        for (int i = 0; i < size; i++)
        {
            if (type == ((Define.HumanOutfitOneGender)i).ToString())
            {
                OneGender[i] = data;
                break;
            }
        }
    }
}

public class CharacterData
{
    private bool _isPlayer = false;

    private Define.CharacterType _type = Define.CharacterType.Human;
    public Define.CharacterType Type { get { return _type; } set { _type = value; } }

    private int _heroId = -1;
    public int HeroId { get { return _heroId; } set { _heroId = value; } }
    
    [SerializeField] protected int _heroGroup = -1;
    public int Group { get { return _heroGroup; } set { _heroGroup = value; } }

    [SerializeField] private Define.NPCPersonality _personality;
    public Define.NPCPersonality Personality { get { return _personality; } }

    private Define.Facilities _currentFacillity = Define.Facilities.Unknown;
    public Define.Facilities CurrentFacillity { get { return _currentFacillity; } set { _currentFacillity = value; } }

    private Vector3 _startPosition = new Vector3();
    public Vector3 StartPosition { get { return _startPosition; } set { _startPosition = value; } }

    private string _characterName;
    public string CharName { get { return _characterName; } set { _characterName = value; } }

    private List<int> _charRelationList = new List<int>();
    private Dictionary<int, float> _characterRelation = new Dictionary<int, float>();

    private HumanOutfit _outfit;
    private EquipWeapon _leftEquipWeapon;
    private EquipWeapon _rightEquipWeapon;

    public CharacterData(int group = -1, bool player = false, Define.NPCPersonality personality = Define.NPCPersonality.Normal, HumanOutfit outfit = null, EquipWeapon left = null,
        EquipWeapon right = null, string name = null)
    {
        _heroGroup = group;
        _isPlayer = player;
        _personality = personality;

        if (outfit == null)
            _outfit = new HumanOutfit();
        else
            _outfit = outfit;

        if (left == null)
            _leftEquipWeapon = new EquipWeapon();
        else
            _leftEquipWeapon = left;

        if (right == null)
            _rightEquipWeapon = new EquipWeapon();
        else
            _rightEquipWeapon = right;
    }

    public bool Player { get { return _isPlayer; } set { _isPlayer = value; } }
    public HumanOutfit Outfit { get { return _outfit; } }
    public EquipWeapon Left { get { return _leftEquipWeapon; } }
    public EquipWeapon Right { get { return _rightEquipWeapon; } }

    public Define.CharacterRelationship GetCharacterRelation(int num)
    {
        if (_charRelationList.Contains(num) == false)
            return Define.CharacterRelationship.Normal;

        float relation = _characterRelation[num];
        if (relation < -50)
            return Define.CharacterRelationship.Baddess;

        if (relation < -20)
            return Define.CharacterRelationship.Bad;
        if (relation < 20)
            return Define.CharacterRelationship.Normal;
        if (relation < 50)
            return Define.CharacterRelationship.Good;

        return Define.CharacterRelationship.Best;
    }
}
