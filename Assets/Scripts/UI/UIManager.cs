using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonMono<UIManager>
{
    [SerializeField] GameObject m_titlePanel;
    [SerializeField] GameObject m_lobbyPanel;
    [SerializeField] GameObject m_gamePanel;

#region Methods
    public void OnClickPlay()
    {
        LoadManager.Instance.LoadSceneAsync(LoadManager.SceneState.Lobby);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    //--------------------------------------------------
    protected override void Start()
    {
        base.Start();
    }
#endregion
}
