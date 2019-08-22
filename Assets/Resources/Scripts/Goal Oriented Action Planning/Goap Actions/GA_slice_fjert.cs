using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_slice_fjert : Scr_goap_action
{
    private Scr_fjert m_fjert;
    public GA_slice_fjert()
    {
        AddPrecondition(G_Actions.HAS_LAST_RESORT_KNIFE, true);
        AddEffect(G_Actions.FJERT_ARM_IS_SLICED, true);
        AddEffect(G_Actions.FJERT_BLEEDING, true);
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
        m_navAgent.SetDestination(m_fjert.m_agentInteractPos);
        if (DistanceCheckObject(m_fjert.m_agentInteractPos, m_goapAgent.m_minRange))
        {
            Scr_goap_agent_bert bert = ((Scr_goap_agent_bert)m_goapAgent);
            bert.RotateTowardsDir(m_fjert.transform);
            return m_fjert.CutOffArm(bert);
        }
        return false;
    }

    

    public override void Reset()
    {

    }
}
