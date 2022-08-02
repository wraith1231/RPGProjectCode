using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIChangeOutfitInterface : UIPopup
{
    enum UIObjects
    {
        Character,
        AllGenderSlider,
        OneGenderSlider,
    }

    enum Buttons
    {
        SubmitButton,
        LeftRotate,
        RightRotate,
    }

    private CharacterOutfit _outfit;
    private HumanOutfit _baseData;

    public override void Init()
    {
        Time.timeScale = 0;
        base.Init();

        Bind<GameObject>(typeof(UIObjects));
        Bind<Button>(typeof(Buttons));
        
        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().planeDistance = GetComponent<Canvas>().sortingOrder;

        _outfit = Get<GameObject>((int)UIObjects.Character).GetComponent<CharacterOutfit>();
        _baseData = new HumanOutfit();
        _baseData.CopyHumanOutfit( Managers.General.GlobalPlayer.Data.Outfit);

        _outfit.SetOutfit(_baseData);

        Get<GameObject>((int)UIObjects.AllGenderSlider).GetComponent<UIAllGenderOutfitSlider>().SetCharacter(_baseData, _outfit);
        Get<GameObject>((int)UIObjects.OneGenderSlider).GetComponent<UIOneGenderOutfitSlider>().SetCharacter(_baseData, _outfit);

        Get<Button>((int)Buttons.LeftRotate).gameObject.BindUIEvent(LeftRotateButton);
        Get<Button>((int)Buttons.RightRotate).gameObject.BindUIEvent(RightRotateButton);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }

    private void LeftRotateButton(PointerEventData eventData)
    {
        Get<GameObject>((int)UIObjects.Character).transform.Rotate(Vector3.up, 10 * Time.deltaTime);
    }
    private void RightRotateButton(PointerEventData eventData)
    {
        Get<GameObject>((int)UIObjects.Character).transform.Rotate(Vector3.up, -10 * Time.deltaTime);
    }
}
