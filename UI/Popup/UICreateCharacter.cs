using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICreateCharacter : UIPopup
{
    enum GameObjects
    {
        Character,
    }

    enum InputFields
    {
        NameInput,
    }

    enum Buttons
    {
        CreateButton,
    }

    private CharacterOutfit _outfit;

    // Start is called before the first frame update
    void Start()
    {
        HumanOutfit outfit = new HumanOutfit();
        outfit.SetStatData(Managers.Data.PlayerStartStat);

        Bind<GameObject>(typeof(GameObjects));
        Bind<TMP_InputField>(typeof(InputFields));
        Bind<Button>(typeof(Buttons));

        Get<GameObject>((int)GameObjects.Character).GetComponent<Rigidbody>().useGravity = false;
        //Get<GameObject>((int)GameObjects.Character).transform.position = Camera.main.transform.position + Camera.main.transform.forward * 4;
        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().planeDistance = 5f;
        _outfit = Get<GameObject>((int)GameObjects.Character).GetComponent<CharacterOutfit>();
        _outfit.SetOutfit(outfit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
