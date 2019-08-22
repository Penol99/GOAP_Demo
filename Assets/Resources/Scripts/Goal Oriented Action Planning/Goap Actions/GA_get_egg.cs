using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_get_egg : Scr_goap_action
{
    public GA_get_egg()
    {
        AddEffect(G_Actions.HAS_QUAIL_EGG, true);
        AddPrecondition(G_Actions.QUAIL_GATE_OPEN, true);
        AddPrecondition(G_Actions.HAS_QUAIL_FODDER, true);
        
    }

    public override bool PreinitializationCondition()
    {
        m_target = m_findItem.FindWorldInteractable(WorldInteract.QUAIL);
        return m_target != null;
    }

    public override bool ActionFailed()
    {
        return false;
    }

    public override bool PerformAction()
    {
        m_navAgent.SetDestination(m_target.m_agentInteractPos);
        if (DistanceCheckObject(m_target.m_agentInteractPos, m_goapAgent.m_minRange))
        {
            Scr_goap_agent_bert bert = ((Scr_goap_agent_bert)m_goapAgent);
            bert.RotateTowardsDir(((Scr_interact_quail)m_target).m_egg.transform);
            return ((Scr_interact_quail)m_target).GetEgg(((Scr_goap_agent_bert)m_goapAgent));
        }
        return false;
    }

    public override void Reset()
    {
        
        m_target = null;
    }


}
