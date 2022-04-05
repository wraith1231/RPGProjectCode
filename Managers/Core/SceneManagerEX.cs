using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }
    public string NextSceneName { get; private set; }

    public void LoadScene(Define.SceneType type)
    {
        NextSceneName = GetSceneName(type);

        if (NextSceneName != null)
        {
            Managers.Clear();

            SceneManager.LoadScene(NextSceneName);
        }
        else
            Debug.Log($"Failed load scene {NextSceneName}");
    }

    public void LoadSceneAsync(Define.SceneType type)
    {
        NextSceneName = GetSceneName(type);

        if(NextSceneName != null)
        {
            Managers.Clear();

            SceneManager.LoadSceneAsync("LoadingScene");
        }
        else
        {
            Debug.Log($"Failed load scen async {NextSceneName}");
        }
    }

    string GetSceneName(Define.SceneType type)
    {
        string ret = System.Enum.GetName(typeof(Define.SceneType), type);

        return ret;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
