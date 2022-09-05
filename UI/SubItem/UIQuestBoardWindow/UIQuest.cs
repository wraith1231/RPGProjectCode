using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIQuest : UIBase
{
    enum UIText
    {
        QuestText,

    }

    private UIQuestBoardWindow _parent;
    private QuestBase _quest;

    private bool _inited = false;
    public override void Init()
    {
        if (_inited == true)
            return;
        Bind<TMP_Text>(typeof(UIText));
        gameObject.BindUIEvent(QuestClicked);
        _inited = true;
    }

    public void SetQuestData(QuestBase quest, UIQuestBoardWindow uiParent)
    {
        if (_inited == false)
            Init();

        _parent = uiParent;
        _quest = quest;

        string text = "";
        text += _quest.Type.ToString() + "\n";
        text += Managers.General.GlobalGroups[_quest.Target].GroupName + "\n";
        text += _quest.RewardText;

        Get<TMP_Text>((int)UIText.QuestText).text = text;

    }

    private void QuestClicked(PointerEventData data)
    {
        if (_parent == null) return;

        _parent.QuestSelected(_quest);

    }
}
