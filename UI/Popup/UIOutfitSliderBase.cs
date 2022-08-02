using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIOutfitSliderBase : UIBase
{
    protected HumanOutfit _outfit;
    protected CharacterOutfit _charOutfit;

    public virtual void SetCharacter(HumanOutfit outfit, CharacterOutfit character)
    {
        _outfit = outfit;
        _charOutfit = character;
    }

    protected abstract void MaxValueChange();

    protected abstract void AddListener();
}
