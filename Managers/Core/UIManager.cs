using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    int _order = 10;

    Stack<UIPopup> _popupStack = new Stack<UIPopup>();
    List<UIPopup> _subItemList = new List<UIPopup>();
    UIScene _scene = null;
    UIBase _worldSpaceUI = null;

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        
        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public GameObject UIRoot
    {
        get
        {
            GameObject uiRoot = GameObject.Find("UIRoot");
            if (uiRoot == null)
                uiRoot = new GameObject { name = "UIRoot" };
            return uiRoot;
        }
    }

    public void ShowSceneUI<T>(string name = null) where T : UIScene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        Managers.Resource.Instantiate($"Scene/{name}", SceneUIInstantiate);
        
        return;
    }

    private void SceneUIInstantiate(GameObject obj)
    {
        UIScene scene = Util.GetOrAddComponent<UIScene>(obj);
        _scene = scene;

        obj.transform.SetParent(UIRoot.transform);
    }

    public void MakeSubItem<T>(Transform parent = null, string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        //GameObject go = Managers.Resource.Instantiate($"SubItem/{name}", null).Result as GameObject;
        Managers.Resource.Instantiate($"SubItem/{name}", SubItemInstantiate, parent);

        return;
    }

    private void SubItemInstantiate(GameObject obj)
    {
        _subItemList.Add( obj.GetComponent<UIPopup>());
    }

    public void MakePopupUI<T>(string name = null) where T : UIPopup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        Managers.Resource.Instantiate($"Popup/{name}", PopupUIInstantiate, UIRoot.transform);

        return;
    }

    private void PopupUIInstantiate(GameObject obj)
    {
        UIPopup popup = Util.GetOrAddComponent<UIPopup>(obj);
        _popupStack.Push(popup);
    }


    public void MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        Managers.Resource.Instantiate($"WorldSpace/{name}", WorldSpaceUIInstantiate, parent);

        return;
    }

    private void WorldSpaceUIInstantiate(GameObject obj)
    {
        Canvas canvas = obj.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        _worldSpaceUI = obj.GetComponent<UIBase>();
    }

    public void ClosePopupUI(UIPopup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if(_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UIPopup p = _popupStack.Pop();
        Managers.Resource.Release(p.gameObject);
        p = null;

        _order--;
    }

    public void CloseAllPopup()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopup();
        _scene = null;
    }
}
