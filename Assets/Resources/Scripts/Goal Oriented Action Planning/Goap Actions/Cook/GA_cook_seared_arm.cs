using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_cook_seared_arm : Scr_goap_action
{
    public GA_cook_seared_arm()
    {
        AddPrecondition(G_Actions.HAS_FJERT_ARM, true);
        //AddPrecondition(G_Actions.FJERT_BLEEDING, false);
        AddEffect(G_Actions.HAS_FOOD_SEARED_ARM, true);
        AddEffect(G_Actions.HAS_FOOD, true);
    }

    public override bool PreinitializationCondition()
    {
        m_target = m_findItem.FindWorldInteractable(WorldInteract.STOVE);
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
            Scr_food.ActivateFood(FoodType.SEARED_ARM, ((Scr_goap_agent_bert)m_goapAgent).m_foodSlot);
            return m_target.Interact(m_goapAgent as Scr_goap_agent_bert);
        }
        return false;
    }

    public override void Reset()
    {

    }

}
