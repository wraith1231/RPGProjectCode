using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGameManager
{
    private GlobalPlayerController _globalPlayer;
    public GlobalPlayerController GlobalPlayer { get { return _globalPlayer; } }

    private List<GlobalCharacterController> _globalCharacters;
    public List<GlobalCharacterController> GlobalCharacters { get { return _globalCharacters; } }

    public List<GlobalGroupController> _globalGroups;
    public List<GlobalGroupController> GlobalGroups { get { return _globalGroups; } }

    private List<GlobalVillageController> _villages;
    public List<GlobalVillageController> GlobalVillages { get { return _villages; } }

    public void Init()
    {
        _globalPlayer = new GlobalPlayerController();
        _globalCharacters = new List<GlobalCharacterController>();
        _globalCharacters.Add(_globalPlayer);
        _globalGroups = new List<GlobalGroupController>();
        _villages = new List<GlobalVillageController>();
    }

    public void GroupSizeChange(int num)
    {
        //´Ã¸®´Â °Í¸¸ µÊ
        int size = num - _globalGroups.Count;
        for (int i = 0; i < size; i++)
            _globalGroups.Add(new GlobalGroupController(-1, "", 0));
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
        int maxGroupSize = 200;
        GroupSizeChange(maxGroupSize);

        GlobalPlayer.GlobalData.SetStatData(Managers.Data.PlayerStartStat);
        GlobalPlayer.Data.Group = 0;
        GlobalPlayer.Data.HeroId = 0;
        GlobalPlayer.Data.CharName = playerName;
        Vector3 mainPos = new Vector3(150, 0, 150);
        mainPos.y = 0;
        GlobalPlayer.Data.StartPosition = mainPos;

        GlobalPlayer.SetRightWeapon(Define.WeaponCategory.OneHand, "Sword1");
        GlobalPlayer.Data.Outfit.SetStatData(Managers.Data.PlayerStartStat);

        GlobalPlayer.Data.Outfit.CopyHumanOutfit(playerOutfit);
        GlobalGroups[0].Group = 0;
        GlobalGroups[0].GroupName = $"{playerName} Group";
        GlobalGroups[0].Foods = 100;
        GlobalGroups[0].Gold = 100;
        GlobalGroups[0].Position = Managers.Data.GetVillagePosition(0);
        GlobalGroups[0].AddGroupMember(Managers.General.GlobalPlayer.Data.HeroId);

        for (int x = 0; x < 500; x++)
        {
            HumanOutfit outfit = new HumanOutfit();
            outfit.SetNPCBaseData();

            EquipWeapon left;
            EquipWeapon right;
            NPCWeaponRandomize(out left, out right);

            int group = Random.Range(1, maxGroupSize);
            CharacterData cdata = new CharacterData(group: group, outfit: outfit, left: left, right: right);

            cdata.HeroId = x + 1;
            cdata.CharName = $"NPC{x}";

            GlobalNPCController controller = new GlobalNPCController();
            controller.Data = cdata;
            controller.GlobalData.SetNPCBaseData();
            GlobalCharacters.Add(controller);

            if (GlobalGroups[group].Group == -1)
            {
                GlobalGroups[group].Group = group;
                int vilNum = Random.Range(0, Managers.Data.GetVillageCount());
                GlobalGroups[group].Position = Managers.Data.GetVillagePosition(vilNum);
                GlobalGroups[group].GroupName = $"{cdata.CharName} Group";
                GlobalGroups[group].Foods = Random.Range(100, 10000);
                GlobalGroups[group].Gold = Random.Range(100, 10000);
                GlobalGroups[group].Type = Define.GroupType.Mercenary;
            }
            GlobalGroups[group].AddGroupMember(controller.Data.HeroId);
            Debug.Log($"{cdata.CharName} added");
        }
    }

    private void NPCWeaponRandomize(out EquipWeapon left, out EquipWeapon right)
    {
        int rand = Random.Range(0, 50);
        Define.WeaponCategory lCategory = Define.WeaponCategory.Unknown;
        Define.WeaponCategory rCategory = Define.WeaponCategory.Unknown;
        Define.WeaponType lType = Define.WeaponType.Unknown;
        Define.WeaponType rType = Define.WeaponType.Unknown;
        int num = 0;
        int size = 0;
        if (rand < 5)
        {

        }
        else if (rand < 10)
        {
            lCategory = Define.WeaponCategory.OneHand;
            lType = (Define.WeaponType)Random.Range(0, (int)Define.WeaponType.Unknown);
            size = Managers.Data.GetWeaponListSize(lCategory, lType);
            num = Random.Range(0, size);
        }
        else if (rand < 15)
        {
            rCategory = Define.WeaponCategory.OneHand;
            rType = (Define.WeaponType)Random.Range(0, (int)Define.WeaponType.Unknown);
            size = Managers.Data.GetWeaponListSize(rCategory, rType);
            num = Random.Range(0, size);
        }
        else if (rand < 20)
        {
            lCategory = Define.WeaponCategory.OneHand;
            lType = (Define.WeaponType)Random.Range(0, (int)Define.WeaponType.Unknown);
            size = Managers.Data.GetWeaponListSize(lCategory, lType);
            num = Random.Range(0, size);

            rCategory = Define.WeaponCategory.Shield;
            rType = Define.WeaponType.Shield;
            size = Managers.Data.GetWeaponListSize(rCategory, rType);
            num = Random.Range(0, size);
        }
        else if (rand < 25)
        {
            rCategory = Define.WeaponCategory.OneHand;
            rType = (Define.WeaponType)Random.Range(0, (int)Define.WeaponType.Unknown);
            size = Managers.Data.GetWeaponListSize(rCategory, rType);
            num = Random.Range(0, size);

            lCategory = Define.WeaponCategory.Shield;
            lType = Define.WeaponType.Shield;
            size = Managers.Data.GetWeaponListSize(lCategory, lType);
            num = Random.Range(0, size);
        }
        else if (rand < 30)
        {
            lCategory = Define.WeaponCategory.Shield;
            lType = Define.WeaponType.Shield;
            size = Managers.Data.GetWeaponListSize(lCategory, lType);
            num = Random.Range(0, size);

            rCategory = Define.WeaponCategory.Shield;
            rType = Define.WeaponType.Shield;
            size = Managers.Data.GetWeaponListSize(rCategory, rType);
            num = Random.Range(0, size);
        }
        else if (rand < 40)
        {
            rCategory = Define.WeaponCategory.TwoHand;
            rType = (Define.WeaponType)Random.Range(0, (int)Define.WeaponType.Unknown);
            size = Managers.Data.GetWeaponListSize(rCategory, rType);
            num = Random.Range(0, size);
        }
        else
        {
            lCategory = Define.WeaponCategory.OneHand;
            lType = (Define.WeaponType)Random.Range(0, (int)Define.WeaponType.Unknown);
            size = Managers.Data.GetWeaponListSize(lCategory, lType);
            num = Random.Range(0, size);

            rCategory = Define.WeaponCategory.OneHand;
            rType = (Define.WeaponType)Random.Range(0, (int)Define.WeaponType.Unknown);
            size = Managers.Data.GetWeaponListSize(rCategory, rType);
            num = Random.Range(0, size);
        }

        left = new EquipWeapon(lCategory, lType, num);
        right = new EquipWeapon(rCategory, rType, num);
    }
}
