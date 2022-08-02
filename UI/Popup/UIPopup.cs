using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : UIBase
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);

        if (GetComponent<Canvas>() == null) return;

        if (GetComponent<Canvas>().renderMode == RenderMode.ScreenSpaceCamera)
        {
        }
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
