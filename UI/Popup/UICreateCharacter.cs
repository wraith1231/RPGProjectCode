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


        if(text.Length < 2 || text.Length > 10)
        {
            Get<TMP_Text>((int)Texts.WarningText).gameObject.SetActive(true);
            return;
        }

        Managers.General.EnterNewGame(text, _baseOutfit);

        Managers.Scene.LoadSceneAsync(Define.SceneType.AreaScene);
        Managers.UI.CloseAllPopup();
    }

}
