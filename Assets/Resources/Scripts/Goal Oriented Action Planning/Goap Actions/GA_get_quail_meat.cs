﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_get_quail_meat : Scr_goap_action
{
    public GA_get_quail_meat()
    {
        AddPrecondition(G_Actions.QUAIL_KILLED, true);
        AddEffect(G_Actions.HAS_QUAIL_MEAT, true);
    }

    public override bool PreinitializationCondition()
    {
        m_target = m_findItem.FindWorldInteractable(WorldInteract.QUAIL);
        return m_target != null;
    }

    public override bool ActionFailed()
    {
        return m_target == null;
    }

    public override bool PerformAction()
    {
        m_navAgent.SetDestination(m_target.m_agentInteractPos);
        if (DistanceCheckObject(m_target.m_agentInteractPos, m_goapAgent.m_minRange))
        {
            ((Scr_goap_agent_bert)m_goapAgent).RotateTowardsDir(m_target.transform);
            return ((Scr_interact_quail)m_target).GetDeadQuail();
        }
        return false;
    }

    

    public override void Reset()
    {
        m_target = null;
    }
}
