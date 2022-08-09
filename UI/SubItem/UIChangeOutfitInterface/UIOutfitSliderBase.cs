using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIOutfitSliderBase : UIBase
{
    protected HumanOutfit _outfit;
    protected HumanOutfit _baseOutfit;
    protected CharacterOutfit _charOutfit;

    protected bool _isInit = false;

    protected UIChangeOutfitInterface _parentUI;

    public void SetParentUI(UIChangeOutfitInterface parent)
    {
        _parentUI = parent;
    }

    public virtual void SetCharacter(HumanOutfit outfit, HumanOutfit baseOutfit, CharacterOutfit character)
    {
        _outfit = outfit;
        _baseOutfit = baseOutfit;
        _charOutfit = character;
    }

    protected abstract void MaxValueChange();

    protected abstract void AddListener();
}
