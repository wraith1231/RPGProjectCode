using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIQuestBoardWindow : UIPopup
{
    enum UIObjects
    {
        QuestPaperParent,
        QuestDetail,
        DetailPanel,
    }
    enum UIButtons
    {
        CancelButton,
        RefreshButton,
    }

    private Transform _questParent;
    private string _currentVillage;
    private List<UIQuest> _quests = null;
    private List<int> _currentActiveQuests = null;

    public override void Init()
    {
        base.Init();

        Time.timeScale = 0;
        GetComponent<Canvas>().worldCamera = Managers.Map.UICam;

        Bind<Button>(typeof(UIButtons));
        Bind<GameObject>(typeof(UIObjects));
        QuestDetailActive(false);
        _questParent = Get<GameObject>((int)UIObjects.QuestPaperParent).transform;

        Get<Button>((int)UIButtons.CancelButton).gameObject.BindUIEvent(CancelButtonClicekd);
        Get<Button>((int)UIButtons.RefreshButton).gameObject.BindUIEvent(RefreshButtonClicekd);
        _currentVillage = Managers.General.GlobalVillages[Managers.General.GlobalGroups[0].CurrentVillageNumber].Data.VillageName;

        QuestListRefresh();
    }

    private void QuestListRefresh()
    {
        foreach(Transform child in _questParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        if (_quests == null)
            _quests = new List<UIQuest>();
        else
            _quests.Clear();

        if (_currentActiveQuests == null)
            _currentActiveQuests = new List<int>();
        else
            _currentActiveQuests.Clear();

        QuestBase[] questList = new QuestBase[ Managers.Quest.QuestLists[_currentVillage].Count];
        Managers.Quest.QuestLists[_currentVillage].CopyTo(questList);

        int listLength = questList.Length;
        //int count = 0;
        int num = 0;
        while(true)
        {
            //if (count > 10 || num >= listLength)
            if (num >= listLength)
                    break;

            if(questList[num].Cleared == false)
            {
                _currentActiveQuests.Add(num);
                Managers.UI.MakeSubItem<UIQuest>(_questParent, foo: QuestMaked);
            }

            num++;
        }

    }

    private void QuestMaked(UIQuest go)
    {
        go.SetQuestData(Managers.Quest.QuestLists[_currentVillage][_currentActiveQuests[0]], this);
        _currentActiveQuests.RemoveAt(0);

        RectTransform trans = go.GetComponent<RectTransform>();
        Vector2 min = trans.anchorMin;
        Vector2 max = trans.anchorMax;
        Vector2 size = max - min;

        trans.anchorMin = new Vector2(Random.Range(0.0f, 1.0f - size.x), Random.Range(0.0f, 1.0f - size.y));
        trans.anchorMax = trans.anchorMin + size;

        go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        
    }

    private void RefreshButtonClicekd(PointerEventData data)
    {
        QuestListRefresh();
    }

    private void CancelButtonClicekd(PointerEventData data)
    {
        Managers.UI.ClosePopupUI(this);
    }

    public void QuestDetailActive(bool activate)
    {
        Get<GameObject>((int)UIObjects.QuestDetail).SetActive(activate);
        Get<GameObject>((int)UIObjects.DetailPanel).SetActive(activate);
    }


    public void QuestSelected(QuestBase data)
    {
        QuestDetailActive(true);
        Get<GameObject>((int)UIObjects.QuestDetail).GetComponent<UIQuestDetail>().SetQuestData(data, this);
    }
}
