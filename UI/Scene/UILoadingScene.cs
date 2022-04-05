using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UILoadingScene : UIScene
{
    enum GameObjects
    {
        BackPanel
    }

    enum Sliders
    {
        LoadingBar
    }

    enum Buttons
    {
        StartButton
    }

    public bool IsDone = false;
    public bool Initialized = false;

    // Start is called before the first frame update
    void Start()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Slider>(typeof(Sliders));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.StartButton).gameObject.BindUIEvent(OnButtonClicked);
        GetButton((int)Buttons.StartButton).gameObject.SetActive(false);

        Initialized = true;
    }

    public void SetSliderValue(float value)
    {
        if (Get<Slider>((int)Sliders.LoadingBar) != null)
            Get<Slider>((int)Sliders.LoadingBar).value = value;
    }

     public void SetButtonActive()
    {
        Get<Slider>((int)Sliders.LoadingBar).gameObject.SetActive(false);
        GetButton((int)Buttons.StartButton).gameObject.SetActive(true);
    }

    public void OnButtonClicked(PointerEventData data)
    {
        IsDone = true;
    }
}
