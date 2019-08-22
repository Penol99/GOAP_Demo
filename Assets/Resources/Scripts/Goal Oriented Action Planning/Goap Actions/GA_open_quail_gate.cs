using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_open_quail_gate : Scr_goap_action
{
    public GA_open_quail_gate()
    {
        AddEffect(G_Actions.QUAIL_GATE_OPEN, true);
        AddPrecondition(G_Actions.HAS_QUAIL_KEY, true);
    }

    public override bool PreinitializationCondition()
    {
        m_target = m_findItem.FindWorldInteractable(WorldInteract.QUAIL_GATE);
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
            ((Scr_goap_agent_bert)m_goapAgent).RotateTowardsDir(m_target.transform);
            return m_target.Interact(m_goapAgent as Scr_goap_agent_bert);
        }
        return false;
    }

    public override void Reset()
    {
        m_target = null;
    }

}
