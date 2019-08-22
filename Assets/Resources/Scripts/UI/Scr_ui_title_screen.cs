using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scr_ui_title_screen : MonoBehaviour
{

    public void StartDemo()
    {
        SceneManager.LoadScene("scn_home");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
