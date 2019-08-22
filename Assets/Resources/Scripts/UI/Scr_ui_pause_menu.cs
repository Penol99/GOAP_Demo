using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ui_pause_menu : MonoBehaviour
{
    public GameObject m_holder;
    public GameObject m_spawnPanel;
    public GameObject m_startPlanPanel;


    private bool m_spawnPanelWasActive;
    private bool m_startPlanWasActive;
    private bool m_pauseKey;
    private bool m_paused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_pauseKey = Input.GetButtonDown("Pause");
        if (m_pauseKey && m_paused)
        {
            m_paused = false;
            ResumeGame();
        }
        if (m_pauseKey)
        {
            m_paused = true;
            PauseGame();
        }
        
        
    }

    private void PauseGame()
    {
        m_holder.SetActive(true);
        m_spawnPanelWasActive = m_spawnPanel.activeInHierarchy;
        m_startPlanWasActive = m_startPlanPanel.activeInHierarchy;
        m_spawnPanel.SetActive(false);
        m_startPlanPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        m_holder.SetActive(false);
        m_spawnPanel.SetActive(m_spawnPanelWasActive);
        m_startPlanPanel.SetActive(m_startPlanWasActive);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
