using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_let_fjert_die : Scr_goap_action
{
    private Scr_fjert m_fjert;

    public GA_let_fjert_die()
    {
        AddPrecondition(G_Actions.FJERT_ARM_IS_SLICED, true);
        AddEffect(G_Actions.FJERT_BLEEDING, false);
    }

    public override bool PreinitializationCondition()
    {
        m_fjert = ((Scr_goap_agent_bert)m_goapAgent).m_fjert;
        return m_fjert != null;
    }

    public override bool ActionFailed()
    {
        return false;
    }

    public override bool PerformAction()
    {
        return true;
    }

    public override void Reset()
    {

    }
}
