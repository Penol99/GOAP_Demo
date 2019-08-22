using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_cook_carrot_soup : Scr_goap_action
{
    public GA_cook_carrot_soup()
    {
        AddPrecondition(G_Actions.HAS_CARROT, true);
        AddEffect(G_Actions.HAS_CARROT, false);
        AddEffect(G_Actions.HAS_FOOD_CARROT_SOUP, true);
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
        m_navAgent.SetDestination(m_target.transform.position);
        if (DistanceCheckObject(m_target.transform, m_goapAgent.m_minRange))
        {
            Scr_food.ActivateFood(FoodType.CARROT_SOUP, ((Scr_goap_agent_bert)m_goapAgent).m_foodSlot);
            return m_target.Interact(m_goapAgent as Scr_goap_agent_bert);
        }
        return false;
    }

    

    public override void Reset()
    {

    }

}
