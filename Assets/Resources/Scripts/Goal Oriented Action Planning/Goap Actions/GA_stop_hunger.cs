using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GA_stop_hunger : Scr_goap_action
{
    private bool m_foodStateRead;

    public GA_stop_hunger()
    {
        AddPrecondition(G_Actions.HAS_FOOD, true);
        AddPrecondition(G_Actions.AT_DINNER_TABLE, true);
        AddEffect(G_Actions.STOP_HUNGER, true);
    }

    public override bool PreinitializationCondition()
    {
        return true;
    }

    public override bool ActionFailed()
    {
        return false;
    }

    public override bool PerformAction()
    {
        if (!m_foodStateRead)
        {
            ((Scr_goap_agent_bert)m_goapAgent).AddFoodPoints();
            m_foodStateRead = true;
            SceneManager.LoadScene("scn_end_not_hungry");
            return true;
        }
        return false;

    }    

    public override void Reset()
    {
        m_foodStateRead = false;
    }

}
