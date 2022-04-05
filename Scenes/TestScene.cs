using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    public override void Clear()
    {

    }

    void Awake()
    {
        Managers.Battle.BattleTerrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();

        Managers.General.GlobalPlayer.Data.Group = 0;
        Managers.General.GlobalPlayer.Data.HeroId = 0;
        Managers.General.GlobalPlayer.Data.CharName = "Player";
        Vector3 mainPos = Managers.Battle.BattleTerrain.terrainData.size * 0.5f;
        mainPos.y = 0;
        Managers.General.GlobalPlayer.SetLeftWeapon(Define.WeaponCategory.OneHand, "Axe1");
        Managers.General.GlobalPlayer.SetRightWeapon(Define.WeaponCategory.OneHand, "Sword1");
        Managers.General.GlobalPlayer.Data.StartPosition = mainPos;
        Managers.Battle.AddCharList(Managers.General.GlobalPlayer.Data);

        
        EquipWeapon weapon = new EquipWeapon(Define.WeaponCategory.OneHand, "Sword1");
        HumanOutfit outfit1 = new HumanOutfit();
        HumanOutfit outfit2 = new HumanOutfit();

        outfit1.Gender = Define.HumanGender.Female;
        for(int i = 0; i < (int)Define.HumanOutfitAllGender.Unknown; i++)
        {
            outfit1.OneGender[i] = 2;
        }

        outfit2.Gender = Define.HumanGender.Male;
        for (int i = 0; i < (int)Define.HumanOutfitAllGender.Unknown; i++)
        {
            outfit2.OneGender[i] = 3;
        }

        for (int x = -5; x < 6; x++)
        {
            for(int z = -5; z < 6; z++)
            {
                int group = Mathf.Abs((x+z) % 5);
                CharacterData data = null;
                if (group % 2 == 0)
                    data = new CharacterData(new Vector3(mainPos.x + 15 + x * (-2), 0, mainPos.z + 10 + z * (-3)), group: group, outfit: outfit1, left: weapon);
                else
                    data = new CharacterData(new Vector3(mainPos.x + 15 + x * (-2), 0, mainPos.z + 10 + z * (-3)), group: group, outfit:outfit2, right: weapon);
                data.CharName = $"NPC{x}{z}";

                Managers.Battle.AddCharList(data);
            }
        }
        
        Managers.Battle.GroupInitialize();
        Managers.Battle.LoadCharacterPrefab();
    }
}
