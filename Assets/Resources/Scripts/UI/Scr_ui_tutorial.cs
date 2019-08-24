using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scr_ui_tutorial : MonoBehaviour
{
    public GameObject m_forewordPanel, m_tutorialPanel;

    public void TitleScreen()
    {
        SceneManager.LoadScene("scn_title_screen");
    }
    public void TutorialScreen()
    {
        m_forewordPanel.SetActive(false);
        m_tutorialPanel.SetActive(true);
    }
    public void ForewordScreen()
    {
        m_forewordPanel.SetActive(true);
        m_tutorialPanel.SetActive(false);
    }
    public void StartDemo()
    {
        SceneManager.LoadScene("scn_home");
    }


    
}
