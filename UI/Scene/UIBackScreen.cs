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
