using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextManager
{
    public struct Contexts
    {
        public string Context;
        public int ValueCount;
        public Define.InteractionEvent NextEvent;
        public int NextEventNumber;
    }
    public struct Choices
    {
        public string Context;
        public Define.InteractionEvent NextEvent;
        public int NextEventNumber;
    }

    private List<Contexts> _contexts = new List<Contexts>();
    private Dictionary<int, List<Choices>> _choices = new Dictionary<int, List<Choices>>();
    private List<string> _contextValues = null;

    public int CurrentContext = -1;
    public int CurrentChoice = -1;
    public string CurrentTalker;

    public UIConversation CurrentConversation = null;
    public UIChoiceInterface CurrentChoiceInterface = null;

    public void Init()
    {
        Managers.Resource.Load<TextAsset>("Datas/Contexts", LoadContext);
        Managers.Resource.Load<TextAsset>("Datas/Choices", LoadChoices);
    }

    private void LoadContext(TextAsset text)
    {
        string temp = text.text;
        string[] slice = temp.Split('\n');
        int id = 1;
        while(id < slice.Length)
        {
            if (slice[id] == "")
                break;
            Contexts cont = new Contexts();
            
            string[] tempStr = slice[id].Split(',');
            cont.Context = tempStr[1];
            cont.ValueCount = Convert.ToInt32(tempStr[2]);
            cont.NextEvent = (Define.InteractionEvent)Convert.ToInt32(tempStr[3]);
            cont.NextEventNumber = Convert.ToInt32(tempStr[4]);

            _contexts.Add(cont);
            id++;
        }
    }

    private void LoadChoices(TextAsset text)
    {
        string temp = text.text;
        string[] slice = temp.Split('\n');
        int id = 1;
        while (id < slice.Length)
        {
            if (slice[id] == "")
                break;
            Choices cont = new Choices();

            string[] tempStr = slice[id].Split(',');
            cont.Context = tempStr[1];
            cont.NextEvent = (Define.InteractionEvent)Convert.ToInt32(tempStr[2]);
            cont.NextEventNumber = Convert.ToInt32(tempStr[3]);

            int choiceId = Convert.ToInt32(tempStr[0]);
            if (_choices.ContainsKey(choiceId) == false)
                _choices[choiceId] = new List<Choices>();
            _choices[choiceId].Add(cont);
            id++;
        }
    }

    public void SetContextValue(params string[] values)
    {
        if(_contextValues != null)
            _contextValues.Clear();
        _contextValues = new List<string>();

        int size = values.Length;
        for(int i =0; i < size; i++)
            _contextValues.Add(values[i]);
    }

    public void ChangeToRandomTalkSurroundContext()
    {
        int number = UnityEngine.Random.Range(0, 10000);
        //잡다한 이야기
        if(number < 3000)
        {
            CurrentContext = 0;
        }
        //퀘스트 이야기
        else if(number < 6000)
        { 
            CurrentContext = 1;
        }
        //화자 없는 이야기
        else
        {
            CurrentTalker = "";
            CurrentContext = 3;
        }
    }

    public void OpenChangeOutfitSelected()
    {
        CurrentContext = 7;
    }

    public Define.InteractionEvent CheckNextContext()
    {
        Contexts cont = _contexts[CurrentContext];
        if (cont.NextEvent == Define.InteractionEvent.End)
            return Define.InteractionEvent.End;

        if(cont.NextEvent == Define.InteractionEvent.Question)
        {
            CurrentChoice = cont.NextEventNumber;
        }
        else
        {
            CurrentContext = cont.NextEventNumber;
        }

        return cont.NextEvent;
    }

    public string GetCurrentContextString()
    {
        Contexts currentContext = _contexts[CurrentContext];
        string ret = "";

        if (currentContext.ValueCount > 0)
            ret = String.Format(currentContext.Context, _contextValues.ToArray());
        else
            ret = String.Format(currentContext.Context);

        return ret;
    }

    public List<string> GetCurrentChoicesString()
    {
        List<Choices> currentChoice = _choices[CurrentChoice];
        List<string> ret = new List<string>();
        int size = currentChoice.Count;
        for (int i = 0; i < size; i++)
            ret.Add(currentChoice[i].Context);

        return ret;
    }

    public Define.InteractionEvent ChoiceSomething(int number)
    {
        Choices choice = _choices[CurrentChoice][number];

        if(choice.NextEvent == Define.InteractionEvent.Context)
        {
            CurrentContext = choice.NextEventNumber;
            Debug.Log($"current context : {CurrentContext}");
        }
        else if(choice.NextEvent == Define.InteractionEvent.Question)
        {
            CurrentChoice = choice.NextEventNumber;
            Debug.Log($"current choice : {CurrentChoice}");
        }

        return choice.NextEvent;
    }
}
