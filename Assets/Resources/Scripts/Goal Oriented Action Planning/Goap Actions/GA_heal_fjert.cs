using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GA_heal_fjert : Scr_goap_action
{
    private Scr_fjert m_fjert;

    public GA_heal_fjert()
    {
        AddPrecondition(G_Actions.HAS_FOOD, false);
        AddPrecondition(G_Actions.HAS_BAND_AID, true);
        AddEffect(G_Actions.FJERT_BLEEDING, false);
    }
    public override bool PreinitializationCondition()
    {
        m_fjert = ((Scr_goap_agent_bert)m_goapAgent).m_fjert;
        return m_fjert != null;
    }


    public override bool ActionFailed()
    {
        return m_fjert == null;
    }

    public override bool PerformAction()
    {
        m_navAgent.SetDestination(m_fjert.m_agentInteractPos);
        if (DistanceCheckObject(m_fjert.m_agentInteractPos, m_goapAgent.m_minRange))
        {
            Scr_goap_agent_bert bert = ((Scr_goap_agent_bert)m_goapAgent);
            bert.RotateTowardsDir(m_fjert.transform);
            if (m_fjert.AddBandaid(bert))
            {
                var bandaid = from x in bert.m_items
                              where x.GetComponent<Scr_item_pickup>().m_itemType == Item.BANDAID
                              select x;
                bandaid.FirstOrDefault().SetActive(false);
                return true;
            }
        }
        return false;
    }

    
    public override void Reset()
    {
        m_fjert = null;
    }

}
