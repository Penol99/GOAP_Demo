using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_get_gun : Scr_goap_action
{
    public GA_get_gun()
    {
        AddEffect(G_Actions.HAS_GUN, true);
    }

    public override bool PreinitializationCondition()
    {
        m_target = m_findItem.FindItem(Item.GUN);
        return m_target != null;
    }

    public override bool ActionFailed()
    {
        return false;
    }

    public override bool PerformAction()
    {
        m_navAgent.SetDestination(m_target.transform.position);
        if (DistanceCheckObject(m_target.transform, m_goapAgent.m_minRange+1))
        {
            return m_target.Interact(m_goapAgent as Scr_goap_agent_bert);
        }
        return false;
    }


    public override void Reset()
    {
        m_target = null;
    }
}
