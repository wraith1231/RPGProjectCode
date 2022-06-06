using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICharacterButton : UIScene
{
    enum Texts
    {
        Name,
        Relation,
    }

    // Start is called before the first frame update
    void Start()
    {
        Bind<TMP_Text>(typeof(Texts));

        int number = Convert.ToInt32(transform.name);

        CharacterData data = Managers.General.GlobalCharacters[number].Data;
        Define.CharacterRelationship relation = data.GetCharacterRelation(0);

        Get<TMP_Text>((int)Texts.Name).text = data.CharName;
        switch (relation)
        {
            case Define.CharacterRelationship.Baddess:
                Get<TMP_Text>((int)Texts.Relation).text = "Baddess";
                break;
            case Define.CharacterRelationship.Bad:
                Get<TMP_Text>((int)Texts.Relation).text = "Bad";
                break;
            case Define.CharacterRelationship.Normal:
                Get<TMP_Text>((int)Texts.Relation).text = "Normal";
                break;
            case Define.CharacterRelationship.Good:
                Get<TMP_Text>((int)Texts.Relation).text = "Good";
                break;
            case Define.CharacterRelationship.Best:
                Get<TMP_Text>((int)Texts.Relation).text = "Best";
                break;
        }
    }
}
