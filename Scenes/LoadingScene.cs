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
        base.Init();
        Managers.UI.ShowSceneUI<UILoadingScene>(foo : EndUIInstantiate);
        Managers.Resource.ReleaseStock();
    }

    void EndUIInstantiate(GameObject obj)
    {
        _ui = obj.GetComponent<UILoadingScene>();

        StartCoroutine(LoadScene(Managers.Scene.NextSceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        Time.timeScale = 0;

        if (sceneName.Equals("TestScene") || sceneName.Equals("BattleScene"))
            BattleSceneLoad();
        else if (sceneName.Equals("AreaScene"))
            AreaSceneLoad();

        float progress = 0;
        while (operation.isDone == false)
        {
            while (_ui.Initialized == false)
                yield return null;
            
            if(sceneName.Equals("TestScene") || sceneName.Equals("BattleScene"))
            {
                progress = Managers.Battle.CurrentProgress;
            }
            else if(sceneName.Equals("AreaScene"))
            {
                progress = Managers.Map.CurrentProgress;
            }

            _ui.SetSliderValue(progress);
            
            if(progress >= 0.9f)
            {
                _ui.SetButtonActive();
                while (_ui.IsDone == false)
                    yield return null;
                
                operation.allowSceneActivation = true;

                Time.timeScale = 1;
                Managers.Resource.Release(_ui.gameObject);
            }

            yield return null;
        }
    }

    private void BattleSceneLoad()
    {
        Managers.Battle.BattleSceneInitialize();
    }

    private void AreaSceneLoad()
    {
        Managers.Map.DataInstantiate();
    }
}
