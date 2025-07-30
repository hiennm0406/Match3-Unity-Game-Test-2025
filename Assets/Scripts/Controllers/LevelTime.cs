using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTime : LevelCondition
{
    private float m_time;
    private float m_timeTmp;

    private GameManager m_mngr;

    public override void Setup(float value, Text txt)
    {
        base.Setup(value, txt);

        // Cache
        m_mngr = GameManager.Instance;

        m_time = value;
        m_timeTmp = value;

        UpdateText();
    }
    public override void ResetLevel()
    {
        m_time = m_timeTmp;
        UpdateText();
    }


    private void Update()
    {
        if (m_conditionCompleted) return;

        if (m_mngr.State != GameManager.eStateGame.GAME_STARTED) return;

        m_time -= Time.deltaTime;

        UpdateText();

        if (m_time <= -1f)
        {
            OnConditionComplete();
        }
    }

    protected override void UpdateText()
    {
        if (m_time < 0f) return;

        m_txt.text = string.Format("TIME:\n{0:00}", m_time);
    }
}
