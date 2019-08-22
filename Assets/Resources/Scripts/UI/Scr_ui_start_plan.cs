using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ui_start_plan : MonoBehaviour
{
    public GameObject m_startPlanPanel;
    public Scr_spawn_manager m_spawnManager;

    void Update()
    {
        if(!m_startPlanPanel.activeInHierarchy && m_spawnManager.m_canStartGoap)
        {
            m_startPlanPanel.SetActive(true);
        }
    }
}
