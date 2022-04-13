using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBackScreen : UIScene
{
    enum GameObjects
    {
        TitlePanel
    }

    enum Buttons
    { 
        NewGameButton,
        ContinueButton,
        EndButton
    }


    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.NewGameButton).gameObject.BindUIEvent(OnNewGameButton);
        GetButton((int)Buttons.ContinueButton).gameObject.BindUIEvent(OnContinueButton);
        GetButton((int)Buttons.EndButton).gameObject.BindUIEvent(OnEndButton);
    }

    public void OnNewGameButton(PointerEventData data)
    {
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
                CharacterData cdata = null;
                outfit1.SetOneGenderData(Define.HumanOutfitOneGender.Torso, group * 5);
                outfit2.SetOneGenderData(Define.HumanOutfitOneGender.Torso, group * 5);
                if (group % 2 == 0)
                    cdata = new CharacterData(new Vector3(mainPos.x + 15 + x * (-2), 0, mainPos.z + 10 + z * (-3)), group: group, outfit: outfit1, left: weapon);
                else
                    cdata = new CharacterData(new Vector3(mainPos.x + 15 + x * (-2), 0, mainPos.z + 10 + z * (-3)), group: group, outfit: outfit2, right: weapon);
                cdata.CharName = $"NPC{x}{z}";

                GlobalNPCController controller = new GlobalNPCController();
                controller.Data = cdata;
                Managers.General.GlobalCharacters.Add(controller);

            }
        }
        Managers.Scene.LoadSceneAsync(Define.SceneType.AreaScene);
    }

    public void OnContinueButton(PointerEventData data)
    {
    }

    public void OnEndButton(PointerEventData data)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
