using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChoiceInterface : UIPopup
{
    enum GameObjects
    {
        ChoiceSubmenuPanel
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
        Managers.Context.CurrentChoiceInterface = null;
    }

    List<string> _choices;
    public override void Init()
    {
        base.Init();

        Time.timeScale = 0;
        Managers.Context.CurrentChoiceInterface = this;
        Bind<GameObject>(typeof(GameObjects));

        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().planeDistance = GetComponent<Canvas>().sortingOrder;

        ResetChoices();
        //Get<GameObject>((int)GameObjects.ChoiceSubmenuPanel).GetComponent<RectTransform>().
    }

    public void ResetChoices()
    {
        _choices = Managers.Context.GetCurrentChoicesString();

        GameObject panel = Get<GameObject>((int)GameObjects.ChoiceSubmenuPanel);

        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        int size = _choices.Count;
        for (int i = 0; i < size; i++)
        {
            Managers.UI.MakeSubItem<UIChoiceSubMenuButton>(panel.transform, $"{i}", SetSubMenu);
        }
    }

    private void SetSubMenu(UIChoiceSubMenuButton button)
    {
        button.ChoiceNumber = Convert.ToInt32(button.name);
        button.ChangeText(_choices[button.ChoiceNumber]);
    }
}
