using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<eStateGame> StateChangedAction = delegate { };

    public static GameManager Instance;
    public enum eLevelMode
    {
        TIMER,
        MOVES
    }

    public enum eStateGame
    {
        SETUP,
        MAIN_MENU,
        GAME_STARTED,
        PAUSE,
        GAME_OVER,
    }

    private eStateGame m_state;
    public eStateGame State
    {
        get { return m_state; }
        private set
        {
            m_state = value;

            StateChangedAction(m_state);
        }
    }



    private BoardController m_boardController;

    private UIMainManager m_uiMenu;

    private LevelCondition m_levelCondition;

    private GameSettings m_gameSettings;
    public GameSettings GameSetting => m_gameSettings;


    private DataSetting m_dataSetting;
    public DataSetting DataSetting => m_dataSetting;


    public List<int> ItemCount;


    private void Awake()
    {
        // Dùng singleton để tránh việc phải setup quá nhiều
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        State = eStateGame.SETUP;

        m_gameSettings = Resources.Load<GameSettings>(Constants.GAME_SETTINGS_PATH);
        m_dataSetting =  Resources.Load<DataSetting>(Constants.DATA_SETTING_PATH);
        m_uiMenu = FindObjectOfType<UIMainManager>();
        // sử dụng singleton
        //m_uiMenu.Setup(this);
    }

    void Start()
    {
        State = eStateGame.MAIN_MENU;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_boardController != null) m_boardController.Update();
    }


    internal void SetState(eStateGame state)
    {
        State = state;

        if(State == eStateGame.PAUSE)
        {
            DOTween.PauseAll();
        }
        else
        {
            DOTween.PlayAll();
        }
    }

    public void LoadLevel(eLevelMode mode)
    {
        ItemCount = new();
        int count = Enum.GetNames(typeof(NormalItem.eNormalType)).Length;
        for (int i = 0; i < count; i++)
        {
            ItemCount.Add(0);
        }
        m_boardController = new GameObject("BoardController").AddComponent<BoardController>();
        m_boardController.StartGame();
        if (mode == eLevelMode.MOVES)
        {
            m_levelCondition = this.gameObject.AddComponent<LevelMoves>();
            m_levelCondition.Setup(m_gameSettings.LevelMoves, m_uiMenu.GetLevelConditionView(), m_boardController);
        }
        else if (mode == eLevelMode.TIMER)
        {
            m_levelCondition = this.gameObject.AddComponent<LevelTime>();
            // bỏ biến khi dùng singleton
            m_levelCondition.Setup(m_gameSettings.LevelMoves, m_uiMenu.GetLevelConditionView());
        }

        m_levelCondition.ConditionCompleteEvent += GameOver;

        State = eStateGame.GAME_STARTED;
    }

    public void ResetLevel()
    {
        if (m_levelCondition == null) return;
        ItemCount = new();
        int count = Enum.GetNames(typeof(NormalItem.eNormalType)).Length;
        for (int i = 0; i < count; i++)
        {
            ItemCount.Add(0);
        }
        m_levelCondition.ResetLevel();
        m_boardController.ResetLevel();
    }

    public void GameOver()
    {
        StartCoroutine(WaitBoardController());
    }

    internal void ClearLevel()
    {
        if (m_boardController)
        {
            m_boardController.Clear();
            Destroy(m_boardController.gameObject);
            m_boardController = null;
        }
    }

    private IEnumerator WaitBoardController()
    {
        while (m_boardController.IsBusy)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1f);

        State = eStateGame.GAME_OVER;

        if (m_levelCondition != null)
        {
            m_levelCondition.ConditionCompleteEvent -= GameOver;
            
            Destroy(m_levelCondition);
            m_levelCondition = null;
        }
    }

    void OnDestroy()
    {
        // Giải phóng bộ nhớ
        if (Instance == this) Instance = null;
    }
}
