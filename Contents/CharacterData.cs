using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanOutfit
{
    public Define.HumanGender Gender;

    public List<int> AllGender = new List<int>();
    public List<int> OneGender = new List<int>();

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

    public int GetAllGenderData(Define.HumanOutfitAllGender allGender)
    {
        return AllGender[(int)allGender];
    }

    public int GetOneGenderData(Define.HumanOutfitOneGender oneGender)
    {
        return OneGender[(int)oneGender];
    }

    public void SetAllGenderData(Define.HumanOutfitAllGender allGender, int data)
    {
        AllGender[(int)allGender] = data;
    }
    public void SetOneGenderData(Define.HumanOutfitOneGender oneGender, int data)
    {
        OneGender[(int)oneGender] = data;
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

    private Vector3 _startPosition = new Vector3();
    public Vector3 StartPosition { get { return _startPosition; } set { _startPosition = value; } }

    private string _characterName;
    public string CharName { get { return _characterName; } set { _characterName = value; } }

    private HumanOutfit _outfit;
    private EquipWeapon _leftEquipWeapon;
    private EquipWeapon _rightEquipWeapon;

    public CharacterData(Vector3 startPos, int group = -1, bool player = false, Define.NPCPersonality personality = Define.NPCPersonality.Normal, HumanOutfit outfit = null, EquipWeapon left = null,
        EquipWeapon right = null, string name = null)
    {
        _startPosition = startPos;
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
}
