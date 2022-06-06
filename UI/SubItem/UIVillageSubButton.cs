using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIVillageSubButton : UIScene
{
    enum Texts
    {
        Text,
    }

    // Start is called before the first frame update
    void Start()
    {
        Bind<TMP_Text>(typeof(Texts));

        Get<TMP_Text>((int)Texts.Text).text = transform.name;
    }

}
