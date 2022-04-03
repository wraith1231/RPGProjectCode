using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : BaseScene
{
    UILoadingScene _ui;
    public override void Clear()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Action<UILoadingScene> uiscene = EndUIInstantiate;

        Managers.Resource.Instantiate<UILoadingScene>("UI/Scene/LoadingScene", uiscene);
        
    }

    void EndUIInstantiate(UILoadingScene obj)
    {
        _ui = obj;

        StartCoroutine(LoadScene(Managers.Scene.NextSceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (operation.isDone == false)
        {
            _ui.SetSliderValue(operation.progress);
            
            if(operation.progress >= 0.9f)
            {
                _ui.SetButtonActive();
                while (_ui.IsDone == false)
                    yield return null;

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
