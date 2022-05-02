using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class UICreateCharacter : UIPopup
{
    enum GameObjects
    {
        Character,
        ChangeButtons,
    }

    enum InputFields
    {
        NameInput,
    }

    enum Texts
    {
        WarningText,
    }

    enum Buttons
    {
        CreateButton,
    }

    private CharacterOutfit _outfit;
    private HumanOutfit _baseOutfit;

    // Start is called before the first frame update
    void Start()
    {
        _baseOutfit = new HumanOutfit();
        _baseOutfit.SetStatData(Managers.Data.PlayerStartStat);

        Bind<GameObject>(typeof(GameObjects));
        Bind<TMP_InputField>(typeof(InputFields));
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));

        Get<TMP_Text>((int)Texts.WarningText).gameObject.SetActive(false);

        Get<GameObject>((int)GameObjects.Character).GetComponent<Rigidbody>().useGravity = false;
        //Get<GameObject>((int)GameObjects.Character).transform.position = Camera.main.transform.position + Camera.main.transform.forward * 4;
        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().planeDistance = 5f;
        _outfit = Get<GameObject>((int)GameObjects.Character).GetComponent<CharacterOutfit>();
        _outfit.SetOutfit(_baseOutfit);

        Get<GameObject>((int)GameObjects.ChangeButtons).GetComponent<UICharacterSlider>().SetCharacter(_baseOutfit, _outfit.gameObject);
        Get<Button>((int)Buttons.CreateButton).gameObject.BindUIEvent(SubmitClicked);
    }

    private void SubmitClicked(PointerEventData eventData)
    {
        string text = Get<TMP_InputField>((int)InputFields.NameInput).text;

        Managers.General.VillageNewGame();

        if(text.Length < 2 || text.Length > 10)
        {
            Get<TMP_Text>((int)Texts.WarningText).gameObject.SetActive(true);
            return;
        }

        int maxGroupSize = 200;
        Managers.General.GroupSizeChange(maxGroupSize);

        Managers.General.GlobalPlayer.GlobalData.SetStatData(Managers.Data.PlayerStartStat);
        Managers.General.GlobalPlayer.Data.Group = 0;
        Managers.General.GlobalPlayer.Data.HeroId = 0;
        Managers.General.GlobalPlayer.Data.CharName = text;
        Vector3 mainPos = new Vector3(150, 0, 150);
        mainPos.y = 0;
        Managers.General.GlobalPlayer.Data.StartPosition = mainPos;
        
        Managers.General.GlobalPlayer.SetRightWeapon(Define.WeaponCategory.OneHand, "Sword1");
        Managers.General.GlobalPlayer.Data.Outfit.SetStatData(Managers.Data.PlayerStartStat);

        Managers.General.GlobalPlayer.Data.Outfit.CopyHumanOutfit(_baseOutfit);
        Managers.General.GlobalGroups[0].Group = 0;
        Managers.General.GlobalGroups[0].GroupName = $"{text} Group";
        Managers.General.GlobalGroups[0].Foods = 100;
        Managers.General.GlobalGroups[0].AddGroupMember(Managers.General.GlobalPlayer.Data.HeroId);

        for (int x = 0; x < 500; x++)
        {
            HumanOutfit outfit = new HumanOutfit();
            outfit.SetNPCBaseData();

            EquipWeapon left;
            EquipWeapon right;
            NPCWeaponRandomize(out left, out right);

            int group = Random.Range(1, maxGroupSize);
            CharacterData cdata = new CharacterData(new Vector3(mainPos.x+ Random.Range(-30, 30), 0, mainPos.z + Random.Range(-30, 30)), group: group, outfit:outfit, left:left, right:right);
            
            cdata.HeroId = x + 1;
            cdata.CharName = $"NPC{x}";
        
            GlobalNPCController controller = new GlobalNPCController();
            controller.Data = cdata;
            controller.GlobalData.SetNPCBaseData();
            Managers.General.GlobalCharacters.Add(controller);

            if (Managers.General.GlobalGroups[group].Group == -1)
            {
                Managers.General.GlobalGroups[group].Group = group;
                Managers.General.GlobalGroups[group].GroupName = $"{cdata.CharName} Group";
                Managers.General.GlobalGroups[group].Foods = Random.Range(100, 10000);
                Managers.General.GlobalGroups[group].Type = Define.GroupType.Mercenary;
            }
            Managers.General.GlobalGroups[group].AddGroupMember(controller.Data.HeroId);
            Debug.Log($"{cdata.CharName} added");
        }
        Managers.Map.GroupInitialize();
        Managers.Scene.LoadSceneAsync(Define.SceneType.AreaScene);
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
        else if(rand < 10)
        {
            lCategory = Define.WeaponCategory.OneHand;
            lType = (Define.WeaponType)Random.Range(0, (int)Define.WeaponType.Unknown);
            size = Managers.Data.GetWeaponListSize(lCategory, lType);
            num = Random.Range(0, size);
        }
        else if(rand < 15)
        {
            rCategory = Define.WeaponCategory.OneHand;
            rType = (Define.WeaponType)Random.Range(0, (int)Define.WeaponType.Unknown);
            size = Managers.Data.GetWeaponListSize(rCategory, rType);
            num = Random.Range(0, size);
        }
        else if(rand < 20)
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
        else if(rand < 30)
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
        else if(rand < 40)
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
