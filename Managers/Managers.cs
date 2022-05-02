using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance { get { Init(); return s_instance; } }

    #region Core
    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance._resource; } }

    UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } }

    SceneManagerEX _scene = new SceneManagerEX();
    public static SceneManagerEX Scene { get { return Instance._scene; } }

    SoundManager _sound = new SoundManager();
    public static SoundManager Sound { get { return Instance._sound; } }

    PoolManager _pool = new PoolManager();
    public static PoolManager Pool { get { return Instance._pool; } }

    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }

    #endregion

    #region Contents
    GeneralGameManager _generalGame = new GeneralGameManager();
    public static GeneralGameManager General { get { return Instance._generalGame; } }

    MapGameManager _mapGame = new MapGameManager();
    public static MapGameManager Map { get { return Instance._mapGame; } }

    BattleGameManager _battleGame = new BattleGameManager();
    public static BattleGameManager Battle { get { return Instance._battleGame; } }

    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {
        if(_input != null)
            _input.OnUpdate();
        if (_mapGame.AreaSceneNow == true)
            _mapGame.OnUpdate();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._pool.Init();
            s_instance._sound.Init();
            s_instance._data.Init();
            s_instance._generalGame.Init();
        }
    }

    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        Pool.Clear();
    }
}
