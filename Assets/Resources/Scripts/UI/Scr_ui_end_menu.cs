using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scr_ui_end_menu : MonoBehaviour
{
    public Animator m_holder;
    private bool m_goBackInTime;


    private void Update()
    {
        if (m_goBackInTime)
        {
            if (!m_holder.GetCurrentAnimatorStateInfo(0).IsName("anim_ui_spinny_zoom"))
            {
                SceneManager.LoadScene("scn_home");
            }
        }
    }


    public void GoBackInTime()
    {
        StartCoroutine(DelayGoBack());
        m_holder.SetTrigger("trigger_spin");

    }
    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator DelayGoBack()
    {
        yield return new WaitForSeconds(.5f);
        m_goBackInTime = true;
    }

   
}
