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
        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.NewGameButton).gameObject.BindUIEvent(OnNewGameButton);
        GetButton((int)Buttons.ContinueButton).gameObject.BindUIEvent(OnContinueButton);
        GetButton((int)Buttons.EndButton).gameObject.BindUIEvent(OnEndButton);
    }

    public void OnNewGameButton(PointerEventData data)
    {
        GetButton((int)Buttons.NewGameButton).gameObject.SetActive(false);
        GetButton((int)Buttons.ContinueButton).gameObject.SetActive(false);
        GetButton((int)Buttons.EndButton).gameObject.SetActive(false);

        Managers.UI.MakePopupUI<UICreateCharacter>();

        //Managers.General.GlobalPlayer.GlobalData.SetStatData(Managers.Data.PlayerStartStat);
        //Managers.General.GlobalPlayer.Data.Group = 0;
        //Managers.General.GlobalPlayer.Data.HeroId = 0;
        //Managers.General.GlobalPlayer.Data.CharName = "Player";
        //Vector3 mainPos = new Vector3(150, 0, 150);
        //mainPos.y = 0;
        //Managers.General.GlobalPlayer.SetRightWeapon(Define.WeaponCategory.OneHand, "Sword1");
        //Managers.General.GlobalPlayer.SetLeftWeapon(Define.WeaponCategory.Shield, "Buckler1");
        //Managers.General.GlobalPlayer.Data.Outfit.SetStatData(Managers.Data.PlayerStartStat);
        //
        //Managers.General.GlobalPlayer.Data.StartPosition = mainPos;
        //
        //EquipWeapon weapon = new EquipWeapon(Define.WeaponCategory.OneHand, "Sword1");
        //
        //for (int x = 0; x < 100; x++)
        //{
        //    HumanOutfit outfit = new HumanOutfit();
        //    outfit.SetNPCBaseData();
        //    int group = Random.Range(1, 30);
        //    CharacterData cdata = new CharacterData(new Vector3(mainPos.x+ Random.Range(-30, 30), 0, mainPos.z + Random.Range(-30, 30)), group: group, outfit:outfit);
        //    cdata.HeroId = x + 1;
        //    cdata.CharName = $"NPC{x}";
        //
        //    GlobalNPCController controller = new GlobalNPCController();
        //    controller.Data = cdata;
        //    controller.GlobalData.SetNPCBaseData();
        //    Managers.General.GlobalCharacters.Add(controller);
        //    Debug.Log($"{cdata.CharName} added");
        //}
        //Managers.Map.GroupInitialize();
        //Managers.Scene.LoadSceneAsync(Define.SceneType.AreaScene);
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
