using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameManager
{
    public GlobalCharacterController GlobalPlayer { get { return _globalCharacters[0]; } }

    private List<GlobalCharacterController> _globalCharacters;
    public List<GlobalCharacterController> GlobalCharacters { get { return _globalCharacters; } }

    public List<GlobalGroupController> _globalGroups;
    public List<GlobalGroupController> GlobalGroups { get { return _globalGroups; } }

    private List<GlobalVillageController> _villages;
    public List<GlobalVillageController> GlobalVillages { get { return _villages; } }

    private int _maxCharCount = 0;
    private int _maxGroupCount = 0;

    public void Init()
    {
        _globalCharacters = new List<GlobalCharacterController>();
        _globalGroups = new List<GlobalGroupController>();
        _villages = new List<GlobalVillageController>();
    }

    public void VillageNewGame()
    {
        int size = Managers.Data.GetVillageCount();
        for(int i =0 ; i < size ; i++)
        {
            _villages.Add(new GlobalVillageController());
            _villages[i].NewGameLoad();
        }
    }

    public GlobalVillageData GetVillageData(string name)
    {
        int size = _villages.Count;
        for(int i = 0; i < size; i++)
        {
            if(name == _villages[i].Data.VillageName)
            {
                return _villages[i].Data;
            }
        }

        return null;
    }

    public bool ConstainsId(int id)
    {
        int size = _globalCharacters.Count;
        for(int i = 0; i < size; i++)
        {
            if (id == _globalCharacters[i].Data.HeroId)
                return true;
        }

        return false;
    }

    public void EnterNewGame(string playerName, HumanOutfit playerOutfit)
    {
        VillageNewGame();
        
        Vector3 pos = Managers.Data.VillageDataList[0].Position;
        MakeGroup(playerName, 100, 100, pos, Define.GroupType.Mercenary);
        EquipWeapon weapon = new EquipWeapon(Define.WeaponCategory.OneHand, "Sword1");
        MakeCharacter(0, playerName, Managers.Data.PlayerStartStat, Define.NPCPersonality.Normal, playerOutfit, null, weapon);

        int maxVillCount = Managers.Data.VillageDataList.Count;
        List<int> mercenaryGroupMemberCount = new List<int>();
        int groupCount = Random.Range(50, 200);
        for(int i = 0; i < groupCount; i++)
        {
            pos = Managers.Data.GetVillagePosition(Random.Range(0, maxVillCount));
            MakeGroup($"Group {i}", Random.Range(100, 10000), Random.Range(100, 10000), pos, Define.GroupType.Mercenary);
            mercenaryGroupMemberCount.Add( Random.Range(1, 10));
        }

        int count = 1;
        groupCount = mercenaryGroupMemberCount.Count;
        //int groupId = 0;
        while(count <= groupCount)
        {
            for(int i = 0; i < mercenaryGroupMemberCount[count-1]; i++)
            {
                MakeRandomCharacter(count, $"NPC {_maxCharCount}");
                //HumanOutfit outfit = new HumanOutfit();
                //outfit.SetNPCBaseData();
                //
                //EquipWeapon left;
                //EquipWeapon right;
                //NPCWeaponRandomize(out left, out right);
                //
                ////groupId = count + 1;
                //CharacterData cdata = new CharacterData(group: count, outfit: outfit, left: left, right: right);
                //
                //MakeCharacter(count, $"NPC {_maxCharCount}", Managers.Data.GetRandomStatData(), Define.NPCPersonality.Normal, outfit, left, right);
            }
            count++;
        }

    }

    public int MakeGroup(string groupName, float food, int gold, Vector3 position, Define.GroupType type)
    {
        int id = _maxGroupCount++;
        GlobalGroupController controller = new GlobalGroupController(id, groupName, food, gold, type);
        controller.Position = position;
        controller.Type = type;

        _globalGroups.Add(controller);

        return id;
    }

    public int MakeCharacter(int groupId, string charName, Data.StatData statData, Define.NPCPersonality personality, HumanOutfit outfit, EquipWeapon left, EquipWeapon right, Define.CharacterType type = Define.CharacterType.Human)
    {
        int id = _maxCharCount++;
        bool isPlayer = false;
        if (id == 0)
            isPlayer = true;

        CharacterData cdata = new CharacterData(groupId, isPlayer, personality, outfit, left, right, charName);
        cdata.Type = type;
        cdata.HeroId = id;
        cdata.CharName = charName;

        GlobalCharacterController controller;
        if (isPlayer == true)
        {
            controller = new GlobalPlayerController();
            controller.Data = cdata;
            controller.Data.Outfit.CopyHumanOutfit(outfit);
            controller.GlobalData.SetStatData(statData);
        }
        else
        {
            controller = new GlobalNPCController();
            controller.Data = cdata;
            controller.Data.Outfit.CopyHumanOutfit(outfit);
            controller.GlobalData.SetStatData(statData);
        }
        controller.BattleData = new BattleCharacterData(controller.GlobalData);
        
        _globalCharacters.Add(controller);
        _globalGroups[groupId].AddGroupMember(id);

        return id;
    }

    public void MakeRandomCharacter(int groupId, string name)
    {
        HumanOutfit outfit = new HumanOutfit();
        outfit.SetNPCBaseData();

        EquipWeapon left;
        EquipWeapon right;
        NPCWeaponRandomize(out left, out right);

        //groupId = count + 1;
        CharacterData cdata = new CharacterData(group: groupId, outfit: outfit, left: left, right: right);

        MakeCharacter(groupId, name, Managers.Data.GetRandomStatData(), Define.NPCPersonality.Normal, outfit, left, right);
    }

    private void NPCWeaponRandomize(out EquipWeapon left, out EquipWeapon right)
    {
        int rand = Random.Range(0, 50);

        int oneHandRange = (int)Define.WeaponType.Spear;
        int twoHandRange = (int)Define.WeaponType.Bow;

        Define.WeaponCategory lCategory = Define.WeaponCategory.Unknown;
        Define.WeaponCategory rCategory = Define.WeaponCategory.Unknown;
        Define.WeaponType lType = Define.WeaponType.Unknown;
        Define.WeaponType rType = Define.WeaponType.Unknown;
        int lnum, rnum;
        int size;
        size = lnum = rnum = 0;

        if (rand < 5)
        {

        }
        else if (rand < 10)
        {
            lCategory = Define.WeaponCategory.OneHand;
            lType = (Define.WeaponType)Random.Range(0, oneHandRange);
            size = Managers.Data.GetWeaponListSize(lCategory, lType);
            lnum = Random.Range(0, size);
        }
        else if (rand < 15)
        {
            rCategory = Define.WeaponCategory.OneHand;
            rType = (Define.WeaponType)Random.Range(0, oneHandRange);
            size = Managers.Data.GetWeaponListSize(rCategory, rType);
            rnum = Random.Range(0, size);
        }
        else if (rand < 20)
        {
            lCategory = Define.WeaponCategory.OneHand;
            lType = (Define.WeaponType)Random.Range(0, oneHandRange);
            size = Managers.Data.GetWeaponListSize(lCategory, lType);
            lnum = Random.Range(0, size);

            rCategory = Define.WeaponCategory.Shield;
            rType = Define.WeaponType.Shield;
            size = Managers.Data.GetWeaponListSize(rCategory, rType);
            rnum = Random.Range(0, size);
        }
        else if (rand < 25)
        {
            rCategory = Define.WeaponCategory.OneHand;
            rType = (Define.WeaponType)Random.Range(0, oneHandRange);
            size = Managers.Data.GetWeaponListSize(rCategory, rType);
            rnum = Random.Range(0, size);

            lCategory = Define.WeaponCategory.Shield;
            lType = Define.WeaponType.Shield;
            size = Managers.Data.GetWeaponListSize(lCategory, lType);
            lnum = Random.Range(0, size);
        }
        else if (rand < 30)
        {
            lCategory = Define.WeaponCategory.Shield;
            lType = Define.WeaponType.Shield;
            size = Managers.Data.GetWeaponListSize(lCategory, lType);
            lnum = Random.Range(0, size);

            rCategory = Define.WeaponCategory.Shield;
            rType = Define.WeaponType.Shield;
            size = Managers.Data.GetWeaponListSize(rCategory, rType);
            rnum = Random.Range(0, size);
        }
        else if (rand < 40)
        {
            rCategory = Define.WeaponCategory.TwoHand;
            //나중에 shield로 바꿀것
            rType = (Define.WeaponType)Random.Range(0, twoHandRange);
            if (rType == Define.WeaponType.Dagger) rType = Define.WeaponType.Sword;
            size = Managers.Data.GetWeaponListSize(rCategory, rType);
            rnum = Random.Range(0, size);
        }
        else
        {
            lCategory = Define.WeaponCategory.OneHand;
            lType = (Define.WeaponType)Random.Range(0, oneHandRange);
            size = Managers.Data.GetWeaponListSize(lCategory, lType);
            lnum = Random.Range(0, size);

            rCategory = Define.WeaponCategory.OneHand;
            rType = (Define.WeaponType)Random.Range(0, oneHandRange);
            size = Managers.Data.GetWeaponListSize(rCategory, rType);
            rnum = Random.Range(0, size);
        }


        left = new EquipWeapon(lCategory, lType, lnum);
        right = new EquipWeapon(rCategory, rType, rnum);
    }
}
