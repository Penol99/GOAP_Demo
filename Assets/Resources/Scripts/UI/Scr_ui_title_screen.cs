using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scr_ui_title_screen : MonoBehaviour
{
    private bool m_skipTutorial;

    public void StartDemo()
    {
        string scene;
        if (!m_skipTutorial)
            scene = "scn_tutorial";
        else
            scene = "scn_home";

        SceneManager.LoadScene(scene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToggleSkipTutorial()
    {
        m_skipTutorial = !m_skipTutorial;
    }
}
