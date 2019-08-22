using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum WarningType
{

    TOO_MANY_ITEMS,
    NEED_KNIFE
}
public class Scr_ui_warning : MonoBehaviour
{
    public GameObject m_warningSymbol;
    public Scr_ui_warning_text[] m_warnings;
    public Scr_spawn_manager m_spawnMan;
    public GameObject m_spawnPanel;
    public GameObject m_startPlanPanel;

    
    private bool m_spawnPanelWasActive;
    private Image m_panel;

    private void Start()
    {
        m_panel = GetComponent<Image>();
    }

    public void EnableWarning(WarningType warning)
    {
        m_panel.enabled = true;
        for (int i = 0; i < m_warnings.Length; i++)
        {
            if (m_warnings[i].m_warningType == warning)
            {
                m_warnings[i].gameObject.SetActive(true);
                m_spawnMan.m_itemInHand = null;
                m_spawnPanelWasActive = m_spawnPanel.activeInHierarchy;
                m_spawnPanel.SetActive(false);
                m_warningSymbol.SetActive(true);
                m_startPlanPanel.SetActive(false);
                return;
            }
        }
    }

    public void DisableWarnings()
    {
        m_panel.enabled = false;
        for (int i = 0; i < m_warnings.Length; i++)
        {
            m_spawnPanel.SetActive(m_spawnPanelWasActive);
            m_warnings[i].gameObject.SetActive(false);
            m_warningSymbol.SetActive(false);
            m_startPlanPanel.SetActive(true);
        }
    }
    
    
}
