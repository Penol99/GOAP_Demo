using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_get_arm : Scr_goap_action
{
    private Scr_fjert m_fjert;

    public GA_get_arm()
    {
        AddPrecondition(G_Actions.FJERT_ARM_IS_SLICED, true);
        AddEffect(G_Actions.HAS_FJERT_ARM, true);
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
            bert.RotateTowardsDir(m_fjert.m_rightArmDisconnected.transform);
            return m_fjert.GetArm();
        }
        return false;
    }



    public override void Reset()
    {

    }

}
