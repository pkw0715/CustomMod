using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonMono<UIManager>
{
    //----------------------------------------
    [SerializeField] private GameObject m_titlePanel;
    [SerializeField] private GameObject m_lobbyPanel;
    [SerializeField] private GameObject m_gamePanel;
    private GameObject[] m_panelsArray;

    private PanelState m_previousPanel;
    private PanelState m_currentPanel;
    //----------------------------------------
    [SerializeField] private Joystick m_joystick;

#region Properties
    public PanelState PreviousPanel
    {
        get { return m_previousPanel; }
    }

    public PanelState CurrentPanel
    {
        get { return m_currentPanel; }
    }

    public Joystick Joystick 
    {
        get { return m_joystick; }
    }
#endregion

#region Methods
    public void OnClickPlay()
    {
        switch (LoadManager.Instance.CurrentScene)
        {
            case LoadManager.SceneState.Title:
                LoadManager.Instance.LoadSceneAsync(LoadManager.SceneState.Lobby);
                break;
            case LoadManager.SceneState.Lobby:
                LoadManager.Instance.LoadSceneAsync(LoadManager.SceneState.Game);
                break;
            default: break;
        }
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenPanel(PanelState _panel)
    {
        OpenPanel((int)_panel);
    }

    public void OpenPanel(int _panelIndex)
    {
        m_panelsArray[_panelIndex].SetActive(true);
    }
    
    public void ClosePanel(PanelState _panel)
    {
        ClosePanel((int)_panel);
    }

    public void ClosePanel(int _panelIndex)
    {
        m_panelsArray[_panelIndex].SetActive(false);
    }

    public void SwitchPanel(PanelState _panel)
    {
        if (m_currentPanel == _panel) return;

        m_previousPanel = m_currentPanel;
        m_currentPanel = _panel;

        OpenPanel(_panel);
        ClosePanel(m_previousPanel);
    }
    
    //--------------------------------------------------
    protected override void Start()
    {
        base.Start();
        m_panelsArray = new GameObject[] { m_titlePanel, m_lobbyPanel, m_gamePanel };

        ClosePanel(PanelState.Lobby);
        ClosePanel(PanelState.Game);

        m_previousPanel = PanelState.None;
        m_currentPanel = PanelState.Title;
    }
#endregion

    public enum PanelState
    {
        None = -1,
        Title,
        Lobby,
        Game,
        Max
    }
}
