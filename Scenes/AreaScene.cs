using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScene : BaseScene
{

    public override void Clear()
    {
        Managers.Map.Clear();
    }
    
    void Awake()
    {
        base.Init();
        //Managers.Battle.BattleTerrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
/*
        Managers.General.GlobalPlayer.Data.Group = 0;
        Managers.General.GlobalPlayer.Data.HeroId = 0;
        Managers.General.GlobalPlayer.Data.CharName = "Player";
        Vector3 mainPos = new Vector3(150, 0, 150); //Managers.Battle.BattleTerrain.terrainData.size * 0.5f;
        mainPos.y = 0;
        Managers.General.GlobalPlayer.SetRightWeapon(Define.WeaponCategory.OneHand, "Sword1");
        Managers.General.GlobalPlayer.SetLeftWeapon(Define.WeaponCategory.Shield, "Buckler1");
        Managers.General.GlobalPlayer.Data.Outfit.SetAllGenderData(Define.HumanOutfitAllGender.AllGenderBackAttachment, 2);
        Managers.General.GlobalPlayer.Data.Outfit.SetOneGenderData(Define.HumanOutfitOneGender.HeadGear, 5);
        Managers.General.GlobalPlayer.Data.Outfit.SetOneGenderData(Define.HumanOutfitOneGender.Torso, 9);
        Managers.General.GlobalPlayer.Data.Outfit.SetOneGenderData(Define.HumanOutfitOneGender.Hips, 12);
        Managers.General.GlobalPlayer.Data.StartPosition = mainPos;
        Managers.Battle.AddCharList(Managers.General.GlobalPlayer.Data);

        EquipWeapon weapon = new EquipWeapon(Define.WeaponCategory.OneHand, "Sword1");
        HumanOutfit outfit1 = new HumanOutfit();
        HumanOutfit outfit2 = new HumanOutfit();

        outfit1.Gender = Define.HumanGender.Female;
        for (int i = 0; i < (int)Define.HumanOutfitAllGender.Unknown; i++)
        {
            outfit1.OneGender[i] = 2;
        }

        outfit2.Gender = Define.HumanGender.Male;
        for (int i = 0; i < (int)Define.HumanOutfitAllGender.Unknown; i++)
        {
            outfit2.OneGender[i] = 3;
        }

        for (int x = -5; x < 5; x++)
        {
            for (int z = -5; z < 5; z++)
            {
                int group = Mathf.Abs((x + z) % 5);
                CharacterData data = null;
                outfit1.SetOneGenderData(Define.HumanOutfitOneGender.Torso, group * 5);
                outfit2.SetOneGenderData(Define.HumanOutfitOneGender.Torso, group * 5);
                if (group % 2 == 0)
                    data = new CharacterData(new Vector3(mainPos.x + 15 + x * (-2), 0, mainPos.z + 10 + z * (-3)), group: group, outfit: outfit1, left: weapon);
                else
                    data = new CharacterData(new Vector3(mainPos.x + 15 + x * (-2), 0, mainPos.z + 10 + z * (-3)), group: group, outfit: outfit2, right: weapon);
                data.CharName = $"NPC{x}{z}";

                Managers.Battle.AddCharList(data);
            }
        }*/
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Managers.Scene.LoadSceneAsync(Define.SceneType.TestScene);
        }
    }
}
