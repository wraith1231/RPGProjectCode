using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIQuestDetail : UIPopup
{
    enum UITexts
    {
        QuestType,
        OrderedVillage,
        QuestTarget,
        Deadline,
        TargetVillage,
        RewardGold,
        NeedGrade,
    }

    enum UIButtons
    {
        Accept,
        Decline,
    }

    private bool _init = false;
    private UIQuestBoardWindow _parent;
    private QuestBase _quest;

    public override void Init()
    {
        if (_init == true)
            return;

        base.Init();

        Bind<TMP_Text>(typeof(UITexts));
        Bind<Button>(typeof(UIButtons));

        if(Managers.General.GlobalGroups[0].CurrentQuest != null)
        {
            Get<Button>((int)UIButtons.Accept).gameObject.SetActive(false);
        }
        else
        {
            Get<Button>((int)UIButtons.Accept).gameObject.BindUIEvent(AcceptButtonClicked);
            Get<Button>((int)UIButtons.Decline).gameObject.BindUIEvent(DeclineButtonClicked);
        }

        _init = true;
    }

    public void SetQuestData(QuestBase quest, UIQuestBoardWindow uiParent)
    {
        _quest = quest;
        _parent = uiParent;

        Get<TMP_Text>((int)UITexts.QuestType).text = "Quest Type : " + _quest.Type.ToString();
        Get<TMP_Text>((int)UITexts.OrderedVillage).text = "Ordered Village : " + _quest.OrderedVillage.VillageName;
        Get<TMP_Text>((int)UITexts.QuestTarget).text = "Quest Target : " + Managers.General.GlobalGroups[ _quest.Target].GroupName;
        Get<TMP_Text>((int)UITexts.Deadline).text = "Deadline : " + _quest.Deadline;
        Get<TMP_Text>((int)UITexts.TargetVillage).text = "Target Village : " + _quest.TargetVillage.VillageName;
        Get<TMP_Text>((int)UITexts.RewardGold).text = "Reward Gold : " + _quest.RewardText;

        string grade = Define.GetGrade(_quest.NeedPerformance);
        Get<TMP_Text>((int)UITexts.NeedGrade).text = "Need Grade : " + grade;
    }

    private void AcceptButtonClicked(PointerEventData data)
    {
        if (_quest == null) return;

        if (Managers.General.GlobalGroups[0].Performance < _quest.NeedPerformance)
            return;

        Managers.General.GlobalGroups[0].CurrentQuest = _quest;
        _parent.QuestDetailActive(false);
        Get<Button>((int)UIButtons.Accept).gameObject.SetActive(false);
        _quest = null;
    }
    private void DeclineButtonClicked(PointerEventData data)
    {
        if (_quest == null) return;

        _parent.QuestDetailActive(false);
        _quest = null;
    }
}
