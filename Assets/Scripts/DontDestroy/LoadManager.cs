using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : SingletonMono<LoadManager>
{
    //--------------------------------------------------
    private SceneState m_previousScene;
    private SceneState m_loadingScene;
    private SceneState m_currentScene;

    //--------------------------------------------------
    private bool m_currentlyLoadingScene;

    //--------------------------------------------------
    private AsyncOperation m_asynOperation;
    
    //--------------------------------------------------

#region Public Properties
    public SceneState PreviousScene
    {
        get { return m_previousScene; }
    }
    public SceneState LoadingScene
    {
        get { return m_loadingScene; }
    }
    public SceneState CurrentScene
    {
        get { return m_currentScene; }
    }
#endregion
    //--------------------------------------------------
#region Methods
    protected override void Start()
    {
        base.Start();
        m_previousScene = m_loadingScene = SceneState.None;
        m_currentScene = SceneState.Title;
    }
    
    public void LoadSceneAsync(SceneState scene)
    {
        m_currentlyLoadingScene = true;
        m_loadingScene = scene;
        m_asynOperation = SceneManager.LoadSceneAsync((int)scene);
    }

    // Can be replaced with Coroutine later on.
    private void Update()
    {
        if (m_currentlyLoadingScene)
        {
            if (m_asynOperation.isDone)
            {
                m_previousScene = m_currentScene;
                m_currentScene = m_loadingScene;
                m_loadingScene = SceneState.None;

                m_currentlyLoadingScene = false;
            }
        }
    }
#endregion

    public enum SceneState
    {
        None = -1,
        Title,
        Lobby,
        Game,
        MAX
    }
}
